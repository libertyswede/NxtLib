using System.Collections.Generic;

namespace NxtLib.Transactions
{
    public class UnconfirmedTransactionsReply : BaseReply
    {
        public List<Transaction> UnconfirmedTransactions { get; set; }
    }
}