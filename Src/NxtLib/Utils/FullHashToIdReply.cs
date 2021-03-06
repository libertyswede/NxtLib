﻿using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Utils
{
    public class FullHashToIdReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.StringId)]
        public ulong Id { get; set; }
        public long LongId { get; set; }
    }
}