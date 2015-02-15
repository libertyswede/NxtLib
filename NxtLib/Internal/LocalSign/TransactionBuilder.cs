using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace NxtLib.Internal.LocalSign
{
    internal class TransactionBuilder
    {
        // Experimental code
        public string CreateSignedTransaction(Transaction transaction, string secretPhrase)
        {
            var crypto = new Crypto();
            var unsignedBytes = GetUnsignedTransactionBytes(transaction);

            var signature = new BinaryHexString(crypto.Sign(unsignedBytes, secretPhrase));
            var referencedTransactionFullHash = transaction.ReferencedTransactionFullHash != null
                ? transaction.ReferencedTransactionFullHash.ToHexString()
                : "";
            
            var jObject = new JObject();
            jObject.Add("type", (byte)  transaction.Type);
            jObject.Add("subtype", (byte) transaction.SubType);
            jObject.Add("timestamp", DateTimeConverter.GetNxtTime(transaction.Timestamp));
            jObject.Add("deadline", transaction.Deadline);
            jObject.Add("senderPublicKey", transaction.SenderPublicKey.ToHexString());
            jObject.Add("amountNQT", transaction.Amount.Nqt.ToString());
            jObject.Add("feeNQT", transaction.Fee.Nqt.ToString());
            if (!string.IsNullOrEmpty(referencedTransactionFullHash))
            {
                jObject.Add("referencedTransactionFullHash", referencedTransactionFullHash);
            }
            jObject.Add("signature", signature.ToHexString());
            jObject.Add("version", transaction.Version);
            jObject.Add("ecBlockHeight", transaction.EcBlockHeight);
            jObject.Add("ecBlockId", transaction.EcBlockId.ToString());
            jObject.Add("recipient", GetRecipientId(transaction).ToString());

            return jObject.ToString();
        }

        private static byte[] GetUnsignedTransactionBytes(Transaction transaction)
        {
            using (var memoryStream = new MemoryStream())
            {
                var referencedTransactionFullHash = transaction.ReferencedTransactionFullHash != null
                    ? transaction.ReferencedTransactionFullHash.ToBytes().ToArray()
                    : new byte[32];
                var senderPublicKey = transaction.SenderPublicKey.ToBytes().ToArray();
                
                memoryStream.Write(new[] {(byte) transaction.Type}, 0, 1);
                memoryStream.Write(new[] {(byte) ((transaction.Version << 4) | (byte) transaction.SubType)}, 0, 1);
                memoryStream.Write(BitConverter.GetBytes(DateTimeConverter.GetNxtTime(transaction.Timestamp)), 0, 4);
                memoryStream.Write(BitConverter.GetBytes(transaction.Deadline), 0, 2);
                memoryStream.Write(senderPublicKey, 0, senderPublicKey.Length);
                memoryStream.Write(BitConverter.GetBytes(GetRecipientId(transaction)), 0, 8);
                memoryStream.Write(BitConverter.GetBytes(transaction.Amount.Nqt), 0, 8);
                memoryStream.Write(BitConverter.GetBytes(transaction.Fee.Nqt), 0, 8);
                memoryStream.Write(referencedTransactionFullHash, 0, 32);
                memoryStream.Write(new byte[64], 0, 64);
                memoryStream.Write(BitConverter.GetBytes(GetFlags(transaction)), 0, 4);
                memoryStream.Write(BitConverter.GetBytes(transaction.EcBlockHeight), 0, 4);
                memoryStream.Write(BitConverter.GetBytes(transaction.EcBlockId), 0, 8);
                var appendixBytes = GetAttachmentBytes(transaction);
                memoryStream.Write(appendixBytes, 0, appendixBytes.Length);

                return memoryStream.ToArray();
            }
        }

        private static byte[] GetAttachmentBytes(Transaction transaction)
        {
            using (var stream = new MemoryStream())
            {
                var appendixes = new List<Appendix>
                {
                    transaction.Attachment,
                    transaction.Message,
                    transaction.EncryptedMessage,
                    transaction.PublicKeyAnnouncement,
                    transaction.EncryptToSelfMessage
                };

                appendixes.Where(a => a != null).ToList().ForEach(a => a.PutBytes(stream));
                return stream.ToArray();
            }
        }

        private static int GetFlags(Transaction transaction)
        {
            var flags = 0;
            var position = 1;
            if (transaction.Message != null)
            {
                flags |= position;
            }

            position <<= 1;
            if (transaction.EncryptedMessage != null)
            {
                flags |= position;
            }

            position <<= 1;
            if (transaction.PublicKeyAnnouncement != null)
            {
                flags |= position;
            }

            position <<= 1;
            if (transaction.EncryptToSelfMessage != null)
            {
                flags |= position;
            }

            return flags;
        }

        private static ulong GetRecipientId(Transaction transaction)
        {
            if (transaction.SubType == TransactionSubType.PaymentOrdinaryPayment)
            {
                if (transaction.Recipient.HasValue)
                {
                    return transaction.Recipient.Value;
                }
                throw new ArgumentException("Transaction recipient must have a value for type: " + transaction.SubType);
            }
            throw new ArgumentException(string.Format("Transaction type {0} is not supported", transaction.SubType));
        }
    }
}
