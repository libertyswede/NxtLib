using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    public class EncryptedDataReply : BaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Data { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Nonce { get; set; }
    }
}