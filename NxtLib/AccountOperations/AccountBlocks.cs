using System.Collections.Generic;

namespace NxtLib.AccountOperations
{
    public class AccountBlocks<T> : BaseReply
    {
        public List<Block<T>> Blocks { get; set; }
    }
}