using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.ServerInfo
{
    public class GetConstantsReply : BaseReply
    {
        public List<object> CurrencyTypes { get; set; }
        public int MaxArbitraryMessageLength { get; set; }
        public int MaxBlockPayloadLength { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong GenesisAccountId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong GenesisBlockId { get; set; }
        public List<object> HashAlgorithms { get; set; }
        public List<PeerState> PeerStates { get; set; }
        public List<TransactionType> TransactionTypes { get; set; }
    }
}