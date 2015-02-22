using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Tokens
{
    public class DecodeHallmarkReply : BaseReply
    {
        [JsonProperty(PropertyName = "account")]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }
        public string Host { get; set; }
        public bool Valid { get; set; }
        public int Weight { get; set; }
    }
}