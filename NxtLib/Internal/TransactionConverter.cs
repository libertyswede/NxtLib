using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.Transactions;

namespace NxtLib.Internal
{
    /// <summary>
    /// A converter is needed for whole Transaction class in order to split the attachment into different .NET properties 
    /// (atm. Attachment, MessageText, EncryptedMessage, EncryptToSelfMessage & PublicKeyAnnouncement atm.)
    /// </summary>
    internal class TransactionConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var transaction = GetTransactionType(objectType);

            if (reader.TokenType != JsonToken.StartObject)
            {
                throw new ArgumentException("Invalid token type, must be StartObject");
            }

            var jObject = JObject.Load(reader);
            var attachmentJobject = jObject.SelectToken("attachment") as JObject;
            var attachmentConverter = new AttachmentConverter(attachmentJobject);

            var typeByte = GetValueOrDefault(jObject, "type", Convert.ToByte);
            var subTypeByte = GetValueOrDefault(jObject, "subtype", Convert.ToByte);
            var type = TransactionTypeMapper.GetMainType(typeByte);
            var subType = TransactionTypeMapper.GetSubType(typeByte, subTypeByte);

            transaction.Amount = GetValueOrDefault(jObject, "amountNQT", obj => Amount.CreateAmountFromNqt(Convert.ToInt64(obj)));
            transaction.Attachment = attachmentConverter.GetAttachment(subType);
            transaction.BlockId = GetValueOrNull(jObject, "block", Convert.ToUInt64);
            transaction.BlockTimestamp = GetDateTimeOrNull(jObject, "blockTimestamp");
            transaction.Confirmations = GetValueOrNull(jObject, "confirmations", Convert.ToInt32);
            transaction.Deadline = GetValueOrDefault(jObject, "deadline", Convert.ToInt16);
            transaction.EcBlockId = GetValueOrDefault(jObject, "ecBlockId", Convert.ToUInt64);
            transaction.EcBlockHeight = GetValueOrDefault(jObject, "ecBlockHeight", Convert.ToInt32);
            transaction.EncryptedMessage = EncryptedMessage.ParseJson(attachmentJobject);
            transaction.EncryptToSelfMessage = EncryptToSelfMessage.ParseJson(attachmentJobject);
            transaction.FeeNqt = GetValueOrDefault(jObject, "feeNQT", obj => Amount.CreateAmountFromNqt(Convert.ToInt64(obj)));
            transaction.FullHash = GetValueOrDefault(jObject, "fullHash", obj => obj.ToString());
            transaction.Height = GetValueOrDefault(jObject, "height", Convert.ToInt32);
            transaction.Message = Message.ParseJson(attachmentJobject);
            transaction.Recipient = GetValueOrNull(jObject, "recipient", Convert.ToUInt64);
            transaction.RecipientRs = GetValueOrDefault(jObject, "recipientRS", obj => obj.ToString());
            transaction.ReferencedTransactionFullHash = GetValueOrDefault(jObject, "referencedTransactionFullHash", obj => obj.ToString());
            transaction.PublicKeyAnnouncement = PublicKeyAnnouncement.ParseJson(attachmentJobject);
            transaction.Sender = GetValueOrDefault(jObject, "sender", Convert.ToUInt64);
            transaction.SenderRs = GetValueOrDefault(jObject, "senderRS", obj => obj.ToString());
            transaction.SenderPublicKey = GetValueOrDefault(jObject, "senderPublicKey", obj => new BinaryHexString(obj.ToString()));
            transaction.Signature = GetValueOrDefault(jObject, "signature", obj => new BinaryHexString(obj.ToString()));
            transaction.SignatureHash = GetValueOrDefault(jObject, "signatureHash", obj => obj.ToString());
            transaction.SubType = subType;
            transaction.Timestamp = GetDateTimeOrDefault(jObject, "timestamp");
            transaction.TransactionId = GetValueOrNull(jObject, "transaction", Convert.ToUInt64);
            transaction.TransactionIndex = GetValueOrDefault(jObject, "transactionIndex", Convert.ToInt32);
            transaction.Type = type;
            transaction.Version = GetValueOrDefault(jObject, "version", Convert.ToInt32);
            return transaction;
        }

        private static Transaction GetTransactionType(Type objectType)
        {
            Transaction transaction;
            if (objectType == typeof (Transaction))
            {
                transaction = new Transaction();
            }
            else if (objectType == typeof (TransactionReply))
            {
                transaction = new TransactionReply();
            }
            else if (objectType == typeof (ParseTransactionReply))
            {
                transaction = new ParseTransactionReply();
            }
            else
            {
                throw new ArgumentException("Can only convert to transaction object");
            }
            return transaction;
        }

        private static T GetValueOrDefault<T>(JToken jToken, string tokenName, Func<object, T> convertFunc)
        {
            JValue jValue;
            return (jValue = jToken.SelectToken(tokenName) as JValue) != null ? convertFunc(jValue.Value) : default(T);
        }

        private static T? GetValueOrNull<T>(JToken jToken, string tokenName, Func<object, T> convertFunc) where T : struct
        {
            JValue jValue;
            return (jValue = jToken.SelectToken(tokenName) as JValue) != null ? convertFunc(jValue.Value) : (T?) null;
        }

        private static DateTime GetDateTimeOrDefault(JToken jToken, string tokenName)
        {
            var value = GetValueOrNull(jToken, tokenName, Convert.ToInt32);
            return value.HasValue ? new DateTimeConverter().GetFromNxtTime(value.Value) : new DateTime();
        }

        private static DateTime? GetDateTimeOrNull(JToken jToken, string tokenName)
        {
            var value = GetValueOrNull(jToken, tokenName, Convert.ToInt32);
            return value.HasValue ? new DateTimeConverter().GetFromNxtTime(value.Value) : (DateTime?) null;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
