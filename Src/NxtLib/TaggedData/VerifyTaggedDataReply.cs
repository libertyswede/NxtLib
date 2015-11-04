using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.TaggedData
{
    public class VerifyTaggedDataReply : BaseReply
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Hash { get; set; }
        public bool Verify { get; set; }

        [JsonProperty(Parameters.VersionTaggedDataUpload)]
        public int TaggedDataUploadVersion { get; set; }
    }
}