using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Transactions
{
    public class SignTransactionReply : BaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString FullHash { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString SignatureHash { get; set; }

        [JsonProperty(PropertyName = "transactionJSON")]
        [JsonConverter(typeof(TransactionConverter))]
        public Transaction Transaction { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString TransactionBytes { get; set; }

        [JsonProperty(PropertyName = "transaction")]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong TransactionId { get; set; }
        public bool Verify { get; set; }
    }
}