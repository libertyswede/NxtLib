using System.Linq;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.Internal.LocalSign;

namespace NxtLib.Local
{
    public class LocalCrypto : ILocalCrypto
    {
        private readonly Crypto _crypto = new Crypto();

        public BinaryHexString GetPublicKey(string secretPhrase)
        {
            return _crypto.GetPublicKey(secretPhrase);
        }

        public JObject SignTransaction(TransactionCreatedReply transactionCreatedReply, string secretPhrase)
        {
            var transaction = transactionCreatedReply.Transaction;
            var unsignedBytes = transactionCreatedReply.UnsignedTransactionBytes.ToBytes().ToArray();
            var referencedTransactionFullHash = transaction.ReferencedTransactionFullHash != null
                ? transaction.ReferencedTransactionFullHash.ToHexString()
                : "";
            var attachment = JObject.Parse(transactionCreatedReply.RawJsonReply).SelectToken("attachment");
            var signature = new BinaryHexString(_crypto.Sign(unsignedBytes, secretPhrase));
            return BuildSignedTransaction(transaction, referencedTransactionFullHash, signature, attachment);
        }

        private static JObject BuildSignedTransaction(Transaction transaction, string referencedTransactionFullHash,
            BinaryHexString signature, JToken attachment)
        {
            var resultJson = new JObject();
            resultJson.Add("type", TransactionTypeMapper.GetMainTypeByte(transaction.Type));
            resultJson.Add("subtype", TransactionTypeMapper.GetSubTypeByte(transaction.SubType));
            resultJson.Add("timestamp", DateTimeConverter.GetNxtTime(transaction.Timestamp));
            resultJson.Add("deadline", transaction.Deadline);
            resultJson.Add("senderPublicKey", transaction.SenderPublicKey.ToHexString());
            resultJson.Add("amountNQT", transaction.Amount.Nqt.ToString());
            resultJson.Add("feeNQT", transaction.Fee.Nqt.ToString());
            if (!string.IsNullOrEmpty(referencedTransactionFullHash))
            {
                resultJson.Add("referencedTransactionFullHash", referencedTransactionFullHash);
            }
            resultJson.Add("signature", signature.ToHexString());
            resultJson.Add("version", transaction.Version);
            if (attachment != null && attachment.Children().Any())
            {
                resultJson.Add("attachment", attachment);
            }
            resultJson.Add("ecBlockHeight", transaction.EcBlockHeight);
            resultJson.Add("ecBlockId", transaction.EcBlockId.ToString());
            resultJson.Add("recipient", transaction.Recipient.ToString());
            return resultJson;
        }
    }
}
