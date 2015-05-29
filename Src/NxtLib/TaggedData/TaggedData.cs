using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.TaggedData
{
    public class TaggedData : ITaggedData
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime BlockTimestamp { get; set; }
        public string Channel { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public bool IsText { get; set; }
        public string Name { get; set; }
        public List<string> ParsedTags { get; set; }
        public string Tags { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "transaction")]
        public ulong TransactionId { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTimestamp { get; set; }
        public string Type { get; set; }
    }
}