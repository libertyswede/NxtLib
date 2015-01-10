using System.Collections.Generic;

namespace NxtLib.MonetarySystemOperations
{
    public class GetAccountCurrenciesReply : BaseReply
    {
        public List<AccountCurrency> AccountCurrencies { get; set; }
    }
}