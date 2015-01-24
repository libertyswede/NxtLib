using System;
using Newtonsoft.Json.Linq;

namespace NxtLib
{
    public abstract class Appendix : Attachment
    {
    }

    public class Message : Appendix
    {
        public bool MessageIsText { get; set; }
        public UnencryptedMessage MessageText { get; set; }

        private Message(string messageText, bool messageIsText)
        {
            MessageText = new UnencryptedMessage(messageText);
            MessageIsText = messageIsText;
        }

        internal static Message ParseJson(JObject jObject)
        {
            JValue messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(MessageKey) as JValue) == null)
            {
                return null;
            }

            var messageIsText = Convert.ToBoolean(jObject.SelectToken(MessageIsTextKey));
            return new Message(messageToken.Value.ToString(), messageIsText);
        }
    }

    public abstract class EncryptedMessageBase : Appendix
    {
        public bool IsText { get; set; }
        public BinaryHexString Nonce { get; set; }
        public BinaryHexString Data { get; set; }

        protected EncryptedMessageBase(JToken messageToken)
        {
            IsText = Convert.ToBoolean(messageToken.SelectToken(IsTextKey));
            Nonce = new BinaryHexString(((JValue)messageToken.SelectToken(NonceKey)).Value.ToString());
            Data = new BinaryHexString(((JValue)messageToken.SelectToken(DataKey)).Value.ToString());
        }
    }

    public class EncryptedMessage : EncryptedMessageBase
    {
        private EncryptedMessage(JToken messageToken)
            : base(messageToken)
        {
        }

        internal static EncryptedMessage ParseJson(JObject jObject)
        {
            JToken messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(EncryptedMessageKey)) == null)
            {
                return null;
            }
            return new EncryptedMessage(messageToken);
        }
    }

    public class EncryptToSelfMessage : EncryptedMessageBase
    {
        private EncryptToSelfMessage(JToken messageToken)
            : base(messageToken)
        {
        }

        internal static EncryptToSelfMessage ParseJson(JObject jObject)
        {
            JToken messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(EncryptToSelfMessageKey)) == null)
            {
                return null;
            }
            return new EncryptToSelfMessage(messageToken);
        }
    }

    public class PublicKeyAnnouncement : Appendix
    {
        public string RecipientPublicKey { get; set; }

        private PublicKeyAnnouncement(string recipientPublicKey)
        {
            RecipientPublicKey = recipientPublicKey;
        }

        internal static PublicKeyAnnouncement ParseJson(JObject jObject)
        {
            JValue announcement;
            if (jObject == null || (announcement = jObject.SelectToken(RecipientPublicKeyKey) as JValue) == null)
            {
                return null;
            }
            return new PublicKeyAnnouncement(announcement.Value.ToString());
        }
    }
}
