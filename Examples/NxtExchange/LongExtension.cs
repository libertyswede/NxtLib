using System;

namespace NxtExchange
{
    public static class LongExtension
    {
        public static ulong ToUnsigned(this long? me)
        {
            if (!me.HasValue)
            {
                throw new ArgumentException();
            }
            return (ulong) me.Value;
        }

        public static long ToSigned(this ulong? me)
        {
            if (!me.HasValue)
            {
                throw new ArgumentException();
            }
            return (long)me.Value;
        }

        public static ulong ToUnsigned(this long me)
        {
            return (ulong) me;
        }

        public static long ToSigned(this ulong me)
        {
            return (long) me;
        }
    }
}
