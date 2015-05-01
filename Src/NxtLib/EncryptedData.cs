using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    public class EncryptedData
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Data { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Nonce { get; set; }
    }
}