using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Transactions
{
    public class BroadcastTransactionReply : CalculateFullHashReply
    {
        [JsonProperty(PropertyName = "transaction")]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong TransactionId { get; set; }
    }
}