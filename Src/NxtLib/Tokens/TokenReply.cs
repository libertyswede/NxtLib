using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Tokens
{
    public class TokenReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public ulong Account { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        public bool Valid { get; set; }
    }
}