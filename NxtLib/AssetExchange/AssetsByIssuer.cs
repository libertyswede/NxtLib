using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetsByIssuer : BaseReply
    {
        public List<List<Asset>> Assets { get; set; }
    }
}