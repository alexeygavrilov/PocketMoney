using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PocketMoney.FileSystem;

namespace PocketMoney.Data.Wrappers
{
    [DataContract]
    public class WrapperFile : IFile
    {
        public WrapperFile(IFile file)
        {
            Id = file.Id;
            FileNameWithExtension = file.FileNameWithExtension;
            Extension = file.Extension;
            Size = file.Size;
            MetaInfo = file.MetaInfo;
            DateCreated = file.DateCreated;
        }

        [DataMember(IsRequired = true)]
        public Guid Id
        {
            get;
            set;
        }

        [DataMember]
        public string FileNameWithExtension
        {
            get;
            set;
        }

        [DataMember]
        public string Extension
        {
            get;
            set;
        }

        [DataMember]
        public long Size
        {
            get;
            set;
        }

        [DataMember]
        public string MetaInfo
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? DateCreated
        {
            get;
            set;
        }

        public System.IO.Stream GetStream()
        {
            return new System.IO.MemoryStream(this.Content);
        }

        //[DataMember]
        [IgnoreDataMember]
        [JsonIgnore]
        public byte[] Content
        {
            get;
            set;
        }

        public FileFormat FileFormat
        {
            get;
            set;
        }

    }
}
