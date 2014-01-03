using System;
using PocketMoney.Data;

namespace PocketMoney.Data
{
    public abstract class EntityLock<TEntity, TId, TIdType> : Entity<TEntity, TId, TIdType>, IEntityLock
        where TEntity : Entity<TEntity, TId, TIdType>
        where TId : AbstractIdentity<TIdType>
        where TIdType : struct
    {
        public virtual bool Locked { get; set; }
        public virtual DateTime? LockedUntil { get; set; }
    }

}
