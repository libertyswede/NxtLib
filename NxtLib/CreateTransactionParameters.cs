using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib
{
    public abstract class CreateTransactionParameters
    {
        public bool Broadcast { get; private set; }
        public short Deadline { get; private set; }
        public Amount Fee { get; private set; }
        public string RecipientPublicKey { get; set; }
        public string ReferencedTransactionFullHash { get; set; }
        public Message UnencryptedMessage { get; set; }
        public AbstractEncryptedMessage EncryptedMessage { get; set; }
        public AbstractEncryptedMessage EncryptedMessageToSelf { get; set; }

        protected CreateTransactionParameters(bool broadcast, short deadline, Amount fee)
        {
            Broadcast = broadcast;
            Deadline = deadline;
            Fee = fee;
        }

        public virtual void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            queryParameters.Add("broadcast", Broadcast.ToString());
            queryParameters.Add("deadline", Deadline.ToString());
            queryParameters.Add("feeNQT", Fee.Nqt.ToString());
            queryParameters.AddIfHasValue("recipientPublicKey", RecipientPublicKey);
            queryParameters.AddIfHasValue("referencedTransactionFullHash", ReferencedTransactionFullHash);
            AddUnencryptedMessage(queryParameters);
            AddEncryptedMessage(queryParameters);
            AddToSelfMessage(queryParameters);
        }

        private void AddUnencryptedMessage(IDictionary<string, string> queryParameters)
        {
            if (UnencryptedMessage != null)
            {
                queryParameters.Add("message", UnencryptedMessage.MessageText);
                queryParameters.Add("messageIsText", UnencryptedMessage.MessageIsText.ToString());
            }
        }

        private void AddEncryptedMessage(IDictionary<string, string> queryParameters)
        {
            MessageToBeEncrypted messageToBeEncrypted;
            AlreadyEncryptedMessage alreadyEncryptedMessage;

            if ((messageToBeEncrypted = EncryptedMessage as MessageToBeEncrypted) != null)
            {
                queryParameters.Add("messageToEncrypt", messageToBeEncrypted.MessageText);
                queryParameters.Add("messageToEncryptIsText", messageToBeEncrypted.MessageIsText.ToString());
            }
            else if ((alreadyEncryptedMessage = EncryptedMessage as AlreadyEncryptedMessage) != null)
            {
                queryParameters.Add("encryptedMessageData", alreadyEncryptedMessage.MessageText);
                queryParameters.Add("encryptedMessageNonce", alreadyEncryptedMessage.NonceText);
                queryParameters.Add("messageToEncryptIsText", alreadyEncryptedMessage.MessageIsText.ToString());
            }
        }

        private void AddToSelfMessage(IDictionary<string, string> queryParameters)
        {
            MessageToBeEncrypted messageToBeEncrypted;
            AlreadyEncryptedMessage alreadyEncryptedMessage;

            if ((messageToBeEncrypted = EncryptedMessageToSelf as MessageToBeEncrypted) != null)
            {
                queryParameters.Add("messageToEncryptToSelf", messageToBeEncrypted.MessageText);
                queryParameters.Add("messageToEncryptToSelfIsText", messageToBeEncrypted.MessageIsText.ToString());
            }
            else if ((alreadyEncryptedMessage = EncryptedMessageToSelf as AlreadyEncryptedMessage) != null)
            {
                queryParameters.Add("encryptToSelfMessageData", alreadyEncryptedMessage.MessageText);
                queryParameters.Add("encryptToSelfMessageNonce", alreadyEncryptedMessage.NonceText);
                queryParameters.Add("messageToEncryptToSelfIsText", alreadyEncryptedMessage.MessageIsText.ToString());
            }
        }

        public class Message
        {
            public bool MessageIsText { get; private set; }
            public string MessageText { get; private set; }

            public Message(string message)
            {
                MessageIsText = true;
                MessageText = message;
            }

            public Message(IEnumerable<byte> messageBytes)
            {
                MessageIsText = false;
                MessageText = ByteToHexStringConverter.ToHexString(messageBytes);
            }
        }

        public abstract class AbstractEncryptedMessage
        {
            public bool MessageIsText { get; private set; }
            public string MessageText { get; private set; }

            internal AbstractEncryptedMessage(string message, bool messageIsText)
            {
                MessageIsText = messageIsText;
                MessageText = message;
            }
        }

        public sealed class AlreadyEncryptedMessage : AbstractEncryptedMessage
        {
            public string NonceText { get; private set; }

            public AlreadyEncryptedMessage(IEnumerable<byte> messageBytes, IEnumerable<byte> encryptedMessageNonce, bool messageIsText)
                : this(ByteToHexStringConverter.ToHexString(messageBytes), encryptedMessageNonce, messageIsText)
            {
            }

            public AlreadyEncryptedMessage(string message, IEnumerable<byte> encryptedMessageNonce, bool messageIsText)
                : base(message, messageIsText)
            {
                NonceText = ByteToHexStringConverter.ToHexString(encryptedMessageNonce);
            }
        }

        public sealed class MessageToBeEncrypted : AbstractEncryptedMessage
        {
            public MessageToBeEncrypted(IEnumerable<byte> messageBytes)
                : base(ByteToHexStringConverter.ToHexString(messageBytes), false)
            {
            }

            public MessageToBeEncrypted(string message) : base(message, true)
            {
            }
        }
    }
}