using System.Collections.Generic;

namespace NxtLib.Accounts
{
    public class AccountBlocks<T> : BaseReply
    {
        public List<Block<T>> Blocks { get; set; }
    }
}