using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class ExpectedAssetTransfersReply : BaseReply
    {
        public List<ExpectedAssetTransfer> Transfers { get; set; }
    }
}