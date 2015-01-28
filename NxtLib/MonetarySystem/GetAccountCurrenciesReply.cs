using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class GetAccountCurrenciesReply : BaseReply
    {
        public List<AccountCurrency> AccountCurrencies { get; set; }
    }
}