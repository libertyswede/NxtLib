using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Messages
{
    public class VerifyPrunableMessageReply : BaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString MessageHash { get; set; }
        public bool Verify { get; set; }
    }
}