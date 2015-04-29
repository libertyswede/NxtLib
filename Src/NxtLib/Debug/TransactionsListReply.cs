using System.Collections.Generic;

namespace NxtLib.Debug
{
    public class TransactionsListReply : BaseReply
    {
        public List<Transaction> Transactions { get; set; }
    }
}