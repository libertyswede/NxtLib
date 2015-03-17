namespace NxtExchange
{
    public class ExchangeSettings
    {
        public string SecretPhrase { get; private set; }
        public string NxtAddress { get; private set; }
        public string PublicKey { get; private set; }
        public ulong LastSecureBlockId { get; private set; }

        public ExchangeSettings(string secretPhrase, string nxtAddress, string publicKey, ulong lastSecureBlockId)
        {
            SecretPhrase = secretPhrase;
            NxtAddress = nxtAddress;
            PublicKey = publicKey;
            LastSecureBlockId = lastSecureBlockId;
        }
    }
}
