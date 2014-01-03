using System;

namespace PocketMoney.Data
{
    public interface ITransaction : IDisposable
    {
        bool IsActive { get; }
        void Rollback();
        void Commit();
    }
}