using System;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Data
{
    [Serializable]
    public abstract class GuidIdentity : AbstractIdentity<Guid>
    {
        private Type _entityType;

        protected GuidIdentity(string id, Type entityType)
            : this(Parse(id), entityType)
        {
            _entityType = entityType;
        }

        protected GuidIdentity(Guid id, Type entityType)
            : base(id)
        {
            _entityType = entityType;
        }
       
        public Type EntityType
        {
            get { return _entityType; }
        }

        public Guid EntityTypeId()
        {
            return _entityType.GetTypeId();
        }

        public virtual string ToTransportString()
        {
            return String.Format("{0}:{1}", this._entityType, this);
        }

        protected static Guid GetTransportedGuid(string transportId)
        {
            return transportId.Split(':')[1].FromBase32Url();
        }

        
        [System.Diagnostics.DebuggerStepThrough]
        protected static Guid Parse(string stringValue)
        {
            try
            {
                return new Guid(stringValue);
            }
            catch (Exception)
            {
                return stringValue.FromBase32Url();
            }
        }

        public override string ToString()
        {
            return Id.ToBase32Url();
        }
    }
}