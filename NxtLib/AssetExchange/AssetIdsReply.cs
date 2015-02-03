using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.AssetExchange
{
    public class AssetIdsReply : BaseReply
    {
        [JsonProperty(PropertyName = "assetIds")]
        public List<ulong> AssetIdList { get; set; }
    }
}