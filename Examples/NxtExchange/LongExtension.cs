using System;

namespace NxtExchange
{
    /// <summary>
    /// Since ID's in NXT are unsigned long's but Entity Framework does not support that, a lot of conversion between long <--> ulong is going on.
    /// These methods help out a bit in the process.
    /// </summary>
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
