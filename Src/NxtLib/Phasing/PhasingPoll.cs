using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;
using NxtLib.VotingSystem;

namespace NxtLib.Phasing
{
    public class PhasingPoll
    {
        [JsonProperty(PropertyName = Parameters.Account)]
        public ulong AcountId { get; set; }
        public string AccountRs { get; set; }
        public bool Finished { get; set; }
        public int FinishHeight { get; set; }
        public string HashedSecret { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Holding)]
        public ulong HoldingId { get; set; }
        public long Quorum { get; set; }
        public long MinBalance { get; set; }
        public MinBalanceModel MinBalanceModel { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Result { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Transaction)]
        public ulong TransactionId { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString TransactionFullHash { get; set; }
        public VotingModel VotingModel { get; set; }
        public List<PhasingPollWhiteList> WhiteList { get; set; }
    }
}