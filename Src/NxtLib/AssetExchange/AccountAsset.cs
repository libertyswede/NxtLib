using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public class AccountAsset
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Asset { get; set; }
        public byte Decimals { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long QuantityQnt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long UnconfirmedQuantityQnt { get; set; }
    }
}