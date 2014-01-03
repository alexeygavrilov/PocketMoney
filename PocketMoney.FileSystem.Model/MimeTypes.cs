// -----------------------------------------------------------------------
// <copyright file="ContentType.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Net.Mime;
using System.Text;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.FileSystem
{
    public static class MimeTypes
    {
        public static string[] Unknown = new[] {MediaTypeNames.Application.Octet, "application/x-download"};
        public static string[] Pdf = new[] {MediaTypeNames.Application.Pdf};
        public static string[] Rtf = new[] {MediaTypeNames.Application.Rtf};
        public static string[] Soap = new[] {MediaTypeNames.Application.Soap};
        public static string[] Zip = new[] {MediaTypeNames.Application.Zip};
        public static string[] Gif = new[] {"image/gif", "image/x-xbitmap", "image/gi_"};

        public static string[] Jpeg = new[]
                                          {
                                              "image/jpeg", "image/jpg", "image/jpe_", "image/pjpeg",
                                              "image/vnd.swiftview-jpeg"
                                          };

        public static string[] Bmp = new[]
                                         {
                                             "image/bmp", "image/x-bmp", "image/x-bitmap", "image/x-xbitmap",
                                             "image/x-win-bitmap", "image/x-windows-bmp", "image/ms-bmp", "image/x-ms-bmp",
                                             "application/bmp", "application/x-bmp", "application/x-win-bitmap"
                                         };

        public static string[] Emf = new[]
                                         {
                                             "application/emf", "application/x-emf", "image/x-emf", "image/x-mgx-emf",
                                             "image/x-xbitmap"
                                         };

        public static string[] Ico = new[]
                                         {
                                             "image/ico", "image/x-icon", "application/ico", "application/x-ico",
                                             "application/x-win-bitmap", "image/x-win-bitmap"
                                         };

        public static string[] Exif = new[] {"image/exif"};
        public static string[] Png = new[] {"image/png", "application/png", "application/x-png"};

        public static string[] Wmf = new[]
                                         {
                                             "application/x-msmetafile", "application/wmf", "application/x-wmf",
                                             "image/wmf", "image/x-wmf", "image/x-win-metafile",
                                             "zz-application/zz-winassoc-wmf"
                                         };

        public static string[] Dcx = new[]
                                         {
                                             "image/dcx", "image/pcx", "image/x-dcx", "image/x-pc-paintbrush",
                                             "image/vnd.swiftview-pcx"
                                         };

        public static string[] Pcx = new[]
                                         {
                                             "application/pcx", "application/x-pcx", "image/pcx", "image/x-pc-paintbrush",
                                             "image/x-pcx", "zz-application/zz-winassoc-pcx"
                                         };

        public static string[] MSWord = new[] {"application/msword"};

        public static string[] MSWord1 = new[]
                                             {"application/vnd.openxmlformats-officedocument.wordprocessingml.document"};

        public static string[] MSWordMacro = new[] {"application/vnd.ms-word.document.macroEnabled.12"};
        public static string[] MSExcel = new[] {"application/vnd.ms-excel"};
        public static string[] MSExcelBinary = new[] {"application/vnd.ms-excel.sheet.binary.macroEnabled.12"};
        public static string[] MSExcelMacro = new[] {"application/vnd.ms-excel.sheet.macroEnabled.12"};
        public static string[] MSExcel1 = new[] {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"};

        public static string[] MSVisio = new[]
                                             {
                                                 "application/visio", "application/x-visio", "application/vnd.visio",
                                                 "application/visio.drawing", "application/vsd", "application/x-vsd",
                                                 "image/x-vsd", "zz-application/zz-winassoc-vsd"
                                             };

        public static string[] MSPowerPoint = new[] {"application/vnd.ms-powerpoint"};
        public static string[] MSPowerPointMacro = new[] {"application/vnd.ms-powerpoint.presentation.macroEnabled.12"};

        public static string[] MSPowerPoint1 = new[]
                                                   {
                                                       "application/vnd.openxmlformats-officedocument.presentationml.presentation"
                                                   };

        public static string[] MSPowerPointShow = new[]
                                                      {
                                                          "application/vnd.openxmlformats-officedocument.presentationml.slideshow"
                                                      };

        public static string[] MSPowerPointShowMacro = new[] {"application/vnd.ms-powerpoint.slideshow.macroEnabled.12"};
        public static string[] MSInfoPath = new[] {"application/vnd.ms-infopath"};
        public static string[] Email_Rfc822 = new[] {"message/rfc822"};
        public static string[] PostScript = new[] {"application/postscript"};
        public static string[] Pcl = new[] {"cation/vnd.hp-pcl"};
        public static string[] WavOrWma = new[] {"audio/x-wav", "audio/wav"};
        public static string[] Mp3 = new[] {"audio/mpeg"};
        public static string[] Xps = new[] {"application/vnd.ms-xpsdocument"};
        public static string[] Avi = new[] {"video/x-msvideo"};
        public static string[] Xaml = new[] {"application/xaml+xml"};

        public static string[] Csv = new[]
                                         {
                                             "text/comma-separated-values", "text/csv", "application/csv",
                                             "application/excel", "application/vnd.ms-excel", "application/vnd.msexcel",
                                             "text/anytext"
                                         };

        public static string[] XFdl = new[]
                                          {
                                              "application/uwi_form", "application/vnd.ufdl", "application/vnd.xfdl",
                                              "application/x-xfdl"
                                          };

        public static string[] Tiff = new[]
                                          {
                                              MediaTypeNames.Image.Tiff, "image/x-tif", "image/tiff", "image/x-tiff",
                                              "application/tif", "application/x-tif", "application/tiff",
                                              "application/x-tiff"
                                          };

        public static string[] Html = new[] {MediaTypeNames.Text.Html, "application/xhtml+xml"};
        public static string[] Plain = new[] {MediaTypeNames.Text.Plain};
        public static string[] RichText = new[] {MediaTypeNames.Text.RichText};
        public static string[] Xml = new[] {MediaTypeNames.Text.Xml, "application/xml"};
        public static string[] Ics = new[] { "text/calendar" };
        public static string[] Json = new[] {"application/json", "text/json"};

        public static string[] Combine(params string[][] values)
        {
            if (values == null) throw new ArgumentNullException("values").LogError();
            if (values.Length == 0) throw new ArgumentNullException("values").LogError();
            return values.SelectMany(t => t).Distinct().ToList().ToArray();
        }

        public static string All(this string[] values)
        {
            if (values == null) return null;
            if (values.Length == 0) return string.Empty;
            var sb = new StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                sb.Append(values[i]);
                if ((i + 1) != values.Length)
                    sb.Append(",");
            }
            return sb.ToString();
        }
    }
}