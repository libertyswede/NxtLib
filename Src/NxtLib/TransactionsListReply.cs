using System.Collections.Generic;

namespace NxtLib
{
    public class TransactionsListReply : BaseReply
    {
        public List<Transaction> Transactions { get; set; }
    }
}