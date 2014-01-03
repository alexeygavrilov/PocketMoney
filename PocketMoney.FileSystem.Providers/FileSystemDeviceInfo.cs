using System;
using System.IO;
using System.Linq;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.FileSystem.Providers
{
    public sealed class FileSystemDeviceInfo : IDeviceInfo
    {
        private readonly string _path;
        private bool? _isAvailable;

        public FileSystemDeviceInfo(string path)
        {
            _path = path;
           
        }

        #region IDeviceInfo Members

        public long SpaceTotal
        {
            get { return GetSpaceTotal(_path);  }
        }

        public long SpaceAvailable
        {
            get { return GetSpaceAvailable(_path); }
        }

        public bool IsAvailable
        {
            get
            {  if (_isAvailable==null)
                    _isAvailable = GetIsAvailable(_path);
                return _isAvailable.Value;
            }
        }

        #endregion


        private static bool GetIsAvailable(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                string fileName = Path.Combine(path, Path.GetRandomFileName());
                try
                {
                    using (FileStream fs = System.IO.File.Create(fileName, 1, FileOptions.DeleteOnClose))
                    {
                        fs.WriteByte(0);
                    }
                    return true;
                }
                catch (UnauthorizedAccessException ex)
                {
                    ex.LogError();
                }
            }
            return false;
        }

        private static long GetSpaceTotal(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                long size = 0;
                GetDirectorySize(path, ref size);
                return size;
            }
            return 0;
        }

        private static void GetDirectorySize(string directory, ref long size)
        {
            try
            {
                foreach (string dir in System.IO.Directory.GetDirectories(directory))
                {
                    GetDirectorySize(dir, ref size);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                ex.LogError();
            }
            size += new DirectoryInfo(directory).GetFiles().Sum(file => file.Length);
        }

        private static long GetSpaceAvailable(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                var drive = new DriveInfo(System.IO.Directory.GetDirectoryRoot(path).Substring(0, 1));
                return drive.TotalFreeSpace;
            }
            return 0;
        }
    }
}