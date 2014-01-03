using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using PocketMoney.Util;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.IO;

namespace PocketMoney.FileSystem
{

    public static class FileFactory
    {
        /// <summary>
        /// Status code for image upload if image is corrupted or invalid
        /// </summary>
        public const int InvalidImageStatus = 512;

        /// <summary>
        /// Status message for image upload if image is corrupted or invalid
        /// </summary>
        private const string InvalidImageMessage = "Invalid image";


        public static IFile FromHttpPostedFile(HttpPostedFile postedFile)
        {
            return new File
                       {
                           FileFormat =
                               FileFormat.Detect(postedFile.ContentType, Path.GetExtension(postedFile.FileName)),
                           DateCreated = Clock.UtcNow(),
                           Extension = Path.GetExtension(postedFile.FileName),
                           FileNameWithExtension = Path.GetFileName(postedFile.FileName),
                           SizeFunc = () => postedFile.ContentLength,
                           Provider = new StreamProvider(() =>
                                                             {
                                                                 var result = new MemoryStream();
                                                                 postedFile.InputStream.CopyToEx(result);
                                                                 return result;
                                                             })
                       };
        }

        public static IFile FromHttpPostedFile(HttpPostedFileBase postedFile)
        {
            return new File
                       {
                           FileFormat =
                               FileFormat.Detect(postedFile.ContentType, Path.GetExtension(postedFile.FileName)),
                           DateCreated = Clock.UtcNow(),
                           Extension = Path.GetExtension(postedFile.FileName),
                           FileNameWithExtension = Path.GetFileName(postedFile.FileName),
                           SizeFunc = () => postedFile.ContentLength,
                           Provider = new StreamProvider(() =>
                                                             {
                                                                 var result = new MemoryStream();
                                                                 postedFile.InputStream.CopyToEx(result);
                                                                 return result;
                                                             })
                       };
        }

        public static IFile FromHttpPostedImage(HttpPostedFileBase postedImage)
        {
            var format = FileFormat.Detect(postedImage.ContentType, Path.GetExtension(postedImage.FileName));
            if(!format.IsImage)
                throw new PocketMoney.Data.InvalidDataException(InvalidImageMessage, "image", InvalidImageStatus);
            return new File
            {
                FileFormat = format,
                DateCreated = Clock.UtcNow(),
                Extension = Path.GetExtension(postedImage.FileName),
                FileNameWithExtension = Path.GetFileName(postedImage.FileName),
                SizeFunc = () => postedImage.ContentLength,
                Provider = new StreamProvider(() =>
                {
                    var result = new MemoryStream();
                    postedImage.InputStream.CopyToEx(result);
                    try
                    {
                        var uploadedImage = Image.FromStream(result, true, true);
                        result.Seek(0, SeekOrigin.Begin);
                    }
                    catch
                    {
                        throw new PocketMoney.Data.InvalidDataException(InvalidImageMessage, "image", InvalidImageStatus);
                    }
                    return result;
                })
            };
        }

        public static IFile FromFile(string fullPathToFile, FileFormat mimeType)
        {
            if (fullPathToFile == null) throw new ArgumentNullException("fullPathToFile").LogError();
            if (mimeType == null) throw new ArgumentNullException("mimeType").LogError();
            if (!System.IO.File.Exists(fullPathToFile))
                throw new FileNotFoundException("fullPathToFile", fullPathToFile).LogError();
            long size = new FileInfo(fullPathToFile).Length;
            Func<Stream> streamFunc = () => System.IO.File.OpenRead(fullPathToFile);
            //Func<Stream> streamFunc = () => new AutoLockStream(() => System.IO.File.OpenRead(fullPathToFile),
            //                                                   ReadWriteSynchronizer<string>.Instance.
            //                                                       CreateReadLock(fullPathToFile));
            return new File
                       {
                           FileFormat = mimeType,
                           DateCreated = Clock.UtcNow(),
                           Extension = Path.GetExtension(fullPathToFile),
                           FileNameWithExtension = Path.GetFileName(fullPathToFile),
                           SizeFunc = () => size,
                           Provider = new StreamProvider(streamFunc)
                       };
        }

        public static IFile FromFile(string fileName, IStreamProvider file, FileFormat mimeType)
        {
            if (String.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName").LogError();
            if (mimeType == null) throw new ArgumentNullException("mimeType").LogError();
            return new File
                       {
                           FileFormat = mimeType,
                           DateCreated = Clock.UtcNow(),
                           Extension = Path.GetExtension(fileName),
                           FileNameWithExtension = Path.GetFileName(fileName),
                           SizeFunc = delegate
                                          {
                                              using (Stream s = file.GetStream())
                                              {
                                                  return s.Length;
                                              }
                                          },
                           Provider = new StreamProvider(file.GetStream)
                       };
        }

        public static IFile FromString(string fileName, string plainText, FileFormat fileFormat)
        {
            if (String.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
            if (plainText == null) throw new ArgumentNullException("plainText").LogError();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return new File
                       {
                           Extension = Path.GetExtension(fileName),
                           FileFormat = fileFormat,
                           FileNameWithExtension = fileName,
                           DateCreated = Clock.UtcNow(),
                           SizeFunc = () => bytes.Length,
                           Provider = new StreamProvider(() => new MemoryStream(bytes))
                       };
        }

        public static IFile FromArray(string fileName, byte[] array, string contentType)
        {
            if (String.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
            if (array == null) throw new ArgumentNullException("array").LogError();
            if (array.Length == 0) throw new ArgumentNullException("array is empty").LogError();
            string extension = Path.GetExtension(fileName);
            return new File
            {
                Extension = extension,
                FileFormat = FileFormat.Detect(contentType, extension),
                FileNameWithExtension = fileName,
                DateCreated = Clock.UtcNow(),
                SizeFunc = () => array.Length,
                Provider = new StreamProvider(() => new MemoryStream(array))
            };
        }

        public static IFile FromEmailAttachment(Attachment attachment)
        {
            return new File
                       {
                           FileFormat = FileFormat.Eml,
                           FileNameWithExtension = attachment.Name,
                           DateCreated = Clock.UtcNow(),
                           SizeFunc = () => attachment.ContentStream.Length,
                           Provider = new StreamProvider(() =>
                                                             {
                                                                 var result = new MemoryStream();
                                                                 attachment.ContentStream.CopyToEx(result);
                                                                 return result;
                                                             })
                       };
        }

        public static IFile FromPlainText(string fileName, string plainText)
        {
            return FromString(fileName, plainText, FileFormat.Txt);
        }

        public static string GetMessageBody(MailMessage mailMessage)
        {
            if (!mailMessage.IsBodyHtml && !String.IsNullOrWhiteSpace(mailMessage.Body.Trim()))
                return mailMessage.Body;
            var part =
                mailMessage.AlternateViews.FirstOrDefault(
                    x => FileFormat.Txt.MimeTypes.Contains(x.ContentType.MediaType));
            if (part != null) return System.Text.Encoding.UTF8.GetString(part.ContentStream.ToArray());
            part = mailMessage.AlternateViews.FirstOrDefault(
                    x => FileFormat.Html.MimeTypes.Contains(x.ContentType.MediaType));
            return part != null ? System.Text.Encoding.UTF8.GetString(part.ContentStream.ToArray()) : mailMessage.Body;
        }

        public static IFile FromMessageAlternateView(string fileNameWithExtension, AlternateView mimeType, FileFormat fileFormat)
        {
            return new File
            {
                FileFormat = fileFormat,
                FileNameWithExtension = fileNameWithExtension,
                DateCreated = Clock.UtcNow(),
                SizeFunc = () => mimeType.ContentStream.Length,
                Provider = new StreamProvider(() =>
                {
                    var result = new MemoryStream();
                    mimeType.ContentStream.CopyToEx(result);
                    return result;
                })
            };
        }

        public static IFile FromPlainText(string plainText)
        {
            return FromString("file.txt", plainText, FileFormat.Txt);
        }

        public static IFile FromHtml(string fileName, string htmlText)
        {
            return FromString(fileName, htmlText, FileFormat.Html);
        }

        public static IFile FromHtml(string htmlText)
        {
            return FromString("file.html", htmlText, FileFormat.Html);
        }

        public static IFile FromRichText(string fileName, string richText)
        {
            return FromString(fileName, richText, FileFormat.Rtf);
        }

        public static IFile FromRichText(string richText)
        {
            return FromString("file.rtf", richText, FileFormat.Rtf);
        }

        public static IFile FromXml(string fileName, string xmlText)
        {
            return FromString(fileName, xmlText, FileFormat.Xml);
        }

        public static IFile FromXml(string xmlText)
        {
            return FromString("file.xml", xmlText, FileFormat.Xml);
        }

        public static IFile FromImage(string fileName, Bitmap image, ImageFormat format)
        {
            if (String.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
            if (image == null) throw new ArgumentNullException("image").LogError();
            if (image.Size.IsEmpty) throw new ArgumentNullException("image is empty").LogError();
            string extension = Path.GetExtension(fileName);
            using (Stream stream = new MemoryStream())
            {
                image.Save(stream, format);
                return new File
                {
                    Extension = extension,
                    FileFormat = FileFormat.Detect("image/" + format.ToString(), extension),
                    FileNameWithExtension = fileName,
                    DateCreated = Clock.UtcNow(),
                    SizeFunc = () => stream.Length,
                    Provider = new StreamProvider(() => new MemoryStream(stream.ToArray()))
                };
            }
        }


        #region Nested type: File

        private class File : IFile, IFileFormatInfo
        {
            #region Implementation of IStreamProvider

            public Stream GetStream()
            {
                if (Provider != null)
                    return Provider.GetStream();
                throw new InvalidOperationException("Stream provider is not specifed!").LogError();
            }

            public byte[] Content
            {
                get { return this.GetStream().ToArray(); }
                set
                {
                    if (Provider != null)
                        Provider.Content = value;
                }
            }
            #endregion

            #region Implementation of IFile

            private long _size;
            internal StreamProvider Provider { get; set; }
            internal Func<long> SizeFunc { get; set; }
            public string FileNameWithExtension { get; set; }
            public string Extension { get; set; }

            public long Size
            {
                get
                {
                    try
                    {
                        return _size = SizeFunc();
                    }
                    catch (ObjectDisposedException ex)
                    {
                        ex.LogDebug();
                        return _size;
                    }
                }
            }

            // public string MimeTypes { get; set; }
            public string MetaInfo { get; set; }
            public DateTime? DateCreated { get; set; }

            public Guid Id
            {
                get { return Guid.NewGuid(); }
            }

            #endregion

            #region Implementation of IFileFormatInfo

            public FileFormat FileFormat { get; set; }

            #endregion
        }

        #endregion

        #region Nested type: StreamProvider

        private class StreamProvider : IStreamProvider
        {
            private Func<Stream> _inputStreamFactory;

            public StreamProvider(Func<Stream> inputStreamFactory)
            {
                if (inputStreamFactory == null) throw new ArgumentNullException("inputStreamFactory").LogError();
                _inputStreamFactory = inputStreamFactory;
            }

            #region Implementation of IStreamProvider

            public Stream GetStream()
            {
                return _inputStreamFactory();
            }

            public byte[] Content
            {
                get { return _inputStreamFactory().ToArray(); }
                set
                {
                    throw new NotImplementedException();
                }
            }
            #endregion
        }

        #endregion

        public static IFile FromStream(string fileName, Stream inputStream, string contentType)
        {
            return new File
            {
                FileFormat =
                    FileFormat.Detect(contentType, Path.GetExtension(fileName)),
                DateCreated = Clock.UtcNow(),
                Extension = Path.GetExtension(fileName),
                FileNameWithExtension = Path.GetFileName(fileName),
                SizeFunc = () => inputStream.Length,
                Provider = new StreamProvider(() =>
                {
                    var result = new MemoryStream();
                    inputStream.CopyToEx(result);
                    return result;
                })
            };
        }
    }
}