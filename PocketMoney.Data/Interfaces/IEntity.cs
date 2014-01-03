using System;

namespace PocketMoney.Data
{
    public interface IEntity
    {
        DateTime? DateCreated { get; set; }

        DateTime? DateUpdated { get; set; }

        int Version { get; set; }
    }

    public interface IEntityLock : IEntity
    {
        bool Locked { get; set; }
        DateTime? LockedUntil { get; set; }
    }

}