using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.ServerInfo
{
    public class BlockchainStatus
    {
        public string Application { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong CumulativeDifficulty { get; set; }
        public int CurrentMinRollbackHeight { get; set; }
        public bool IncludeExpiredPrunable { get; set; }
        public bool IsDownloading { get; set; }
        public bool IsScanning { get; set; }
        public bool IsTestnet { get; set; }
        public string LastBlockchainFeeder { get; set; }
        public int LastBlockchainFeederHeight { get; set; }

        [JsonProperty(PropertyName = Parameters.LastBlock)]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong LastBlockId { get; set; }
        public int MaxPrunableLifetime { get; set; }
        public int MaxRollback { get; set; }
        public int NumberOfBlocks { get; set; }
        public IList<string> Services { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; set; }
        public string Version { get; set; }
    }
}