using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.VotingSystem
{
    public class Poll
    {
        [JsonProperty(PropertyName = "account")]
        public ulong AcountId { get; set; }
        public string AccountRs { get; set; }
        public string Description { get; set; }
        public bool Finished { get; set; }
        public int FinishHeight { get; set; }

        [JsonProperty(PropertyName = "holding")]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong HoldingId { get; set; }
        public int MaxNumberOfOptions { get; set; }
        public int MaxRangeValue { get; set; }
        public long MinBalance { get; set; }
        public MinBalanceModel MinBalanceModel { get; set; }
        public int MinNumberOfOptions { get; set; }
        public int MinRangeValue { get; set; }
        public string Name { get; set; }
        public List<string> Options { get; set; }

        [JsonProperty(PropertyName = "poll")]
        public ulong PollId { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        public VotingModel VotingModel { get; set; }
    }
}