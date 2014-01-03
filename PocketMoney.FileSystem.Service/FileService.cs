using System;
using System.IO;
using System.Web;
using Castle.Services.Transaction;
using PocketMoney.Data;
using PocketMoney.Data.Security;
using PocketMoney.Util;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.IO;

namespace PocketMoney.FileSystem.Service
{
    [Transactional]
    public class FileService : IFileService
    {
        private readonly IAuthorization _authorization;
        private readonly IRepository<File, FileId, Guid> _fileRepository;
        private readonly IStorageService _storageService;

        public FileService(IStorageService storageService,
                            IAuthorization authorization,
                            IRepository<File, FileId, Guid> fileRepository)
        {
            _storageService = storageService;
            _fileRepository = fileRepository;
            _authorization = authorization;
        }


        [Transaction(TransactionMode.Requires)]
        public File Store(IFile file, IFamily currentFamily, string securityDescriptor, IUser currentUser)
        {
            if (String.IsNullOrEmpty(securityDescriptor))
                throw new EmptyDataException("Security Descriptor").
                    LogError();
            if (file.IsNull()) throw new ArgumentNullException("file").LogError();
            if (currentFamily == null) throw new ArgumentNullException("family").LogError();
            IDevice device = _storageService.CurrentDevice;
            if (!device.StorageInfo.IsAvailable)
                throw new UnauthorizedAccessException("You do not have permission to use the '" + device.Settings.Name +
                                                      "' device.").LogError();

            File newfileRecord = null;

            try
            {
                long _md5Hi, _md5Lo;
                File duplicate = FindDuplicate(file, out _md5Hi, out _md5Lo);
                if (duplicate == null)
                {
                    newfileRecord = new File(currentFamily, currentUser, device.Settings.Name);
                    newfileRecord.PopulateFrom(file);
                    newfileRecord.MD5HashHI = _md5Hi;
                    newfileRecord.MD5HashLO = _md5Lo;
                    newfileRecord.SecurityDescriptor = securityDescriptor;

                    _fileRepository.Add(newfileRecord);

                    device.Store(newfileRecord, file);
                }
                else
                    newfileRecord = duplicate;

                UpdateStatistics(newfileRecord);
                return newfileRecord;
            }
            catch (Exception e)
            {
                e.LogDebug();
                if ((newfileRecord != null) && (newfileRecord.Id != Guid.Empty))
                    device.DeleteFile(newfileRecord);
                throw;
            }
        }

        public File Retrieve(FileId fileId)
        {
            File result = _fileRepository.One(fileId);
            if (result == null)
                throw new FileNotFoundException(String.Format("Invalid file reference {0}", fileId.Id));
            CheckSecurity(result, ObjectPermissions.Read);
            UpdateStatistics(result);
            return result;
        }

        public File RenameFile(FileId fileId, string fileName)
        {
            File file = this.Retrieve(fileId);
            file.FileNameWithExtension = Path.GetFileName(fileName);
            file.Extension = Path.GetExtension(fileName);
            _fileRepository.Update(file);
            return file;
        }

        public void RemoveAll()
        {
            IDevice device = _storageService.CurrentDevice;
            device.ClearAllData();
        }

        private void CheckSecurity(File file, ObjectPermissions accessRequested)
        {
            // TODO: enable security checking
//            if (!_authorization.IsAuthorized(accessRequested, file))
//            {
//#if DEBUG
//                Debugger.Break();
//#else
//                    throw new SecurityException("Unable to open file. Permission denied.").LogError(String.Format("SecurityDescriptor: {0} ", file.SecurityDescriptor));
//#endif
//            }
            return;
        }

        private void UpdateStatistics(File file)
        {
            // Make the Audit subsystem
            if (file != null)
            {
                file.AccessCount++;
                file.DateLastAccessed = Clock.UtcNow();
                _fileRepository.Update(file);
            }
        }

        private File FindDuplicate(IFile file, out long md5Hi, out long md5Lo)
        {
            var dest = file as File;
            if ((dest != null) && (dest.Id != Guid.Empty))
            {
                if (_fileRepository.Exists(x => x.Id == dest.Id))
                {
                    md5Hi = dest.MD5HashHI;
                    md5Lo = dest.MD5HashLO;
                    return dest;
                }
            }

            long _md5Hi;
            long _md5Lo;
            //string fileName = file.FileNameWithExtension;
            using (Stream stream = file.GetStream())
            {
                stream.GetMD5(out _md5Hi, out _md5Lo);
                md5Hi = _md5Hi;
                md5Lo = _md5Lo;
            }
            long len = file.Size;
            try
            {
                return _fileRepository.FindOne(x => (x.MD5HashHI == _md5Hi) && (x.MD5HashLO == _md5Lo) && (x.Size == len));
            }
            catch
            {
                return null;
            }
        }

        public static string VirtualPathToAbsolute(string virtualPath)
        {
            string url = ProcessVirtualPath(virtualPath);
            if (url != null)
            {
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (!applicationPath.Equals("/", StringComparison.OrdinalIgnoreCase))
                {
                    if (!url.Contains(applicationPath + "/")) url = applicationPath + url;
                }
                return url;
            }
            return virtualPath;
        }

        private static string ProcessVirtualPath(string virtualPath)
        {
            string url = virtualPath;
            if ((!string.IsNullOrEmpty(url)) && (HttpContext.Current != null))
            {
                if (!(url.StartsWith(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) || url.StartsWith(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase)))
                {
                    if (url.StartsWith("~", StringComparison.OrdinalIgnoreCase)) url = url.Remove(0, 1);
                    if (!url.StartsWith("/", StringComparison.OrdinalIgnoreCase)) url = "/" + url;
                    return url;
                }
            }
            return null;
        }

    }


}
