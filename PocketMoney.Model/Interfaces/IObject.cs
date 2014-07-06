using System;

namespace PocketMoney.Model
{
    public interface IIdentity
    {
        Guid Id { get; }
    }

    public interface IObject : IIdentity
    {
        eObjectType ObjectType { get; }

        string Name { get; }
    }

    public enum eObjectType
    {
        User,
        Family,
        Task,
        Attainment
    }
}
