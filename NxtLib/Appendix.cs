using System;
using Newtonsoft.Json.Linq;

namespace NxtLib
{
    public abstract class Appendix : Attachment
    {
    }

    public class UnencryptedMessage : Appendix
    {
        public bool MessageIsText { get; set; }
        public string Message { get; set; }

        private UnencryptedMessage(string message, bool messageIsText)
        {
            Message = message;
            MessageIsText = messageIsText;
        }

        internal static UnencryptedMessage ParseJson(JObject jObject)
        {
            JValue messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(MessageKey) as JValue) == null)
            {
                return null;
            }

            var messageIsText = Convert.ToBoolean(jObject.SelectToken(MessageIsTextKey));
            return new UnencryptedMessage(messageToken.Value.ToString(), messageIsText);
        }
    }

    public abstract class EncryptedMessageBase : Appendix
    {
        public bool IsText { get; set; }
        public string Nonce { get; set; }
        public string Data { get; set; }

        protected EncryptedMessageBase(JToken messageToken)
        {
            IsText = Convert.ToBoolean(messageToken.SelectToken(IsTextKey));
            Nonce = ((JValue) messageToken.SelectToken(NonceKey)).Value.ToString();
            Data = ((JValue) messageToken.SelectToken(DataKey)).Value.ToString();
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
