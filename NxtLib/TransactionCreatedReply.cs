using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    public class TransactionCreatedReply : BaseReply
    {
        public bool Broadcasted { get; set; }
        public string FullHash { get; set; }
        public string SignatureHash { get; set; }

        [JsonProperty(PropertyName = "transactionJSON")]
        public Transaction Transaction { get; set; }
        public string TransactionBytes { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "transaction")]
        public ulong? TransactionId { get; set; }
        public string UnsignedTransactionBytes { get; set; }
    }
}