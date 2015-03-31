using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class CurrencyOrAccountLocator : LocatorBase
    {
        public readonly string AccountId;
        public readonly ulong? CurrencyId;

        private CurrencyOrAccountLocator(ulong currencyId)
            : base("currency", currencyId.ToString())
        {
            CurrencyId = currencyId;
        }

        private CurrencyOrAccountLocator(string accountId)
            : base("account", accountId)
        {
            AccountId = accountId;
        }

        private CurrencyOrAccountLocator(ulong currencyId, string accountId, Dictionary<string, string> parameters)
            : base(parameters)
        {
            AccountId = accountId;
            CurrencyId = currencyId;
        }

        public static CurrencyOrAccountLocator ByCurrencyId(ulong currencyId)
        {
            return new CurrencyOrAccountLocator(currencyId);
        }

        public static CurrencyOrAccountLocator ByAccountId(string accountId)
        {
            return new CurrencyOrAccountLocator(accountId);
        }

        public static CurrencyOrAccountLocator ByCurrencyAndAccount(ulong currencyId, string accountId)
        {
            var dictionary = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {"account", accountId}
            };
            return new CurrencyOrAccountLocator(currencyId, accountId, dictionary);
        }
    }
}