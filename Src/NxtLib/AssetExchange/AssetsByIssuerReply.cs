using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetsByIssuerReply : BaseReply
    {
        public List<List<Asset>> Assets { get; set; }
    }
}