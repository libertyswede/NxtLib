using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace NxtLib.Internal.LocalSign
{
    internal class TransactionBuilder
    {
        const byte Version = 1;

        // Experimental code
        public string BuildSendMoney(CreateTransactionBySecretPhrase parameters,
            string recipient, Amount amount, int ecBlockHeight, ulong ecBlockId)
        {
            var recipientId = GetRecipientId(recipient);

            var unsignedBytes = GetUnsignedTransactionBytes(parameters, recipientId, amount, ecBlockHeight, ecBlockId);
            var crypto = new Crypto();
            var signature = new BinaryHexString(crypto.Sign(unsignedBytes, parameters.SecretPhrase));
            var referencedTransactionFullHash = parameters.ReferencedTransactionFullHash != null
                ? parameters.ReferencedTransactionFullHash.ToHexString()
                : "";
            
            var jObject = new JObject();
            jObject.Add("type", (byte)TransactionMainType.Payment);
            jObject.Add("subtype", (byte) TransactionSubType.PaymentOrdinaryPayment);
            jObject.Add("timestamp", DateTimeConverter.GetNxtTime(DateTime.UtcNow));
            jObject.Add("deadline", parameters.Deadline);
            jObject.Add("senderPublicKey", crypto.GetPublicKey(parameters.SecretPhrase).ToHexString());
            jObject.Add("amountNQT", amount.Nqt.ToString());
            jObject.Add("feeNQT", parameters.Fee.Nqt.ToString());
            if (!string.IsNullOrEmpty(referencedTransactionFullHash))
            {
                jObject.Add("referencedTransactionFullHash", referencedTransactionFullHash);
            }
            jObject.Add("signature", signature.ToHexString());
            jObject.Add("version", 1);
            jObject.Add("ecBlockHeight", ecBlockHeight);
            jObject.Add("ecBlockId", ecBlockId.ToString());
            jObject.Add("recipient", recipientId.ToString());
            return jObject.ToString();
        }

        private static ulong GetRecipientId(string recipient)
        {
            ulong recipientId;
            if (!UInt64.TryParse(recipient, out recipientId))
            {
                recipientId = ReedSolomon.Decode(recipient);
            }
            return recipientId;
        }

        private static byte[] GetUnsignedTransactionBytes(CreateTransactionBySecretPhrase parameters, ulong recipient,
            Amount amount, int ecBlockHeight, ulong ecBlockId)
        {
            using (var memoryStream = new MemoryStream())
            {
                var crypto = new Crypto();

                var referencedTransactionFullHash = parameters.ReferencedTransactionFullHash != null
                    ? parameters.ReferencedTransactionFullHash.ToBytes().ToArray()
                    : new byte[32];
                var senderPublicKey = crypto.GetPublicKey(parameters.SecretPhrase).ToBytes().ToArray();


                memoryStream.Write(new[] {(byte) TransactionMainType.Payment}, 0, 1);
                memoryStream.Write(new[] {(byte) ((Version << 4) | (byte) TransactionSubType.PaymentOrdinaryPayment)}, 0, 1);
                memoryStream.Write(BitConverter.GetBytes(DateTimeConverter.GetNxtTime(DateTime.UtcNow)), 0, 4);
                memoryStream.Write(BitConverter.GetBytes(parameters.Deadline), 0, 2);
                memoryStream.Write(senderPublicKey, 0, senderPublicKey.Length);
                memoryStream.Write(BitConverter.GetBytes(recipient), 0, 8);
                memoryStream.Write(BitConverter.GetBytes(amount.Nqt), 0, 8);
                memoryStream.Write(BitConverter.GetBytes(parameters.Fee.Nqt), 0, 8);
                memoryStream.Write(referencedTransactionFullHash, 0, 32);
                memoryStream.Write(new byte[64], 0, 64);
                memoryStream.Write(BitConverter.GetBytes(0), 0, 4);
                memoryStream.Write(BitConverter.GetBytes(ecBlockHeight), 0, 4);
                memoryStream.Write(BitConverter.GetBytes(ecBlockId), 0, 8);

                return memoryStream.ToArray();
            }
        }
    }
}
