using System.Collections.Generic;

namespace NxtLib.TaggedData
{
    public class TaggedDataExtendTransactionsReply : BaseReply
    {
        public IList<Transaction> ExtendTransactions { get; set; }
    }
}