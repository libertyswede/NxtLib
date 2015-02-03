using System.Collections.Generic;

namespace NxtLib.Accounts
{
    public class AccountBlocksReply<T> : BaseReply
    {
        public List<Block<T>> Blocks { get; set; }
    }
}