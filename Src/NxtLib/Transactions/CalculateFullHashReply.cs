using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Transactions
{
    public class CalculateFullHashReply : BaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString FullHash { get; set; }
    }
}