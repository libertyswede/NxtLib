using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.ArbitraryMessageOperations
{
    public class ReadMessageReply : BaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public UnencryptedMessage DecryptedMessage { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public UnencryptedMessage DecryptedMessageToSelf { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public UnencryptedMessage Message { get; set; }
    }
}