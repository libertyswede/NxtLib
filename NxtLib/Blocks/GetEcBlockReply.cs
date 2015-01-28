using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Blocks
{
    public class GetEcBlockReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int EcBlockHeight { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong EcBlockId { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}