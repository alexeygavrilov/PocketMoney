// -----------------------------------------------------------------------
// <copyright file="ExceptionFactory.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace PocketMoney.Util.Logging
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Web;

    using Castle.Core.Logging;

    [Serializable]
    public abstract class LoggerBase : LevelFilteredLogger
    {
        protected LoggerBase(string name)
            : base(name)
        {
        }

        protected LoggerBase(string name, LoggerLevel logLevel)
            : base(name, logLevel)
        {
        }

        protected static string GetStackTrace()
        {
            return System.Environment.StackTrace;

        }

        protected static string GetResponse()
        {
            var sb = new StringBuilder();
            if (HttpContext.Current != null)
            {
                var httpContext = HttpContext.Current;
                var r = httpContext.Response;
                sb.AppendFormat("ResponseStatus:{0}", r.Status).AppendLine();
                sb.AppendFormat("ResponseStatusCode:{0}", r.StatusCode).AppendLine();
                sb.AppendFormat("ResponseStatusDescription:{0}", r.StatusDescription).AppendLine();
                sb.AppendFormat("ResponseContentType:{0}", r.ContentType).AppendLine();
                sb.AppendFormat("ResponseExpirese:{0}", r.Expires).AppendLine();
                sb.AppendFormat("ResponseRedirectLocation:{0}", r.RedirectLocation).AppendLine();
                sb.AppendFormat("ResponseData:{0}", (object)GetResponseData(r)).AppendLine();
            }
            return sb.ToString();
        }

        protected static string GetResponseData(HttpResponse httpResponse)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < httpResponse.Headers.Count; i++)
            {
                sb.AppendFormat("Header={0}, Value={1}", httpResponse.Headers.Keys[i], httpResponse.Headers[i]);
            }

            return sb.ToString();
        }

        protected static string GetRequest()
        {
            var sb = new StringBuilder();
            if (HttpContext.Current != null)
            {
                var httpContext = HttpContext.Current;
                sb.AppendFormat("Request:{0}", httpContext.Request.RawUrl).AppendLine();
                sb.AppendFormat("RequestType:{0}", httpContext.Request.RequestType).AppendLine();
                sb.AppendFormat("Method:{0}", httpContext.Request.HttpMethod).AppendLine();
                sb.AppendFormat("UserHostAddress:{0}", httpContext.Request.UserHostAddress).AppendLine();
                sb.AppendFormat("User:{0}", httpContext.User.Identity.Name).AppendLine();
                sb.AppendFormat("RequestData:{0}", GetRequestData(httpContext)).AppendLine();
            }
            return sb.ToString();
        }

        protected static string GetRequestData(HttpContext context)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < context.Request.QueryString.Count; i++)
            {
                sb.AppendFormat("QueryString={0}, Value={1}", context.Request.QueryString.Keys[i], context.Request.QueryString[i]).AppendLine();
            }

            for (int i = 0; i < context.Request.Form.Count; i++)
            {
                sb.AppendFormat("Form={0}, Value={1}", context.Request.Form.Keys[i], context.Request.Form[i]).AppendLine();
            }
            for (int i = 0; i < context.Request.Headers.Count; i++)
            {
                sb.AppendFormat("Header={0}, Value={1}", context.Request.Headers.Keys[i], context.Request.Headers[i]).AppendLine();
            }
            return sb.ToString();
        }

        protected static Func<IIdentity, string> ClaimsFetcher;
        private static string GetClaims(IIdentity idenityt)
        {
            if (ClaimsFetcher == null) return "No claim fetcher delegate is set";
            return ClaimsFetcher(idenityt);
        }
        protected static string GetUser()
        {
            var sb = new StringBuilder();
            if (HttpContext.Current != null)
            {
                sb.AppendLine("HttpContext Identity").AppendLine();
                var httpContext = HttpContext.Current;
                sb.AppendFormat("HttpContextUserIdentity:{0}", httpContext.User.Identity).AppendLine();
                sb.AppendFormat("HttpContextUserAuthenticated:{0}", httpContext.User.Identity.IsAuthenticated).AppendLine();
                sb.AppendFormat("HttpContextUserClaims:{0}", GetClaims(httpContext.User.Identity)).AppendLine();
            }
            if (Thread.CurrentPrincipal != null)
            {
                sb.AppendLine("Thread Identity").AppendLine();
                sb.AppendFormat("ThreadUserIdentity:{0}", Thread.CurrentPrincipal.Identity).AppendLine();
                sb.AppendFormat("ThreadUserAuthenticated:{0}", Thread.CurrentPrincipal.Identity.IsAuthenticated).AppendLine();
              //  sb.AppendFormat("ThreadUserClaims:{0}", this.GetClaims(Thread.CurrentPrincipal.Identity)).AppendLine();

            }
            return sb.ToString();
        }

        protected static string GetExceptionTypeStack(Exception e)
        {
                if (e.InnerException != null)
                {
                    var message = new StringBuilder();
                    message.AppendLine(GetExceptionTypeStack(e.InnerException));
                    message.AppendLine("   " + e.GetType());
                    return (message.ToString());
                }
                else
                {
                    return "   " + e.GetType();
                }
        }

        protected static string GetExceptionMessageStack(Exception e)
        {
            if (e.InnerException != null)
            {
                var message = new StringBuilder();
                message.AppendLine(GetExceptionMessageStack(e.InnerException));
                message.AppendLine("   " + e.Message);
                return (message.ToString());
            }
            else
            {
                return "   " + e.Message;
            }
        }

        protected static string GetExceptionCallStack(Exception e)
        {
            if (e.InnerException != null)
            {
                var message = new StringBuilder();
                message.AppendLine(GetExceptionCallStack(e.InnerException));
                message.AppendLine("--- Next Call Stack:");
                message.AppendLine(e.StackTrace);
                return (message.ToString());
            }
            else
            {
                return e.StackTrace;
            }
        }

        protected static string GetExeption(Exception exception)
        {
            if (exception != null)
            {
                var error = new StringBuilder();

                error.AppendLine("Date:              " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                error.AppendLine("ServerOS:                " + Environment.OSVersion.ToString());
                error.AppendLine("Culture:           " + CultureInfo.CurrentCulture.Name);
                error.AppendLine("App up time:       " + (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString());


                error.AppendLine("");

                error.AppendLine("Exception classes:   ");
                error.Append(GetExceptionTypeStack(exception));
                error.AppendLine("");
                error.AppendLine("Exception messages: ");
                error.Append(GetExceptionMessageStack(exception));

                error.AppendLine("");
                error.AppendLine("Stack Traces:");
                error.Append(GetExceptionCallStack(exception));
                error.AppendLine("");
                error.AppendLine("Loaded Modules:");
                Process thisProcess = Process.GetCurrentProcess();
                foreach (ProcessModule module in thisProcess.Modules)
                    error.AppendLine(module.FileName + " " + module.FileVersionInfo.FileVersion);
                return error.ToString();
            }
            else return "Unknown error";
        }

        protected static string GetThread()
        {
            return Thread.CurrentThread.Name;
        }
    }
}
