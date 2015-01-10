using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.ServerInfoOperations
{
    public class GetBlockchainStatusReply : BaseReply
    {
        public string Application { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong CumulativeDifficulty { get; set; }
        public bool IsScanning { get; set; }
        public string LastBlockchainFeeder { get; set; }
        public int LastBlockchainFeederHeight { get; set; }

        [JsonProperty(PropertyName = "lastBlock")]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong LastBlockId { get; set; }
        public int NumberOfBlocks { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; set; }
        public string Version { get; set; }
    }
}