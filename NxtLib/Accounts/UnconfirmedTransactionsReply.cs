using System.Collections.Generic;

namespace NxtLib.Accounts
{
    public class UnconfirmedTransactionsReply : BaseReply
    {
        public List<Transaction> UnconfirmedTransactions { get; set; }
    }
}