using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.Http
{
    public class HttpService : IHttpService
    {
        private const int BufferLength = 8096;

        private static readonly Func<Uri, WebRequest> defaultFactory = url =>
                                                                           {
                                                                               var request =
                                                                                   (HttpWebRequest)
                                                                                   WebRequest.Create(url);

                                                                               request.Method = "GET";
                                                                               request.AllowAutoRedirect = true;
                                                                               request.MaximumAutomaticRedirections = 4;
                                                                               request.Timeout = 5*1000;
                                                                               // Is 5 Seconds too much?
                                                                               request.AutomaticDecompression =
                                                                                   DecompressionMethods.GZip |
                                                                                   DecompressionMethods.Deflate;
                                                                               request.Accept = "*.*";
                                                                               request.Expect = string.Empty;
                                                                               request.CookieContainer =
                                                                                   new CookieContainer();

                                                                               return request;
                                                                           };

        private static Func<Uri, WebRequest> factory;

        public static Func<Uri, WebRequest> CreateRequest
        {
            [DebuggerStepThrough]
            get { return factory ?? defaultFactory; }

            [DebuggerStepThrough]
            set { factory = value; }
        }

        #region IHttpService Members

        public virtual HttpServiceResult Get(string url)
        {
            WebRequest request = CreateRequest(new Uri(url));
            WebResponse response = request.GetResponse();

            if (response == null)
            {
                throw new InvalidOperationException("Unable to get the response.");
            }

            var httpResponse = response as HttpWebResponse;
            var statusCode = (int) HttpStatusCode.OK;

            if (httpResponse != null)
            {
                statusCode = (int) httpResponse.StatusCode;
            }

            if (IsError(statusCode))
            {
                return new HttpServiceResult(null, null, statusCode);
            }

            Stream payload = response.GetResponseStream();

            if (payload == null)
            {
                throw new InvalidOperationException("Unable to get the response stream.");
            }

            string content;

            using (var reader = new StreamReader(payload))
            {
                content = reader.ReadToEnd();
            }

            string contentType = response.ContentType;

            var serviceResult = new HttpServiceResult(content, contentType, statusCode);

            if (httpResponse != null)
            {
                foreach (string key in response.Headers.AllKeys)
                {
                    serviceResult.Headers.Add(key, response.Headers[key]);
                }

                foreach (Cookie cookie in httpResponse.Cookies)
                {
                    serviceResult.Cookies.Add(cookie.Name, cookie.Value);
                }
            }

            return serviceResult;
        }

        public virtual void Get(string url, Action<HttpServiceResult> onSuccess, Action<Exception> onError)
        {
            WebRequest request = CreateRequest(new Uri(url));

            var state = new ReadState
                            {
                                OnSuccess = onSuccess,
                                OnError = onError,
                                Request = request
                            };

            Critical<InvalidOperationException>(state, () => request.BeginGetResponse(OnBeginGetResponse, state));
        }

        #endregion

        private static void OnBeginGetResponse(IAsyncResult result)
        {
            var state = (ReadState) result.AsyncState;
            WebResponse response = null;

            if (!Critical<WebException>(state, () => response = state.Request.EndGetResponse(result)))
            {
                return;
            }

            var statusCode = (int) HttpStatusCode.OK;
            var httpResponse = response as HttpWebResponse;

            if (httpResponse != null)
            {
                statusCode = (int) httpResponse.StatusCode;
            }

            if (IsError(statusCode))
            {
                if (state.OnSuccess != null)
                {
                    state.OnSuccess(new HttpServiceResult(null, null, statusCode));
                }

                return;
            }

            if (httpResponse != null)
            {
                foreach (Cookie cookie in httpResponse.Cookies)
                {
                    state.Cookies.Add(cookie.Name, cookie.Value);
                }
            }

            Stream stream = null;

            if (!Critical<ProtocolViolationException>(state, () => stream = response.GetResponseStream()))
            {
                return;
            }

            state.StatusCode = statusCode;
            state.ContentType = response.ContentType;
            state.OrignalStream = stream;
            state.BufferStream = new MemoryStream();
            state.Data = new byte[BufferLength];

            foreach (string key in response.Headers.AllKeys)
            {
                state.Headers.Add(key, response.Headers[key]);
            }

            Critical<IOException>(state, () => stream.BeginRead(state.Data, 0, BufferLength, OnBeginReadComplete, state));
        }

        private static void OnBeginReadComplete(IAsyncResult result)
        {
            var state = (ReadState) result.AsyncState;

            int bytesRead = 0;

            if (!Critical<IOException>(state, () => bytesRead = state.OrignalStream.EndRead(result)))
            {
                return;
            }

            if (bytesRead > 0)
            {
                state.BufferStream.Write(state.Data, 0, bytesRead);
                Critical<IOException>(state,
                                      () =>
                                      state.OrignalStream.BeginRead(state.Data, 0, BufferLength, OnBeginReadComplete,
                                                                    state));
                return;
            }

            state.BufferStream.Flush();

            string content = System.Text.Encoding.UTF8.GetString(state.BufferStream.ToArray());

            var serviceResult = new HttpServiceResult(content, state.ContentType, state.StatusCode);

            foreach (var pair in state.Headers)
            {
                serviceResult.Headers.Add(pair);
            }

            foreach (var pair in state.Cookies)
            {
                serviceResult.Cookies.Add(pair);
            }

            state.CloseStreams();

            if (state.OnSuccess != null)
            {
                state.OnSuccess(serviceResult);
            }
        }

        private static bool IsError(int statusCode)
        {
            return (statusCode < (int) HttpStatusCode.OK) && (statusCode >= (int) HttpStatusCode.Ambiguous);
        }

        private static bool Critical<TException>(ReadState state, Action block) where TException : Exception
        {
            try
            {
                block();
                return true;
            }
            catch (TException e)
            {
                e.LogError();
                state.CloseStreams();

                if (state.OnError != null)
                {
                    state.OnError(e);
                }
            }

            return false;
        }

        #region Nested type: ReadState

        private sealed class ReadState
        {
            public ReadState()
            {
                Headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                Cookies = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            public Action<HttpServiceResult> OnSuccess { get; set; }

            public Action<Exception> OnError { get; set; }

            public WebRequest Request { get; set; }

            public int StatusCode { get; set; }

            public string ContentType { get; set; }

            public IDictionary<string, string> Headers { get; private set; }

            public IDictionary<string, string> Cookies { get; private set; }

            public Stream OrignalStream { get; set; }

            public MemoryStream BufferStream { get; set; }

            public byte[] Data { get; set; }

            public void CloseStreams()
            {
                if (OrignalStream != null)
                {
                    OrignalStream.Close();
                }

                if (BufferStream != null)
                {
                    BufferStream.Close();
                }
            }
        }

        #endregion
    }
}