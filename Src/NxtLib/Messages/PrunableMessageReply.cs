namespace NxtLib.Messages
{
    public class PrunableMessageReply : PrunableMessage, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}