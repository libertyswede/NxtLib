namespace NxtLib.MonetarySystem
{
    public class CurrencyLocator : LocatorBase
    {
        public CurrencyLocator(ulong currencyId)
            : base("currency", currencyId.ToString())
        {
        }

        public CurrencyLocator(string code)
            : base("code", code)
        {
        }
    }
}
