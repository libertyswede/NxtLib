using System.Collections.Generic;

namespace NxtLib.Accounts
{
    public class AccountTransactionsReply : BaseReply
    {
        public List<Transaction> Transactions { get; set; }
    }
}