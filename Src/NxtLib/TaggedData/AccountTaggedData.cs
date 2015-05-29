using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.TaggedData
{
    public class AccountTaggedData : TaggedData
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime BlockTimestamp { get; set; }
        public List<string> ParsedTags { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "transaction")]
        public ulong TransactionId { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTimestamp { get; set; }
    }
}