using System;
using System.Runtime.Serialization;

namespace PocketMoney.Data
{
    [DataContract]
    [Serializable]
    public abstract class Entity<TEntity, TId, TIdType> : EntityBase, IEquatable<TEntity>
        where TEntity : Entity<TEntity, TId, TIdType>
        where TId : AbstractIdentity<TIdType>
        where TIdType : struct
    {
        private readonly object _locker;
        protected Entity()
        {
            _locker = new object();
        }
        private TIdType _id;
        private TId _strongId;
        [DataMember]
        [Details]
        public virtual TIdType Id
        {
            get
            {
                    return _id;
            }
            set
            {
                if  (Equals(_id,value))  return;
                lock (_locker)
                {
                    if (Equals(_id, value)) return;
                    _id = value;
                    this._strongId = null;
                }
            }
        }

        public virtual TId StrongId
        {
            get
            {
                if (_strongId != null) return this._strongId;
                lock (_locker)
                {
                
                    if (_strongId != null) return this._strongId;
                    return this._strongId ?? (this._strongId = IdFactory<TIdType>.Create<TId>(_id));
                }
            }
            set
            {

                if (!ReferenceEquals(value, null))
                lock (_locker)
                {
                    _id = value.Id;
                    this._strongId = value;
                }
            }
        }

        #region IEquatable<TEntity> Members



        public virtual bool Equals(TEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return GetType().IsAssignableFrom(other.GetType()) && Equals((object)other);
        }

        #endregion

        public override bool Equals(object obj)
        {
            var other = obj as TEntity;

            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            var left = StrongId;
            var right = other.StrongId;
            if (!Equals(default(TId), left)
                && !Equals(default(TId), right))
                return Equals(left, right);

            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            var sid = StrongId;
            if (!sid.Equals(default(TId)))
                return sid.GetType().GetHashCode() ^ sid.GetHashCode();
            return base.GetHashCode();
        }
    }
}