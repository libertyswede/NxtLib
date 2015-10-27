using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib.MonetarySystem
{
    public class CurrencyOrAccountLocator : LocatorBase
    {
        public readonly Account Account;
        public readonly ulong? CurrencyId;

        private CurrencyOrAccountLocator(ulong currencyId)
            : base(Parameters.Currency, currencyId.ToString())
        {
            CurrencyId = currencyId;
        }

        private CurrencyOrAccountLocator(Account account)
            : base(Parameters.Account, account.AccountId.ToString())
        {
            Account = account;
        }

        private CurrencyOrAccountLocator(ulong currencyId, Account account, Dictionary<string, string> parameters)
            : base(parameters)
        {
            Account = account;
            CurrencyId = currencyId;
        }

        public static CurrencyOrAccountLocator ByCurrencyId(ulong currencyId)
        {
            return new CurrencyOrAccountLocator(currencyId);
        }

        public static CurrencyOrAccountLocator ByAccountId(Account account)
        {
            return new CurrencyOrAccountLocator(account);
        }

        public static CurrencyOrAccountLocator ByCurrencyAndAccount(ulong currencyId, Account account)
        {
            var dictionary = new Dictionary<string, string>
            {
                {Parameters.Currency, currencyId.ToString()},
                {Parameters.Account, account.AccountId.ToString()}
            };
            return new CurrencyOrAccountLocator(currencyId, account.AccountId.ToString(), dictionary);
        }
    }
}