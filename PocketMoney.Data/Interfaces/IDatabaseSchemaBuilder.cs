using System;

namespace PocketMoney.Data
{
    public interface IDatabaseSchemaBuilder : IDisposable
    {
        void CreateSchema();

        void DropSchema();
    }
}