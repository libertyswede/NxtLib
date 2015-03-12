using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.Internal.LocalSign;
using AlreadyEncryptedMessage = NxtLib.CreateTransactionParameters.AlreadyEncryptedMessage;

namespace NxtLib.Local
{
    public class LocalCrypto : ILocalCrypto
    {
        private readonly Crypto _crypto = new Crypto();

        public BinaryHexString GetPublicKey(string secretPhrase)
        {
            return _crypto.GetPublicKey(secretPhrase);
        }

        public ulong GetAccountIdFromPublicKey(BinaryHexString publicKey)
        {
            return _crypto.GetAccountIdFromPublicKey(publicKey);
        }

        public string GetReedSolomonFromAccountId(ulong accountId)
        {
            return ReedSolomon.Encode(accountId);
        }

        public ulong GetAccountIdFromReedSolomon(string reedSolomonAddress)
        {
            return ReedSolomon.Decode(reedSolomonAddress);
        }

        public AlreadyEncryptedMessage AesEncryptMessage(string message, string secretPhrase,
            BinaryHexString recipientPublicKey, out IEnumerable<byte> nonce)
        {
            var encryptedMessage = AesEncryptMessage(ByteToHexStringConverter.ToBytes(message), secretPhrase,
                recipientPublicKey, out nonce);
            encryptedMessage.MessageIsText = true;
            return encryptedMessage;
        }

        public AlreadyEncryptedMessage AesEncryptMessage(string message, string secretPhrase, BinaryHexString recipientPublicKey,
            IEnumerable<byte> nonce)
        {
            var encryptedMessage = AesEncryptMessage(ByteToHexStringConverter.ToBytes(message), secretPhrase, recipientPublicKey, nonce);
            encryptedMessage.MessageIsText = true;
            return encryptedMessage;
        }

        public AlreadyEncryptedMessage AesEncryptMessage(IEnumerable<byte> message, string secretPhrase,
            BinaryHexString recipientPublicKey, out IEnumerable<byte> nonce)
        {
            nonce = _crypto.GenerateNonceBytes();
            return AesEncryptMessage(message, secretPhrase, recipientPublicKey, nonce);
        }

        public AlreadyEncryptedMessage AesEncryptMessage(IEnumerable<byte> message, string secretPhrase,
            BinaryHexString recipientPublicKey, IEnumerable<byte> nonce)
        {
            var nonceArray = nonce.ToArray();
            var privateKey = _crypto.GetPrivateKey(secretPhrase);
            var encryptedBytes = _crypto.AesEncrypt(message.ToArray(), privateKey,
                recipientPublicKey.ToBytes().ToArray(), nonceArray);
            var encryptedMessage = new AlreadyEncryptedMessage(encryptedBytes, nonceArray, false);
            return encryptedMessage;
        }

        public JObject SignTransaction(TransactionCreatedReply transactionCreatedReply, string secretPhrase)
        {
            var transaction = transactionCreatedReply.Transaction;
            var unsignedBytes = transactionCreatedReply.UnsignedTransactionBytes.ToBytes().ToArray();
            var referencedTransactionFullHash = transaction.ReferencedTransactionFullHash != null
                ? transaction.ReferencedTransactionFullHash.ToHexString()
                : "";
            var attachment = JObject.Parse(transactionCreatedReply.RawJsonReply).SelectToken("transactionJSON['attachment']");
            var signature = new BinaryHexString(_crypto.Sign(unsignedBytes, secretPhrase));
            return BuildSignedTransaction(transaction, referencedTransactionFullHash, signature, attachment);
        }

        private static JObject BuildSignedTransaction(Transaction transaction, string referencedTransactionFullHash,
            BinaryHexString signature, JToken attachment)
        {
            var resultJson = new JObject();
            resultJson.Add("type", (int)TransactionTypeMapper.GetMainTypeByte(transaction.Type));
            resultJson.Add("subtype", (int)TransactionTypeMapper.GetSubTypeByte(transaction.SubType));
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
            if (transaction.Recipient.HasValue)
            {
                resultJson.Add("recipient", transaction.Recipient.ToString());
            }
            return resultJson;
        }
    }
}
