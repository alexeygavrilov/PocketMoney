using System;

namespace PocketMoney.Data
{
    [Serializable]
    public abstract class LongIdentity : AbstractIdentity<long>
    {
        protected LongIdentity(string id)
            : base(Parse(id))
        {
        }

        protected LongIdentity(long id)
            : base(id)
        {
        }

        protected static long Parse(string stringValue)
        {
            return long.Parse(stringValue);
        }
    }


    [Serializable]
    public abstract class IntIdentity : AbstractIdentity<int>
    {
        protected IntIdentity(string id)
            : base(Parse(id))
        {
        }

        protected IntIdentity(int id)
            : base(id)
        {
        }

        protected static int Parse(string stringValue)
        {
            return int.Parse(stringValue);
        }
    }
}