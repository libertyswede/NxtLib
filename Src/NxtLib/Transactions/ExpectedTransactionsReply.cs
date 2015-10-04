using System.Collections.Generic;

namespace NxtLib.Transactions
{
    public class ExpectedTransactionsReply : BaseReply
    {
        public List<Transaction> ExpectedTransactions { get; set; }
    }
}