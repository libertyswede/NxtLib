namespace NxtLib.Crypto
{
    internal interface ICrypto
    {
        byte[] Hash { get; }
        byte[] ComputeHash(byte[] value);
        BinaryHexString GetPublicKey(string secretPhrase);
    }
}
