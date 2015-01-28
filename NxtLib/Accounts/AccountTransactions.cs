using System.Collections.Generic;

namespace NxtLib.Accounts
{
    public class AccountTransactions : BaseReply
    {
        public List<Transaction> Transactions { get; set; }
    }
}