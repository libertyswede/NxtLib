namespace NxtLib.Internal.LocalSign
{
    internal interface ICrypto
    {
        byte[] ComputeHash(byte[] value);
        BinaryHexString GetPublicKey(string secretPhrase);
        byte[] Sign(byte[] message, string secretPhrase);
    }
}
