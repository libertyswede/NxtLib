using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Transactions
{
    public class SignTransactionReply : BaseReply
    {
        public string FullHash { get; set; }
        public string SignatureHash { get; set; }
        public string TransactionBytes { get; set; }

        [JsonProperty(PropertyName = "transaction")]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong TransactionId { get; set; }
        public bool Verify { get; set; }
    }
}