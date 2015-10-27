using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public class AssetIdsReply : BaseReply
    {
        [JsonProperty(PropertyName = Parameters.AssetIds)]
        public List<ulong> AssetIdList { get; set; }
    }
}