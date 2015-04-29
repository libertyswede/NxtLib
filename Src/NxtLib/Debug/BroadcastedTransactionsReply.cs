using System.Collections.Generic;

namespace NxtLib.Debug
{
    public class BroadcastedTransactionsReply : BaseReply
    {
        public List<Transaction> Transactions { get; set; }
    }
}