namespace PocketMoney.Data
{
    using System;

    [Serializable]
    public abstract class AbstractIdentity<T> where T : struct //:IXmlSerializable
    {
        private T _id;


        protected AbstractIdentity(T id)
        {
            _id = id;
        }

        public virtual T Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static implicit operator T(AbstractIdentity<T> stronglyTypedIdentity)
        {
            return stronglyTypedIdentity.Id;
        }

        public static bool operator ==(AbstractIdentity<T> lhs, AbstractIdentity<T> rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(AbstractIdentity<T> lhs, AbstractIdentity<T> rhs)
        {
            return !(lhs == rhs);
        }


        public override bool Equals(object obj)
        {
            var other = obj as AbstractIdentity<T>;

            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (obj.GetType()!=this.GetType()) return false;

            if (!Equals(default(T), Id)
                && !Equals(default(T), other.Id))
                return Equals(Id, other.Id);
            return Equals(other);
        }


        public bool Equals(AbstractIdentity<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Equals(other._id, _id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }


        //#region Implementation of IXmlSerializable

        //public virtual XmlSchema GetSchema()
        //{
        //    return null;
        //}

        //public void ReadXml(XmlReader reader)
        //{
        //    _id = Parse(reader.GetAttribute("Id"));
        //}

        //protected abstract T Parse(string stringValue);


        //public void WriteXml(XmlWriter writer)
        //{
        //    writer.WriteAttributeString("Id",_id.ToString());
        //}

        //#endregion

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}