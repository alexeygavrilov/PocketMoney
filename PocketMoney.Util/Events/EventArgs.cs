using System;
using System.Diagnostics;

namespace PocketMoney.Util.Events
{
    public class EventArgs<TValue> : EventArgs
    {
        [DebuggerStepThrough]
        public EventArgs(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; private set; }
    }
}