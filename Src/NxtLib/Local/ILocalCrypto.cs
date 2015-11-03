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
        BinaryHexString EncryptTextTo(BinaryHexString recipientPublicKey, string message, byte[] nonce, bool compress, string secretPhrase);
        BinaryHexString EncryptDataTo(BinaryHexString recipientPublicKey, BinaryHexString data, byte[] nonce, bool compress, string secretPhrase);
        string DecryptTextFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompressDecryptedMessage, string secretPhrase);
        byte[] DecryptDataFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompressDecryptedMessage, string secretPhrase);
    }
}