using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    public class Block<T>
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong BaseTarget { get; set; }

        [JsonProperty(PropertyName = Parameters.Block)]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong BlockId { get; set; }
        public string BlockSignature { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong CumulativeDifficulty { get; set; }
        public string GenerationSignature { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Generator { get; set; }
        public string GeneratorPublicKey { get; set; }
        public string GeneratorRs { get; set; }
        public int Height { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong? NextBlock { get; set; }
        public int NumberOfTransactions { get; set; }
        public string PayloadHash { get; set; }
        public int PayloadLength { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong? PreviousBlock { get; set; }
        public string PreviousBlockHash { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        public List<T> Transactions { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.TotalAmountNqt)]
        public Amount TotalAmount { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.TotalFeeNqt)]
        public Amount TotalFee { get; set; }
        public int Version { get; set; }
    }
}