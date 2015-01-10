using System.Collections.Generic;

namespace NxtLib.AssetOperations
{
    public class AssetTransfers : BaseReply
    {
        public List<AssetTransfer> Transfers { get; set; }
    }
}