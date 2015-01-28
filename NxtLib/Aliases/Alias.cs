using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Aliases
{
    public class Alias : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "alias")]
        public ulong AliasId { get; set; }
        public string AliasName { get; set; }
        public string AliasUri { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "buyer")]
        public ulong? BuyerId { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "priceNQT")]
        public Amount Price { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}