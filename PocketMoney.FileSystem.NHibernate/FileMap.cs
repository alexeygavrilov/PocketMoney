using PocketMoney.Data;
using PocketMoney.Data.NHibernate;
using NHibernate.Type;

namespace PocketMoney.FileSystem.NHibernate
{

    public sealed class FileMap : VersionedClassMap<File>
    {
        public FileMap()
        {
            Id(x => x.Id);
            Map(x => x.DeviceName).Not.Nullable().Length(50).Index("IX_File_All_Properties");
            Map(x => x.FileNameWithExtension).Nullable().Length(255).Index("IX_File_All_Properties");
            Map(x => x.Extension).Nullable().Index("IX_File_All_Properties");
            Map(x => x.Size).Not.Nullable().Index("IX_File_All_Properties");
            Map(x => x.Format).Not.Nullable().Index("IX_File_All_Properties");
            Map(x => x.MD5HashHI).Not.Nullable().Index("IX_File_All_Properties");
            Map(x => x.MD5HashLO).Not.Nullable().Index("IX_File_All_Properties");
            Map(x => x.MetaInfo).Length(255).Nullable().Index("IX_File_All_Properties");
            Map(x => x.DateLastAccessed).Nullable().CustomType<UtcDateTimeType>();
            Map(x => x.AccessCount).Not.Nullable();
            Map(x => x.Indexed).Not.Nullable();
            Map(x => x.IndexedOn).Nullable();
            Map(x => x.IsEncrypted);
            Map(x => x.SecurityDescriptor).Nullable().Length(5000);

            Component(x => x.FileCreatedBy).ColumnPrefix("CreatedBy");
            Component(x => x.FileOwner);
        }
    }
}
