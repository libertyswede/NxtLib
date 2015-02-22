using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetTransfersReply : BaseReply
    {
        public List<AssetTransfer> Transfers { get; set; }
    }
}