using NxtLib.Internal;

namespace NxtLib.MonetarySystem
{
    public class CurrencyLocator : LocatorBase
    {
        public readonly ulong? CurrencyId;
        public readonly string Code;

        private CurrencyLocator(ulong currencyId)
            : base(Parameters.Currency, currencyId.ToString())
        {
            CurrencyId = currencyId;
        }

        private CurrencyLocator(string code)
            : base(Parameters.Code, code)
        {
            Code = code;
        }

        public static implicit operator CurrencyLocator(ulong currencyId)
        {
            return ByCurrencyId(currencyId);
        }

        public static implicit operator CurrencyLocator(string code)
        {
            return ByCode(code);
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
