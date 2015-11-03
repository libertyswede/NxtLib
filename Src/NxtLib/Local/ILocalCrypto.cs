using Newtonsoft.Json.Linq;

namespace NxtLib.Local
{
    public interface ILocalCrypto
    {
        BinaryHexString GetPublicKey(string secretPhrase);

        JObject SignTransaction(TransactionCreatedReply transactionCreatedReply, string secretPhrase);

        ulong GetAccountIdFromPublicKey(BinaryHexString publicKey);

        string GetReedSolomonFromAccountId(ulong accountId);

        ulong GetAccountIdFromReedSolomon(string reedSolomonAddress);
        BinaryHexString EncryptTo(BinaryHexString recipientPublicKey, string message, byte[] nonce, bool compress, string secretPhrase);
        BinaryHexString EncryptTo(BinaryHexString recipientPublicKey, byte[] data, byte[] nonce, bool compress, string secretPhrase);
    }
}