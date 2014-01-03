using System;
using PocketMoney.Data;

namespace PocketMoney.Data
{
    [Serializable]
    public abstract class EntityBase : ObjectBase, IEntity
    {
        #region IEntity Members

        public virtual DateTime? DateCreated { get; set; }

        public virtual DateTime? DateUpdated { get; set; }

        public virtual int Version { get; set; }

        #endregion
    }
}