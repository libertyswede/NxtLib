using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class CurrencyAccountsReply : BaseReply
    {
        public List<CurrencyAccount> CurrencyAccounts { get; set; }
    }
}