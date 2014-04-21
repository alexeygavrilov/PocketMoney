using System;

namespace PocketMoney.Model
{
    public interface IObject
    {
        Guid Id { get; }
        eObjectType ObjectType { get; }
    }

    public enum eObjectType
    {
        User,
        Family,
        Task
    }
}
