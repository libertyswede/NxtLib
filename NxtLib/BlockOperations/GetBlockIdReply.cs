using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.BlockOperations
{
    public class GetBlockIdReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "block")]
        public ulong BlockId { get; set; }
    }
}