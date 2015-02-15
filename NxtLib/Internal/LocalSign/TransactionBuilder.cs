using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace NxtLib.Internal.LocalSign
{
    public class TransactionBuilder
    {
        // Experimental code
        public string CreateSignedTransaction(TransactionCreatedReply transactionCreatedReply, string secretPhrase)
        {
            var crypto = new Crypto();
            var transaction = transactionCreatedReply.Transaction;
            var unsignedBytes = transactionCreatedReply.UnsignedTransactionBytes.ToBytes().ToArray();
            var signature = new BinaryHexString(crypto.Sign(unsignedBytes, secretPhrase));
            var referencedTransactionFullHash = transaction.ReferencedTransactionFullHash != null
                ? transaction.ReferencedTransactionFullHash.ToHexString()
                : "";
            var transactionCreatedJson = JObject.Parse(transactionCreatedReply.RawJsonReply);
            var attachment = transactionCreatedJson.SelectToken("attachment");
            
            var resultJson = new JObject();
            resultJson.Add("type", (byte) transaction.Type);
            resultJson.Add("subtype", (byte) transaction.SubType);
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

            return resultJson.ToString();
        }
    }
}
