using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountLedgerEntry
    { 
        [JsonProperty(Parameters.Account)]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Balance { get; set; }

        [JsonProperty(Parameters.Block)]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong BlockId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Change { get; set; }

        [JsonProperty(Parameters.Event)]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong EventId { get; set; }
        public string EventType { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int Height { get; set; }

        [JsonProperty(Parameters.Holding)]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong HoldingId { get; set; }
        public string HoldingType { get; set; }
        public bool IsTransactionEvent { get; set; }
        
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int LedgerId { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonConverter(typeof(TransactionConverter))]
        public Transaction Transaction { get; set; }
    }
}