namespace NxtExchange
{
    public static class LongExtension
    {
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
