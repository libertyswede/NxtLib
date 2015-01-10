using System.Collections.Generic;

namespace NxtLib.AccountOperations
{
    public class AccountTransactions : BaseReply
    {
        public List<Transaction> Transactions { get; set; }
    }
}