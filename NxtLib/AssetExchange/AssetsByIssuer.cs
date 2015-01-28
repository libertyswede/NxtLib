using System.Collections.Generic;

namespace NxtLib.AssetOperations
{
    public class AssetsByIssuer : BaseReply
    {
        public List<List<Asset>> Assets { get; set; }
    }
}