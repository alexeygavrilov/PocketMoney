using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PocketMoney.Util.ExtensionMethods;
using System.Web;
using PocketMoney.FileSystem.Service;

namespace PocketMoney.FileSystem
{
    public static class FileExtensions
    {
        public static File To(this IFile file)
        {
            if (file == null) return null;
            if (file is File)
                return (File)file;
            else
            {
                var fileService = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<IFileService>();
                return fileService.Retrieve(new FileId(file.Id));
            }
        }

        public static string DownloadURL(this IFile file)
        {
            return FileService.VirtualPathToAbsolute(string.Format("~/file/{0}/{1}", file.Id.ToBase32Url(), file.FileNameWithExtension));
        }

        public static string ThumbnailURL(this IFile file, ThumbnailOptions options)
        {
            return FileService.VirtualPathToAbsolute(string.Format("~/file/{0}/{1}/{2}/{3}/{4}/{5}", file.Id.ToBase32Url(), options.Width, options.Height, (int)options.Align, options.EnableColor ? "1" : "0", file.FileNameWithExtension));
        }

    }
}
