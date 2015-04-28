namespace NxtLib.ServerInfo
{
    public class GetBlockchainStatusReply : BlockchainStatus, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}