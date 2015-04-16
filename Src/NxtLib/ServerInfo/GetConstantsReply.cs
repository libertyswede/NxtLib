using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.ServerInfo
{
    public class GetConstantsReply : BaseReply
    {
        [JsonConverter(typeof(DictionaryConverter<string, long>))]
        public Dictionary<string, long> CurrencyTypes { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong GenesisAccountId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong GenesisBlockId { get; set; }

        [JsonConverter(typeof(DictionaryConverter<string, long>))]
        public Dictionary<string, long> HashAlgorithms { get; set; }
        public int MaxArbitraryMessageLength { get; set; }
        public int MaxBlockPayloadLength { get; set; }

        [JsonConverter(typeof(DictionaryConverter<string, long>))]
        public Dictionary<string, long> MinBalanceModels { get; set; }

        [JsonConverter(typeof(DictionaryConverter<string, long>))]
        public Dictionary<string, long> PeerStates { get; set; }

        [JsonConverter(typeof(TransactionTypesConverter))]
        public Dictionary<int, Dictionary<int, TransactionType>> TransactionTypes { get; set; }

        [JsonConverter(typeof(DictionaryConverter<string, long>))]
        public Dictionary<string, long> VotingModels { get; set; }
    }
}