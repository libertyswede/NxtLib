using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    public abstract class CurrencyInfo
    {
        public string Code { get; set; }
        public byte Decimals { get; set; }
        public int IssuanceHeight { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "issuerAccount")]
        public ulong IssuerAccountId { get; set; }
        public string IssuerAccountRs { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(CurrencyTypeConverter))]
        public CurrencyType Type { get; set; }
    }
}
