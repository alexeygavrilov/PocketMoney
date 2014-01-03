using System;
using System.Collections.Generic;

namespace PocketMoney.Util.Http
{
    public class HttpServiceResult
    {
        public HttpServiceResult(string content, string contentType, int statusCode)
        {
            Content = content;
            ContentType = contentType;
            StatusCode = statusCode;

            Headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            Cookies = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public string Content { get; private set; }

        public string ContentType { get; private set; }

        public int StatusCode { get; private set; }

        public IDictionary<string, string> Headers { get; private set; }

        public IDictionary<string, string> Cookies { get; private set; }
    }
}