using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.AssetExchange
{
    public class AssetIds : BaseReply
    {
        [JsonProperty(PropertyName = "assetIds")]
        public List<ulong> AssetIdList { get; set; }
    }
}