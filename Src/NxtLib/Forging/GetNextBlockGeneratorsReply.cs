using Newtonsoft.Json;
using NxtLib.Internal;
using System;
using System.Collections.Generic;

namespace NxtLib.Forging
{
    public class GetNextBlockGeneratorsReply : BaseReply
    {
        public int ActiveCount { get; set; }
        public List<NextBlockGenerator> Generators { get; set; }
        public int Height { get; set; }

        [JsonProperty(PropertyName = Parameters.LastBlock)]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong LastBlockId { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}