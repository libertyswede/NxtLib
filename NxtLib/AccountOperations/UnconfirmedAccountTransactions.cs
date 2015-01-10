using System.Collections.Generic;

namespace NxtLib.AccountOperations
{
    public class UnconfirmedAccountTransactions : BaseReply
    {
        public List<Transaction> UnconfirmedTransactions { get; set; }
    }
}