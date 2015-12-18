using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;
using NxtLib.VotingSystem;

namespace NxtLib.AccountControl
{
    public class PhasingOnlyControl
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Account { get; set; }
        public string AccountRs { get; set; }
        public int MaxDuration { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        public Amount MaxFees { get; set; }
        public long MinBalance { get; set; }
        public MinBalanceModel MinBalanceModel { get; set; }
        public int MinDuration { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Quorum { get; set; }
        public VotingModel VotingModel { get; set; }
        public IList<WhiteListAddress> WhiteList { get; set; }
    }
}