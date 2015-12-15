using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Shuffling
{
    public class HoldingInfo
    {
        public string Code { get; set; }
        public int Decimals { get; set; }
        public int? IssuanceHeight { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.IssuerAccount)]
        public ulong? IssuerAccountId { get; set; }
        public string IssuerAccountRs { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(CurrencyTypeConverter))]
        public CurrencyType? Type { get; set; }
    }
}