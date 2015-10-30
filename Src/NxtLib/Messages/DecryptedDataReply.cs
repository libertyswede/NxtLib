using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Messages
{
    public class DecryptedDataReply : BaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        [JsonProperty(PropertyName = Parameters.DecryptedMessage)]
        public BinaryHexString Data { get; set; }
    }
}