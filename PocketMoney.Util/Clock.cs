using System;
using System.Diagnostics;

namespace PocketMoney.Util
{
    public class Clock
    {
        private static Func<DateTime> now = () => DateTime.Now;
        private static Func<DateTime> utcNow = () => DateTime.UtcNow;

        public static Func<DateTime> Now
        {
            [DebuggerStepThrough]
            get { return now; }

            [DebuggerStepThrough]
            set { now = value; }
        }

        public static Func<DateTime> UtcNow
        {
            [DebuggerStepThrough]
            get { return utcNow; }

            [DebuggerStepThrough]
            set { utcNow = value; }
        }
    }
}