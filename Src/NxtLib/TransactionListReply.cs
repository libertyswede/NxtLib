using System.Collections.Generic;

namespace NxtLib
{
    public class TransactionListReply : BaseReply
    {
        public List<Transaction> Transactions { get; set; }
    }
}