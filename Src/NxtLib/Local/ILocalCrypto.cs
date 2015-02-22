using Newtonsoft.Json.Linq;

namespace NxtLib.Local
{
    public interface ILocalCrypto
    {
        BinaryHexString GetPublicKey(string secretPhrase);
        JObject SignTransaction(TransactionCreatedReply transactionCreatedReply, string secretPhrase);
    }
}