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
        BinaryHexString CreateNonce();
        BinaryHexString EncryptTextTo(BinaryHexString recipientPublicKey, string message, BinaryHexString nonce, bool compress, string secretPhrase);
        BinaryHexString EncryptDataTo(BinaryHexString recipientPublicKey, BinaryHexString data, BinaryHexString nonce, bool compress, string secretPhrase);
        string DecryptTextFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompress, string secretPhrase);
        byte[] DecryptDataFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompress, string secretPhrase);
    }
}