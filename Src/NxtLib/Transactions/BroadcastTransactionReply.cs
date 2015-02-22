using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Transactions
{
    public class BroadcastTransactionReply : BaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString FullHash { get; set; }

        [JsonProperty(PropertyName = "transaction")]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong TransactionId { get; set; }
    }
}