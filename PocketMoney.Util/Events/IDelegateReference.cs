using System;

namespace PocketMoney.Util.Events
{
    public interface IDelegateReference
    {
        Delegate Target { get; }
    }
}