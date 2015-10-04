using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Utils
{
    public class HashReply : BaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Hash { get; set; }
    }
}