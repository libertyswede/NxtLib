namespace NxtLib.TaggedData
{
    public class VerifyTaggedDataReply : TaggedDataUploadAttachment, IBaseReply
    {
        public string RequestUri { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RawJsonReply { get; set; }
        public bool Verify { get; set; }
    }
}