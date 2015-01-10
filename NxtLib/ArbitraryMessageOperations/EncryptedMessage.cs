namespace NxtLib.ArbitraryMessageOperations
{
    public class EncryptedMessage : BaseReply
    {
        public string Data { get; set; }
        public string Nonce { get; set; }
    }
}