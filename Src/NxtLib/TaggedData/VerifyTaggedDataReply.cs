using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.TaggedData
{
    public class VerifyTaggedDataReply : ITaggedData, IBaseReply
    {
        public string Channel { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Hash { get; set; }
        public bool IsText { get; set; }
        public string Name { get; set; }
        public IEnumerable<KeyValuePair<string, string>> PostData { get; set; }
        public string RequestUri { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RawJsonReply { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public bool Verify { get; set; }

        [JsonProperty("version.TaggedDataUpload")]
        public int TaggedDataUploadVersion { get; set; }
    }
}