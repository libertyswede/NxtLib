using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class FxtQuantityReply : BaseReply
    {
        public int DistributionEnd { get; set; }
        public int DistributionFrequency { get; set; }
        public int DistributionStart { get; set; }
        public int DistributionStep { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.FxtAsset)]
        public ulong FxtAssetId { get; set; }
        
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long QuantityQnt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long RemainingQuantityQNT { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long TotalExpectedQuantityQNT { get; set; }
    }
}
