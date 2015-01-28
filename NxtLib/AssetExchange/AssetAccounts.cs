using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetAccounts : BaseReply
    {
        public List<AssetAccount> AccountAssets { get; set; }
    }
}