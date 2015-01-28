using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.AssetExchange
{
    public class Assets : BaseReply
    {
        [JsonProperty(PropertyName = "assets")]
        public List<Asset> AssetList { get; set; }
    }
}