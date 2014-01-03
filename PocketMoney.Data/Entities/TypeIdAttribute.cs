using System;

namespace PocketMoney.Data
{
    [SerializableAttribute]
    [AttributeUsageAttribute(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class TypeIdAttribute : Attribute
    {
        private readonly Guid _typeId;

        public TypeIdAttribute(string typeGuid)
        {
            _typeId = new Guid(typeGuid);
        }


        public Guid TypeGuid
        {
            get { return _typeId; }
        }
    }
}