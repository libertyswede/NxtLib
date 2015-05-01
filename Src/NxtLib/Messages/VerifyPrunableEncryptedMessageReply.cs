using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Messages
{
    public class VerifyPrunableEncryptedMessageReply : BaseReply
    {
        public EncryptedMessage EncryptedMessage { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString EncryptedMessageHash { get; set; }
        public bool Verify { get; set; }
    }
}