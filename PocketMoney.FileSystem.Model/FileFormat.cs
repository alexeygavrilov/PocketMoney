using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Imaging;

namespace PocketMoney.FileSystem
{
    public interface IFileFormatInfo
    {
        FileFormat FileFormat { get; }
    }

    public class FileFormat : IEquatable<FileFormat>
    {
        public static FileFormat Pdf = new FileFormat(FileSystem.MimeTypes.Pdf, true, true, FileFormatEnum.Pdf,
                                                      ".pdf");

        public static FileFormat Unknown = new FileFormat(FileSystem.MimeTypes.Unknown, false, true,
                                                          FileFormatEnum.Unknown, ".");

        public static FileFormat Tiff = new FileFormat(FileSystem.MimeTypes.Tiff, true, true, FileFormatEnum.Tif,
                                                       ".tif", ".tiff");

        public static FileFormat Jpeg = new FileFormat(FileSystem.MimeTypes.Jpeg, false, true,
                                                       FileFormatEnum.Jpeg, ".jpg", ".jpeg");

        public static FileFormat Png = new FileFormat(FileSystem.MimeTypes.Png, false, true, FileFormatEnum.Png,
                                                      ".png");

        public static FileFormat Gif = new FileFormat(FileSystem.MimeTypes.Gif, true, true, FileFormatEnum.Gif,
                                                      ".gif", ".giff");

        public static FileFormat Txt = new FileFormat(FileSystem.MimeTypes.Plain, false, false,
                                                      FileFormatEnum.Txt, ".txt", ".text");

        public static FileFormat Html = new FileFormat(FileSystem.MimeTypes.Html, false, false,
                                                       FileFormatEnum.Html, ".htm", ".html");

        public static FileFormat Rtf = new FileFormat(FileSystem.MimeTypes.RichText, false, false,
                                                      FileFormatEnum.Rtf, ".rtf");

        public static FileFormat Xml = new FileFormat(FileSystem.MimeTypes.Xml, false, false, FileFormatEnum.Xml,
                                                      ".xml");

        public static FileFormat Eml = new FileFormat(FileSystem.MimeTypes.Email_Rfc822, false, false,
                                                      FileFormatEnum.Eml, ".eml");

        public static FileFormat Mp3 = new FileFormat(FileSystem.MimeTypes.Mp3, false, true,
                                              FileFormatEnum.Mp3, ".mp3");

        public static FileFormat Wav = new FileFormat(FileSystem.MimeTypes.WavOrWma, false, true,
                                      FileFormatEnum.Wav, ".wav");

        public static FileFormat Wma = new FileFormat(FileSystem.MimeTypes.WavOrWma, false, true,
                              FileFormatEnum.Wma, ".wma");

        public static FileFormat MSWord_doc = new FileFormat(FileSystem.MimeTypes.MSWord, true, true,
                                                             FileFormatEnum.Doc, ".doc");

        public static FileFormat MSWord_docx =
            new FileFormat(
                FileSystem.MimeTypes.Combine(FileSystem.MimeTypes.MSWord1,
                                                     FileSystem.MimeTypes.MSWord), true, true,
                FileFormatEnum.Docx, ".docx");

        public static FileFormat MSExcel_xls = new FileFormat(FileSystem.MimeTypes.MSExcel, true, true,
                                                              FileFormatEnum.Xls, ".xls");

        public static FileFormat MSExcel_xlsx =
            new FileFormat(
                FileSystem.MimeTypes.Combine(FileSystem.MimeTypes.MSExcel,
                                                     FileSystem.MimeTypes.MSExcel1), true, true,
                FileFormatEnum.Xlsx, ".xlsx");

        public static FileFormat MSOutlook_ics = new FileFormat(FileSystem.MimeTypes.Ics, false, false, FileFormatEnum.Ics, ".ics");


        #region class members

        private static List<FileFormat> _allFormats;
        private readonly FileFormatEnum _format;
        private readonly bool _isBinary;

        private readonly string[] _knownFileExtensions;
        private readonly string[] _mimeTypes;
        private readonly bool _supportsMultiplePages;

        private FileFormat(string[] mimeTypes, bool supportsMultiplePages, bool isBinary, FileFormatEnum format,
                           params string[] knownFileExtensions)
        {
            _mimeTypes = mimeTypes;
            _format = format;
            _isBinary = isBinary;
            _supportsMultiplePages = supportsMultiplePages;
            _knownFileExtensions = knownFileExtensions;
            if (_allFormats == null)
                lock (GetType())
                {
                    if (_allFormats == null)
                        _allFormats = new List<FileFormat>();
                }
            _allFormats.Add(this);
        }

        public static IEnumerable<FileFormat> All
        {
            get { return _allFormats.ToArray(); }
        }

        private IEnumerable<string> MimeTypesInternal
        {
            get { return _mimeTypes; }
        }

        public string MimeTypes
        {
            get { return _mimeTypes.All(); }
        }

        public string[] KnownFileExtensions
        {
            get { return _knownFileExtensions; }
        }

        public bool SupportsMultiplePages
        {
            get { return _supportsMultiplePages; }
        }

        public bool IsBinary
        {
            get { return _isBinary; }
        }

        public bool IsImage
        {
            get
            {
                return CheckIsImage(Format);
            }
        }
        
        public bool IsDocument
        {
            get
            {
                return CheckIsDoc(Format);
            }
        }

        public static Boolean CheckIsImage(FileFormatEnum formatEnum)
        {
            bool isImage;
            switch (formatEnum)
            {
                case FileFormatEnum.Tif:
                case FileFormatEnum.Jpeg:
                case FileFormatEnum.Png:
                case FileFormatEnum.Gif:
                    isImage = true;
                    break;
                default:
                    isImage = false;
                    break;
            }
            return isImage;
        }

        public static Boolean CheckIsDoc(FileFormatEnum formatEnum)
        {
            bool isDoc;
            switch (formatEnum)
            {
                case FileFormatEnum.Doc:
                case FileFormatEnum.Docx:
                case FileFormatEnum.Eml:
                case FileFormatEnum.Html:
                case FileFormatEnum.Pdf:
                case FileFormatEnum.Rtf:
                case FileFormatEnum.Txt:
                case FileFormatEnum.Xls:
                case FileFormatEnum.Xlsx:
                case FileFormatEnum.Xml:
                    isDoc = true;
                    break;
                default:
                    isDoc = false;
                    break;
            }
            return isDoc;
        }

        public ImageFormat GetImageFormat()
        {
            switch (_format)
            {
                case FileFormatEnum.Tif:
                    return ImageFormat.Tiff;
                case FileFormatEnum.Jpeg:
                    return ImageFormat.Jpeg;
                case FileFormatEnum.Png:
                    return ImageFormat.Png;
                case FileFormatEnum.Gif:
                    return ImageFormat.Gif;
                default:
                    throw new ArgumentException("Unknown image format for this file");
            }
        }

        public FileFormatEnum Format
        {
            get { return _format; }
        }

        public bool Equals(FileFormat other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._format, _format);
        }

        public static FileFormat Find(FileFormatEnum fileFormatEnum)
        {
            return _allFormats.Single(f => f.Format == fileFormatEnum);
        }

        public static IList<FileFormat> Find(string mimeType)
        {
            IEnumerable<string> parts = mimeType.Split(',').Select(t => t.Trim());
            var result = new List<FileFormat>();
            foreach (string part in parts)
            {
                string part1 = part;
                result.AddRange(
                    _allFormats.Where(
                        f => f.MimeTypesInternal.Contains(part1, StringComparer.InvariantCultureIgnoreCase)).Distinct().
                        Select(t => t));
            }
            return result.Distinct().Select(t => t).ToList();
        }

        public override string ToString()
        {
            return _format.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(FileFormat)) return false;
            return Equals((FileFormat)obj);
        }

        public override int GetHashCode()
        {
            return _format.GetHashCode();
        }

        public static bool operator ==(FileFormat left, FileFormat right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileFormat left, FileFormat right)
        {
            return !Equals(left, right);
        }

        #endregion

        public static FileFormat Detect(string contentType, string fileExtension)
        {
            IList<FileFormat> results = Find(contentType);
            //if (results.Count == 0)            
            //    throw new ArgumentException(String.Format("File '{0}' with content '{1}' is not supported!",fileExtension,contentType)).LogFatal();            
            if ((results.Count == 1) && (results[0] != Unknown))
                return results[0];
            FileFormat found = results.Count > 1
                                   ? results.FirstOrDefault(
                                       r =>
                                       r.KnownFileExtensions.Contains(fileExtension,
                                                                      StringComparer.InvariantCultureIgnoreCase))
                                   : All.FirstOrDefault(
                                       x =>
                                       x.KnownFileExtensions.Contains(fileExtension,
                                                                      StringComparer.InvariantCultureIgnoreCase));
            return found ?? Unknown;
        }
    }

    public enum FileFormatEnum
    {
        Unknown = 0,
        Pdf = 10,
        Rtf = 20,
        Tif = 30,
        Jpeg = 40,
        Png = 50,
        Gif = 60,
        Txt = 70,
        Html = 80,
        Doc = 90,
        Docx = 91,
        Xls = 100,
        Xlsx = 101,
        Xml = 110,
        Eml = 120,
        Ics = 130,
        Mp3 = 140,
        Wav = 150,
        Wma = 160
    }
}