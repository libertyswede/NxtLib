namespace NxtLib
{
    public class EncryptedData : BaseReply
    {
        public string Data { get; set; }
        public string Nonce { get; set; }
    }
}