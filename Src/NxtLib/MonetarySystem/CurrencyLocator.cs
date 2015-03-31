namespace NxtLib.MonetarySystem
{
    public class CurrencyLocator : LocatorBase
    {
        public readonly ulong? CurrencyId;
        public readonly string Code;

        private CurrencyLocator(ulong currencyId)
            : base("currency", currencyId.ToString())
        {
            CurrencyId = currencyId;
        }

        private CurrencyLocator(string code)
            : base("code", code)
        {
            Code = code;
        }

        public static CurrencyLocator ByCurrencyId(ulong currencyId)
        {
            return new CurrencyLocator(currencyId);
        }

        public static CurrencyLocator ByCode(string code)
        {
            return new CurrencyLocator(code);
        }
    }
}
