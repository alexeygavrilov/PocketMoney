using System;
using System.Collections.Generic;
using System.IO;
using PocketMoney.Util;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.IO;
using PocketMoney.Util.Threading;
using PocketMoney.FileSystem;

namespace PocketMoney.FileSystem.Service
{
    public class TempFileService : ITempFileService
    {
        public const string APPFOLDER = "GAZIBOO_TEMP";
        private static readonly object Locker = new object();
        private static bool _isFirstInsance = true;

        public TempFileService()
        {
            if (_isFirstInsance) //Peformance optimization, locks are expensive
            {
                lock (Locker)
                {
                    if (_isFirstInsance) //still first instance?
                    {
                        _isFirstInsance = false;
                        CleanUpFolder();
                    }
                }
            }
        }

        private void CleanUpFolder()
        {
            try
            {
                if (Directory.Exists(GetTempFolderRoot()))
                    Directory.Delete(GetTempFolderRoot(), true);
            }
            catch (Exception e)
            {
                e.LogDebug();
                //TODO: Handle the error!
            }
        }

        #region Implementation of ITempFileManager

        public ITempFolder CreateTempFolder()
        {
            return new TempFolder(GetTempFolderRoot());
        }

        public ITempFile CreateTempFile(ITempFolder folder, string fileName, FileFormat format)
        {
            if (folder == null) throw new ArgumentNullException("folder").LogError();
            if (format == null) throw new ArgumentNullException("format");
            if (String.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName").LogError();
            return new TempFile(folder, fileName, format.KnownFileExtensions[0], format.MimeTypes, format);
        }

        private string GetTempFolderRoot()
        {
            try
            {
                return this.GetRuntimeSpecificTempFolder();
            }
            catch
            {
                return GetWindowsTempFolder();
            }
        }

        protected virtual string GetRuntimeSpecificTempFolder()
        {
            return GetWindowsTempFolder();
        }

        private static string GetWindowsTempFolder()
        {
            return Path.Combine(Path.GetTempPath(), APPFOLDER);
        }

        #region Nested type: TempFile

        private class TempFile : Disposable, ITempFile
        {
            private readonly string _fileExtension;
            private readonly FileFormat _fileFormat;
            private readonly string _fileName;
            private readonly string _mimeTypes;
            private readonly string fakeName;
            private ITempFolder _folder;

            public TempFile(ITempFolder folder, string fileName, string fileExtension, string mimeTypes,
                            FileFormat fileFormat)
            {
                _folder = folder;
                fakeName = fileName;
                _mimeTypes = mimeTypes;
                _fileExtension = fileExtension;
                var pathElements = new[]
                                       {
                                           folder.Path,
                                           System.IO.Path.ChangeExtension(Guid.NewGuid().ToBase32Url(), fileExtension)
                                       };
                _fileName = System.IO.Path.Combine(pathElements);
                System.IO.File.WriteAllBytes(_fileName, new byte[0]);
                var fldr = folder as TempFolder;
                if (fldr != null)
                    fldr.Add(this);
                _fileFormat = fileFormat;
            }

            protected override void DisposeCore()
            {
                base.DisposeCore();
                try
                {
                    if (System.IO.File.Exists(_fileName))
                        System.IO.File.Delete(_fileName);
                }
                catch (Exception e)
                {
                    e.LogDebug();
                    //TODO: Handle error here!
                }
                var fldr = _folder as TempFolder;
                if (fldr != null)
                    fldr.Remove(this);
                _folder = null;
            }

            #region Implementation of ITempFile

            public string Path
            {
                get { return _fileName; }
            }

            #endregion

            #region Implementation of IStreamProvider

            public Stream GetStream()
            {
                return System.IO.File.Open(Path, FileMode.Open);
                //return new AutoLockStream(() => System.IO.File.Open(Path, FileMode.Open),
                //                          ReadWriteSynchronizer<string>.Instance.CreateWriteLock(Path));
            }

            public byte[] Content
            {
                get { return this.GetStream().ToArray(); }
                set
                {
                    System.IO.File.WriteAllBytes(Path, value);
                }
            }
            #endregion

            #region Implementation of IFile

            public Guid Id
            {
                get { return Guid.Empty; }
            }

            public string MimeTypes
            {
                get { return _mimeTypes; }
            }

            public string FileNameWithExtension
            {
                get { return System.IO.Path.ChangeExtension(fakeName, _fileExtension); }
            }

            public string Extension
            {
                get { return _fileExtension; }
            }

            public long Size
            {
                get { return new FileInfo(Path).Length; }
            }

            public string MetaInfo { get; set; }

            public DateTime? DateCreated
            {
                get { return new FileInfo(Path).CreationTimeUtc; }
            }

            #endregion

            #region Implementation of IFileFormatInfo

            public FileFormat FileFormat
            {
                get { return _fileFormat; }
            }

            #endregion

        }

        #endregion

        #region Nested type: TempFolder

        private class TempFolder : Disposable, ITempFolder
        {
            private readonly string _fullPath;
            private List<TempFile> _files;

            public TempFolder(string getTempFolderRoot)
            {
                var pathElements = new[] { getTempFolderRoot, Guid.NewGuid().ToBase32Url() };
                _fullPath = System.IO.Path.Combine(pathElements);
                Directory.CreateDirectory(_fullPath);
                _files = new List<TempFile>();
            }

            #region Implementation of IDisposable

            protected override void DisposeCore()
            {
                base.DisposeCore();
                try
                {
                    DisposeOfFiles();

                    if (Directory.Exists(_fullPath))
                        Directory.Delete(_fullPath, true);
                }
                catch (Exception e)
                {
                    e.LogDebug();
                    //TODO: Handle error here!
                }
            }

            private void DisposeOfFiles()
            {
                if (_files != null)
                {
                    TempFile[] tempFiles = _files.ToArray();
                    foreach (TempFile tempFile in tempFiles)
                        tempFile.Dispose();
                    _files.Clear();
                    _files = null;
                }
            }

            #endregion

            #region Implementation of ITempFolder

            public string Path
            {
                get { return _fullPath; }
            }

            #endregion

            public void Add(TempFile tempFile)
            {
                if (_files != null)
                    _files.Add(tempFile);
            }

            public void Remove(TempFile tempFile)
            {
                if (_files != null)
                    _files.RemoveAll(f => f == tempFile);
            }
        }

        #endregion

        #endregion
    }
}