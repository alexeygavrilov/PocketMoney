using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.IO;
using PocketMoney.Util.Threading;
using PocketMoney.Data;
using System.Drawing;
using PocketMoney.Util;
using PocketMoney.Data.Wrappers;

namespace PocketMoney.FileSystem.Providers
{
    public sealed class FileSystemDevice : DeviceBase
    {
        private const int MaxPath = 260;
        private const int MaxWidthThumbnail = 2000;
        private const int MaxHeightThumbnail = 2000;
        private string _path;
        private IDeviceInfo _storgaeInfo;

        public override IDeviceInfo StorageInfo
        {
            get
            {
                if (_storgaeInfo == null)
                    _storgaeInfo = new FileSystemDeviceInfo(_path);
                return _storgaeInfo;
            }
        }

        public override void Initialize(IDeviceSettings settings)
        {
            base.Initialize(settings);
            _path = ValidatePath(Settings.Settings);

        }

        private static string ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new EmptyDataException("You must specify a device path in provider settings.", "path");

            if (!System.IO.Directory.Exists(path))
            {
                if (path.StartsWith("/", StringComparison.OrdinalIgnoreCase) ||
                    path.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
                {
                    path = path.Replace("~", string.Empty);
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path.Remove(0, 1).Replace("/", "\\"));
                    if (!System.IO.Directory.Exists(path))
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                                                                  "Couldn't find the '{0}' directory", path));
                }
                else
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                                                              "Couldn't find the '{0}' directory", path));
            }

            string testMaxPath =
                new DestinationFile(path, new WrapperFamily(Guid.NewGuid(), "Wrapper Family"), new FileId(Guid.NewGuid())).FileName;
            if ((testMaxPath.Length) >= MaxPath)
                throw new InvalidOperationException(
                    String.Format(CultureInfo.InvariantCulture,
                                  "Device path '{0}' is too long. Please make sure the device path is under {1} characters. ",
                                  path, MaxPath - testMaxPath.Length)).LogError();
            return path;
        }

        public override void Store(File file, IStreamProvider stream)
        {
            if (stream == null) throw new ArgumentNullException("stream").LogError();
            if (file == null) throw new ArgumentNullException("file").LogError();

            var location = new DestinationFile(_path, file.FileOwner, file.StrongId);
            if (!System.IO.Directory.Exists(location.FileDirectory))
                System.IO.Directory.CreateDirectory(location.FileDirectory);
            Func<Stream, Stream> transform = (s) => s;
            if (file.IsEncrypted)
                transform = (s) => { return ReadOnlyStream.Encrypt(s, file.FileOwner.Id.ToByteArray(), file.Id.ToByteArray()); };
            using (
                var f = System.IO.File.Create(location.FileName))
                //var f = new AutoLockStream(() => System.IO.File.Create(location.FileName),
                //                           ReadWriteSynchronizer<string>.Instance.CreateWriteLock(location.FileName)))
            {
                using (Stream fileStream = transform(stream.GetStream()))
                {
                    fileStream.CopyToEx(f);
                }
            }
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public override IStreamProvider Retrieve(File file)
        {
            if (file == null) throw new ArgumentNullException("file").LogError();
            Func<Stream, Stream> transform = (s) => s;
            if (file.IsEncrypted)
                transform = (s) => { return ReadOnlyStream.Decrypt(s, file.FileOwner.Id.ToByteArray(), file.Id.ToByteArray()); };
            IStreamProvider streamProvider =
                new FileStreamProvider(new DestinationFile(_path, file.FileOwner, file.StrongId).FileName, transform);
            // TODO: add the option to check file content MD5. For now I disabled it because it is being processed too slowly for much threads receiving. 
            //using (var f = streamProvider.GetStream())
            //{
            //    long md5Lo, md5Hi;
            //    f.GetMD5(out md5Lo, out md5Hi);
            //    if (file.MD5HashHI != md5Hi ||
            //        file.MD5HashLO != md5Lo)
            //        throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "Invalid MD5 hash for '{0}' file", file.FileNameWithExtension));
            //}
            return streamProvider;
        }

        public override IStreamProvider Thumbnail(File file, ThumbnailOptions option)
        {
            if (!file.FileFormat.IsImage)
                return this.Retrieve(file);

            if ((option.Width > MaxWidthThumbnail) || (option.Height > MaxHeightThumbnail))
                return this.Retrieve(file);

            DestinationFile thumbnailFile = new DestinationFile(_path, file.FileOwner, file.StrongId, option);

            if (!System.IO.File.Exists(thumbnailFile.FileName))
            {
                //using (var f = new AutoLockStream(() => System.IO.File.Create(thumbnailFile.FileName),
                //               ReadWriteSynchronizer<string>.Instance.CreateWriteLock(thumbnailFile.FileName)))
                using (var f = System.IO.File.Create(thumbnailFile.FileName))
                {
                    using (Stream fileStream = ImageUtilities.Thumbnail(file.GetStream(), option, file.FileFormat.GetImageFormat()))
                    {
                        fileStream.CopyToEx(f);
                    }
                }
            }
            return new FileStreamProvider(thumbnailFile.FileName, (s) => s);
        }

        public override void DeleteFile(File file)
        {
            if (file == null) throw new ArgumentNullException("file").LogError();
            var location = new DestinationFile(_path, file.FileOwner, file.StrongId);
            ILock lck = ReadWriteSynchronizer<string>.Instance.CreateWriteLock(location.FileName);
            lck.Acquire();
            try
            {
                if (System.IO.File.Exists(location.FileName))
                    System.IO.File.Delete(location.FileName);
            }
            finally
            {
                lck.Release();
            }
            PocketMoney.Util.IO.FileSystem.PruneDirectoriesAsync(location.FileDirectory);
        }

        /// <summary>
        /// TODO: This could be a very long and expensive operation - need to off load it to a separate thread.
        /// </summary>  
        public override void DeleteCatalog(IFamily family)
        {
            if (family == null) throw new ArgumentNullException("family").LogError();
            var location = new DestinationFolder(_path, family);
            if (System.IO.Directory.Exists(location.StorageDirectory))
                System.IO.Directory.Delete(location.StorageDirectory, true);
        }

        public override void ClearAllData()
        {
            if (System.IO.Directory.Exists(_path))
            {
                foreach (var dir in System.IO.Directory.GetDirectories(_path))
                    System.IO.Directory.Delete(dir, true);
                foreach (var file in System.IO.Directory.GetFiles(_path))
                    System.IO.File.Delete(file);
            }

        }

        /// <summary>
        /// TODO: Implement functionality
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IFamily> GetCatalogs()
        {
            var websiteIds = new List<IFamily>();
            string[] directories = System.IO.Directory.GetDirectories(_path);

            //yield return websiteIds.AddRange(from d in directories select new WebsiteId(d));
            //yield return from d in directories select new WebsiteId(d);
            websiteIds.AddRange(from d in directories select new WrapperFamily(d.FromBase32Url(), "Wrapper Family"));

            return websiteIds;
        }


        #region Nested type: DestinationFile

        /// <summary>
        /// Encapsulates building logic of file location.
        /// </summary>
        internal sealed class DestinationFile : DestinationFolder
        {
            private readonly string _fileName;
            private readonly string _path;

            public DestinationFile(string devicePath, IFamily family, FileId fileId)
                : base(devicePath, family)
            {
                fileId = fileId.Validate();
                _path = Path.Combine(StorageDirectory, HashCodeToDirectoryStructure(fileId));
                _fileName = Path.Combine(_path, fileId.Id.ToBase32Url());
            }

            public DestinationFile(string devicePath, IFamily family, FileId fileId, ThumbnailOptions option)
                : base(devicePath, family)
            {
                fileId = fileId.Validate();
                _path = Path.Combine(StorageDirectory, HashCodeToDirectoryStructure(fileId));
                _fileName = Path.Combine(_path, string.Concat(fileId.Id.ToBase32Url(), "w", option.Width.ToString(), "h", option.Height.ToString(), "a", ((int)option.Align).ToString(), "c", option.EnableColor ? "1" : "0"));
            }

            public string FileDirectory
            {
                get { return _path; }
            }

            public string FileName
            {
                get { return _fileName; }
            }

            /// <summary>
            /// This method is used to load balance number of files in eanc directory. There will be max 256 files in each directory
            /// and max 256 directories in each directory
            /// </summary>
            /// <param name="fileId"></param>
            /// <returns></returns>
            private static string HashCodeToDirectoryStructure(FileId fileId)
            {
                int hc = fileId.GetHashCode();
                if (hc == 0)
                    hc = Int32.MinValue;
                string result = hc.ToString("X", CultureInfo.InvariantCulture);
                if (result.Length < 8) result = result + "FFFFFF";
                return result;
                //char s = Path.DirectorySeparatorChar;
                //return result.Substring(0, 2) + s + result.Substring(2, 2) +s + result.Substring(4, 2) + s + result.Substring(6, 2);
            }
        }

        #endregion

        #region Nested type: DestinationFolder

        /// <summary>
        /// Encapsulates building logic of companyId's location
        /// </summary>
        internal class DestinationFolder
        {
            private readonly string _storageDirectory;

            //Since we are going to use Guid.COMB menthod = Base64 encoding wont work, because file system does not differ lower case from upper case characters. And this potentialy could
            //put files in the same folder. I use Base32 encoding instead.
            public DestinationFolder(string devicePath, IFamily family)
            {
                _storageDirectory = Path.Combine(devicePath, family.Id.ToBase32Url());
            }

            public string StorageDirectory
            {
                get { return _storageDirectory; }
            }
        }

        #endregion

    }
}