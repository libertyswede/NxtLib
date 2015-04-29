using System.Collections.Generic;

namespace NxtLib.Accounts
{
    public class SearchAccountsReply : BaseReply
    {
        public List<AccountInfo> Accounts { get; set; }
    }
}