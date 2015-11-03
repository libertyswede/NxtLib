using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.Internal.LocalSign;
using System;

namespace NxtLib.Local
{
    public class LocalCrypto : ILocalCrypto
    {
        private readonly Crypto _crypto = new Crypto();
        private readonly Compressor _compressor = new Compressor();

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

        public JObject SignTransaction(TransactionCreatedReply transactionCreatedReply, string secretPhrase)
        {
            var transaction = transactionCreatedReply.Transaction;
            var unsignedBytes = transactionCreatedReply.UnsignedTransactionBytes.ToBytes().ToArray();
            var referencedTransactionFullHash = transaction.ReferencedTransactionFullHash != null
                ? transaction.ReferencedTransactionFullHash.ToHexString()
                : "";
            var attachment = JObject.Parse(transactionCreatedReply.RawJsonReply).SelectToken($"{Parameters.TransactionJson}['{Parameters.Attachment}']");
            var signature = new BinaryHexString(_crypto.Sign(unsignedBytes, secretPhrase));
            return BuildSignedTransaction(transaction, referencedTransactionFullHash, signature, attachment);
        }

        public byte[] CreateNonce()
        {
            var nonce = new byte[32];
            var random = RandomNumberGenerator.Create();
            random.GetBytes(nonce);
            return nonce;
        }

        // Sample here: http://stackoverflow.com/questions/29701401/encrypt-string-with-bouncy-castle-aes-cbc-pkcs7
        public BinaryHexString EncryptTextTo(BinaryHexString recipientPublicKey, string message, byte[] nonce, bool compress, string secretPhrase)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            return EncryptDataTo(recipientPublicKey, messageBytes, nonce, compress, secretPhrase);
        }
        
        public BinaryHexString EncryptDataTo(BinaryHexString recipientPublicKey, BinaryHexString data, byte[] nonce, bool compress, string secretPhrase)
        {
            var recipientPublicKeyBytes = recipientPublicKey.ToBytes().ToArray();
            var dataBytes = data.ToBytes().ToArray();
            if (compress)
            {
                dataBytes = _compressor.GzipCompress(dataBytes);
            }
            return _crypto.AesEncryptTo(recipientPublicKeyBytes, dataBytes, nonce, secretPhrase);
        }
        
        public string DecryptTextFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompressDecryptedMessage, string secretPhrase)
        {
            throw new NotImplementedException();
        }

        public byte[] DecryptDataFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompressDecryptedMessage, string secretPhrase)
        {
            throw new NotImplementedException();
        }
        
        private static JObject BuildSignedTransaction(Transaction transaction, string referencedTransactionFullHash,
            BinaryHexString signature, JToken attachment)
        {
            var resultJson = new JObject();
            resultJson.Add(Parameters.Type, (int)TransactionTypeMapper.GetMainTypeByte(transaction.Type));
            resultJson.Add(Parameters.SubType, (int)TransactionTypeMapper.GetSubTypeByte(transaction.SubType));
            resultJson.Add(Parameters.Timestamp, DateTimeConverter.GetNxtTime(transaction.Timestamp));
            resultJson.Add(Parameters.Deadline, transaction.Deadline);
            resultJson.Add(Parameters.SenderPublicKey, transaction.SenderPublicKey.ToHexString());
            resultJson.Add(Parameters.AmountNqt, transaction.Amount.Nqt.ToString());
            resultJson.Add(Parameters.FeeNqt, transaction.Fee.Nqt.ToString());
            if (!string.IsNullOrEmpty(referencedTransactionFullHash))
            {
                resultJson.Add(Parameters.ReferencedTransactionFullHash, referencedTransactionFullHash);
            }
            resultJson.Add(Parameters.Signature, signature.ToHexString());
            resultJson.Add(Parameters.Version, transaction.Version);
            if (attachment != null && attachment.Children().Any())
            {
                resultJson.Add(Parameters.Attachment, attachment);
            }
            resultJson.Add(Parameters.EcBlockHeight, transaction.EcBlockHeight);
            resultJson.Add(Parameters.EcBlockId, transaction.EcBlockId.ToString());
            if (transaction.Recipient.HasValue)
            {
                resultJson.Add(Parameters.Recipient, transaction.Recipient.ToString());
            }
            return resultJson;
        }
    }
}