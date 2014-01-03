using NHibernate.Type;
using PocketMoney.Data;

namespace PocketMoney.Data.NHibernate
{
    public abstract class LockClassMap<T> : VersionedClassMap<T>
        where T : IEntityLock
    {
        protected LockClassMap()
        {
            OptimisticLock.None();
            Map(x => x.Locked).Not.Nullable().Default("0");
            Map(x => x.LockedUntil).Nullable().CustomType<UtcDateTimeType>();
        }
    }
}
