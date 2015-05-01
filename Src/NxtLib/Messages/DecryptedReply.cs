using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Messages
{
    public class DecryptedReply : BaseReply
    {
        [JsonProperty(PropertyName = "decryptedMessage")]
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public IEnumerable<byte> Data { get; set; }
    }
}