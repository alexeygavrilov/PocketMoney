using System;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.IO;
using System.Web.UI;

namespace PocketMoney.FileSystem.Service
{
    public class FileRouteHandler : IRouteHandler
    {
        public const string FileURL = "file/{fileId}/{fileName}";
        public const string ThumbnailURL = "file/{fileId}/{weight}/{height}/{align}/{color}/{fileName}";

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var routeValues = requestContext.RouteData.Values;
            if (routeValues.ContainsKey("fileId"))
            {
                Guid fileId;
                if (routeValues["fileId"].ToString().TryParseGuidFromBase32Url(out fileId))
                {
                    if (routeValues.ContainsKey("weight") && routeValues.ContainsKey("height") && routeValues.ContainsKey("align"))
                    {
                        int w, h, a;
                        bool color = true;
                        int.TryParse(routeValues["weight"].ToString(), out w);
                        int.TryParse(routeValues["height"].ToString(), out h);
                        int.TryParse(routeValues["align"].ToString(), out a);
                        if (routeValues.ContainsKey("color") 
                            && !string.IsNullOrEmpty(routeValues["color"] as string)
                            && ((string)routeValues["color"]) == "0")
                                color = false;

                        if ((w > 0 && h > 0) || !color)
                            return new FileHandler(fileId, new ThumbnailOptions(w, h, (ThumbnailAlign)a, color));
                    }
                    return new FileHandler(fileId);
                }
            }
            return null;
        }

        public class FileHandler : IHttpHandler
        {
            private Guid _id;
            private ThumbnailOptions? _option;

            public FileHandler(Guid id, ThumbnailOptions? option)
            {
                _id = id;
                _option = option;
            }
            public FileHandler(Guid id) : this(id, null) { }

            #region Public Properties

            public bool IsReusable
            {
                get { return true; }
            }

            #endregion

            #region Private Methods

            private static void ConfigureResponse(HttpResponse response, DateTime expires, Encoding encoding)
            {
                response.Clear();
                response.ClearHeaders();
                response.Charset = encoding.WebName;
                response.ContentEncoding = encoding;
                response.Cache.SetCacheability(HttpCacheability.Public);
                response.Cache.SetExpires(expires.ToLocalTime());
            }
            #endregion

            #region Public Methods

            public void ProcessRequest(HttpContext context)
            {
                var fileService = ServiceLocator.Current.GetInstance<IFileService>();

                var file = fileService.Retrieve(new FileId(_id));
                if (file == null)
                {
                    context.Response.StatusCode = 404;
                    context.Response.End();
                    return;
                }

                ConfigureResponse(context.Response, DateTime.UtcNow.AddMonths(1), Encoding.UTF8);
                
                string contentDisposition = string.Empty;
                context.Response.ContentType = file.FileFormat.MimeTypes;
                context.Response.BufferOutput = true;
                if (context.Request.Browser.IsBrowser("IE") || context.Request.UserAgent.Contains("Chrome"))
                    contentDisposition = "filename=\"" + PocketMoney.Util.Encoding.Encoding.ToHexString(file.FileNameWithExtension) + "\";";
                else if (context.Request.UserAgent.Contains("Safari"))
                    contentDisposition = "filename=\"" + file.FileNameWithExtension + "\";";
                else
                    contentDisposition = "filename*=utf-8''" + HttpUtility.UrlPathEncode(file.FileNameWithExtension) + ";";

                if (file.FileFormat.IsDocument) //  || file.FileFormat.IsImage
                    contentDisposition = "attachment;" + contentDisposition;
                
                if (!string.IsNullOrEmpty(contentDisposition))
                    context.Response.AddHeader("Content-Disposition", contentDisposition);
                if (_option.HasValue)
                {
                    using (var stream = file.GetThumbnail(_option.Value))
                    {
                        stream.CopyToEx(context.Response.OutputStream);
                    }
                }
                else
                {
                    using (var stream = file.GetStream())
                    {
                        stream.CopyToEx(context.Response.OutputStream);
                    }
                }

                context.Response.End();
            }
        }

            #endregion
    }
}
