namespace NxtLib.TaggedData
{
    public class TaggedDataReply : TaggedData, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}