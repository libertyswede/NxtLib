using System.Collections.Generic;

namespace NxtLib.Accounts
{
    public class GetAccountLedgerReply : BaseReply
    {
        public List<AccountLedgerEntry> Entries { get; set; }
    }
}