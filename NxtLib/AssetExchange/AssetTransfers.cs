using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetTransfers : BaseReply
    {
        public List<AssetTransfer> Transfers { get; set; }
    }
}