using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public class AssetsReply : BaseReply
    {
        [JsonProperty(PropertyName = Parameters.Assets)]
        public List<Asset> AssetList { get; set; }
    }
}