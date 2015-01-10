namespace NxtLib.BlockOperations
{
    public class GetBlockReply<T> : Block<T>, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}