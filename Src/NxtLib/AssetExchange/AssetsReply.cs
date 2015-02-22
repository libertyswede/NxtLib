using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.AssetExchange
{
    public class AssetsReply : BaseReply
    {
        [JsonProperty(PropertyName = "assets")]
        public List<Asset> AssetList { get; set; }
    }
}