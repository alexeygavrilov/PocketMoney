using System;

namespace PocketMoney.Data
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DoNotMapAttribute : Attribute
    {
    }
}