using System;
using System.Collections.Generic;

namespace NxtLib
{
    public abstract class Appendix : Attachment
    {
    }

    public class UnencryptedMessage : Appendix
    {
        public bool MessageIsText { get; set; }
        public string Message { get; set; }

        internal static string AppendixName { get { return MessageKey; } }

        public UnencryptedMessage(IReadOnlyDictionary<string, object> values)
        {
            MessageIsText = Convert.ToBoolean(values[MessageIsTextKey]);
            Message = values[MessageKey].ToString();
        }
    }

    public abstract class EncryptedMessageBase : Appendix
    {
        public bool IsText { get; set; }
        public string Nonce { get; set; }
        public string Data { get; set; }

        protected EncryptedMessageBase(IReadOnlyDictionary<string, object> values, string encryptedMessageKey)
        {
            var encryptedMessageValues = (IReadOnlyDictionary<string, object>)values[encryptedMessageKey];

            IsText = (bool)encryptedMessageValues[IsTextKey];
            Nonce = encryptedMessageValues[NonceKey].ToString();
            Data = encryptedMessageValues[DataKey].ToString();
        }
    }

    public class EncryptedMessage : EncryptedMessageBase
    {
        internal static string AppendixName { get { return EncryptedMessageKey; } }

        public EncryptedMessage(IReadOnlyDictionary<string, object> values)
            : base(values, EncryptedMessageKey)
        {
        }
    }

    public class EncryptToSelfMessage : EncryptedMessageBase
    {
        internal static string AppendixName { get { return EncryptToSelfMessageKey; } }

        public EncryptToSelfMessage(IReadOnlyDictionary<string, object> values)
            : base(values, EncryptToSelfMessageKey)
        {
        }
    }

    public class PublicKeyAnnouncement : Appendix
    {
        public string RecipientPublicKey { get; set; }

        internal static string AppendixName { get { return RecipientPublicKeyKey; } }

        public PublicKeyAnnouncement(IReadOnlyDictionary<string, object> values)
        {
            RecipientPublicKey = values[RecipientPublicKeyKey].ToString();
        }
    }
}
