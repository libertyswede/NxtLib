using System.Collections.Generic;

namespace NxtLib.MonetarySystemOperations
{
    public class CurrencyOrAccountLocator : LocatorBase
    {
        private CurrencyOrAccountLocator(string key, string value) : base(key, value)
        {
        }

        private CurrencyOrAccountLocator(Dictionary<string, string> parameters)
            : base(parameters)
        {
        }

        public static CurrencyOrAccountLocator ByCurrencyId(ulong currencyId)
        {
            return new CurrencyOrAccountLocator("currency", currencyId.ToString());
        }

        public static CurrencyOrAccountLocator ByAccountId(string accountId)
        {
            return new CurrencyOrAccountLocator("account", accountId);
        }

        public static CurrencyOrAccountLocator ByCurrencyAndAccount(ulong currencyId, string accountId)
        {
            var dictionary = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {"account", accountId}
            };
            return new CurrencyOrAccountLocator(dictionary);
        }
    }
}