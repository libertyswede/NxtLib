using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Blocks
{
    public class GetBlockIdReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Block)]
        public ulong BlockId { get; set; }
    }
}