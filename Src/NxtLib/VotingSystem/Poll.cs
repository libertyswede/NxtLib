using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.VotingSystem
{
    public abstract class Poll
    {
        [JsonProperty(PropertyName = "account")]
        public ulong AcountId { get; set; }
        public string AccountRs { get; set; }
        public string Description { get; set; }
        public bool Finished { get; set; }
        public int FinishHeight { get; set; }
        public int MaxNumberOfOptions { get; set; }
        public int MaxRangeValue { get; set; }
        public MinBalanceModel MinBalanceModel { get; set; }
        public long MinNumberOfBalance { get; set; }
        public int MinNumberOfOptions { get; set; }
        public int MinRangeValue { get; set; }
        public string Name { get; set; }
        public List<string> Options { get; set; }
        public ulong PollId { get; set; }
        public VotingModel VotingModel { get; set; }
    }
}