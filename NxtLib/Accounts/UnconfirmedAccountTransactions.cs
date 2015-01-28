using System.Collections.Generic;

namespace NxtLib.Accounts
{
    public class UnconfirmedAccountTransactions : BaseReply
    {
        public List<Transaction> UnconfirmedTransactions { get; set; }
    }
}