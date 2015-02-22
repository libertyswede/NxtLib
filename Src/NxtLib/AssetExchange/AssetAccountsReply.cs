using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetAccountsReply : BaseReply
    {
        public List<AssetAccount> AccountAssets { get; set; }
    }
}