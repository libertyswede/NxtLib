using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.MonetarySystemOperations
{
    public class GetMintingTargetReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Counter { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "currency")]
        public ulong CurrencyId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int Difficulty { get; set; }
        public string TargetBytes { get; set; }
    }
}