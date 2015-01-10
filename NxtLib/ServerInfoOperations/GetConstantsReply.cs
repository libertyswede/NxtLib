using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.ServerInfoOperations
{
    public class GetConstantsReply : BaseReply
    {
        public int MaxArbitraryMessageLength { get; set; }
        public int MaxBlockPayloadLength { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong GenesisAccountId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong GenesisBlockId { get; set; }
        public List<PeerState> PeerStates { get; set; }
        public List<TransactionType> TransactionTypes { get; set; }
    }
}