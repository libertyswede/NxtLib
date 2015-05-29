using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.TaggedData
{
    public class VerifyTaggedDataReply : TaggedData, IBaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Hash { get; set; }

        public string RequestUri { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RawJsonReply { get; set; }
        public bool Verify { get; set; }
    }
}