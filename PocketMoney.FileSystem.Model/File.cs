using System;
using System.IO;
using PocketMoney.Data;
using PocketMoney.Data.Security;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.IO;
using Microsoft.Practices.ServiceLocation;

namespace PocketMoney.FileSystem
{
    public class File : Entity<File, FileId, Guid>, IFile, IFileAdvanced, ISecuredObject, IFileFormatInfo
    {
        protected File()
        {
            IsEncrypted = false;
        }

        public File(IFamily owner, IUser user, string deviceName)
        {
            FileOwner = owner;
            FileCreatedBy = user;
            DeviceName = deviceName;
        }

        #region Primitive Properties

        public virtual FileFormatEnum Format { get; set; }

        public virtual string SecurityDescriptor { get; set; }

        #region IFile Members

        private string _fileNameWithExtension;

        public virtual string FileNameWithExtension
        {
            get
            {
                return this._fileNameWithExtension;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new EmptyDataException("File name");
                value = value.RemoveInvalidFileNameCharacters('_');
                if (String.IsNullOrWhiteSpace(value))
                    throw new EmptyDataException("File name");
                this._fileNameWithExtension = value;
            }
        }

        public virtual bool IsEncrypted { get; set; }

        public virtual string Extension { get; set; }

        public virtual long Size { get; set; }

        public virtual string MetaInfo { get; set; }

        #endregion

        #region IFileAdvanced Members

        public virtual string DeviceName { get; set; }

        public virtual long MD5HashHI { get; set; }

        public virtual long MD5HashLO { get; set; }

        public virtual DateTime? DateLastAccessed { get; set; }

        public virtual long AccessCount { get; set; }

        public virtual bool Indexed { get; set; }

        public virtual DateTime? IndexedOn { get; set; }

        #endregion

        #endregion

        #region Navigation Properties

        public virtual IFamily FileOwner { get; set; }

        public virtual IUser FileCreatedBy { get; set; }

        #endregion

        #region Implementation of IStreamProvider

        public virtual Stream GetStream()
        {
            var storageManager = ServiceLocator.Current.GetInstance<IStorageService>();
            IDevice device = storageManager.GetDevice(DeviceName);
            IStreamProvider sp = device.Retrieve(this);
            return sp.GetStream();
        }

        public virtual Stream GetThumbnail(ThumbnailOptions option)
        {
            var storageManager = ServiceLocator.Current.GetInstance<IStorageService>();
            IDevice device = storageManager.GetDevice(DeviceName);
            IStreamProvider sp = device.Thumbnail(this, option);
            return sp.GetStream();
        }

        public virtual byte[] Content
        {
            get
            {
                using (var stream = this.GetStream())
                {
                    return stream.ToArray();
                }
            }
            set
            {
                var storageManager = ServiceLocator.Current.GetInstance<IStorageService>();
                IDevice device = storageManager.GetDevice(DeviceName);
                device.Store(this, FileFactory.FromArray(this.FileNameWithExtension, value, this.FileFormat.MimeTypes));

            }
        }

        #endregion

        #region Methods

        public virtual void PopulateFrom(IFile file)
        {
            if (file == null) throw new ArgumentNullException("file").LogError();
            FileNameWithExtension = file.FileNameWithExtension;
            Extension = String.IsNullOrEmpty(file.Extension)
                            ? Path.GetExtension(file.FileNameWithExtension)
                            : file.Extension;
            MetaInfo = file.MetaInfo ?? string.Empty;
            Size = file.Size;
            if (file.FileFormat != null)
                Format = file.FileFormat.Format;
            else
                Format = FileFormat.Detect(string.Empty, file.Extension).Format;
            DateCreated = file.DateCreated;
        }
        #endregion

        #region Implementation of IFileFormatInfo

        public virtual FileFormat FileFormat
        {
            get { return FileFormat.Find(Format); }
        }

        #endregion

        #region Implementation of ISecuredObject

        public virtual string GetSecurityDescriptor()
        {
            return SecurityDescriptor;
        }

        #endregion

    }

    [Serializable]
    public class FileId : GuidIdentity
    {
        public FileId()
            : base(Guid.Empty, typeof(File))
        {
        }

        public FileId(string id)
            : base(id, typeof(File))
        {
        }

        public FileId(Guid fileId)
            : base(fileId, typeof(File))
        {
        }
    }

}