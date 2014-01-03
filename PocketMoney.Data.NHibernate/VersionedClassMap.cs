using PocketMoney.Data;
using NHibernate.Type;

namespace PocketMoney.Data.NHibernate
{
    public abstract class VersionedClassMap<T> : FluentNHibernate.Mapping.ClassMap<T>
        where T : IEntity
    {
        protected VersionedClassMap()
        {
            OptimisticLock.None();
            Map(x => x.DateCreated).Not.Nullable().CustomType<UtcDateTimeType>();
            Map(x => x.DateUpdated).Not.Nullable().CustomType<UtcDateTimeType>();
            Version(x => x.Version).Not.Nullable().Default("0");
        }
    }
}