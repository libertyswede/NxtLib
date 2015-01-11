using System;
using System.Collections.Generic;

namespace NxtLib
{
    public abstract class Appendix : Attachment
    {
        protected Appendix(IReadOnlyDictionary<string, object> values, string name) 
            : base(values, name)
        {
        }
    }

    public class UnencryptedMessage : Appendix
    {
        public bool MessageIsText { get; set; }
        public string Message { get; set; }

        internal const string AttachmentName = "version.Message";

        public UnencryptedMessage(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            MessageIsText = Convert.ToBoolean(values["messageIsText"]);
            Message = values["message"].ToString();
        }
    }

    public abstract class EncryptedMessageBase : Appendix
    {
        public bool IsText { get; set; }
        public string Nonce { get; set; }
        public string Data { get; set; }

        protected EncryptedMessageBase(IReadOnlyDictionary<string, object> values, string encryptedMessageKey, string name)
            : base(values, name)
        {
            var encryptedMessageValues = (IReadOnlyDictionary<string, object>)values[encryptedMessageKey];

            IsText = (bool)encryptedMessageValues["isText"];
            Nonce = encryptedMessageValues["nonce"].ToString();
            Data = encryptedMessageValues["data"].ToString();
        }
    }

    public class EncryptedMessage : EncryptedMessageBase
    {
        internal const string AttachmentName = "version.EncryptedMessage";

        public EncryptedMessage(IReadOnlyDictionary<string, object> values)
            : base(values, "encryptedMessage", AttachmentName)
        {
        }
    }

    public class EncryptToSelfMessage : EncryptedMessageBase
    {
        internal const string AttachmentName = "version.EncryptToSelfMessage";

        public EncryptToSelfMessage(IReadOnlyDictionary<string, object> values)
            : base(values, "encryptToSelfMessage", AttachmentName)
        {
        }
    }

    public class PublicKeyAnnouncement : Attachment
    {
        public string RecipientPublicKey { get; set; }

        internal const string AttachmentName = "version.PublicKeyAnnouncement";

        public PublicKeyAnnouncement(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            RecipientPublicKey = values["recipientPublicKey"].ToString();
        }
    }
}
