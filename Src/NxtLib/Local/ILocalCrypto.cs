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
    }
}