using System.Collections.Generic;
using System.Linq;
using NxtLib.Internal;

namespace NxtLib
{
    public abstract class CreateTransactionParameters
    {
        public bool Broadcast { get; }
        public short Deadline { get; }
        public Amount Fee { get; }
        public BinaryHexString RecipientPublicKey { get; set; }
        public BinaryHexString ReferencedTransactionFullHash { get; set; }
        public UnencryptedMessage Message { get; set; }
        public AbstractEncryptedMessage EncryptedMessage { get; set; }
        public AbstractEncryptedMessage EncryptedMessageToSelf { get; set; }
        public CreateTransactionPhasing Phasing { get; set; }

        protected CreateTransactionParameters(bool broadcast, short deadline, Amount fee)
        {
            Broadcast = broadcast;
            Deadline = deadline;
            Fee = fee;
        }

        internal virtual void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            queryParameters.Add("broadcast", Broadcast.ToString());
            queryParameters.Add(Parameters.Deadline, Deadline.ToString());
            queryParameters.Add(Parameters.FeeNqt, Fee.Nqt.ToString());
            queryParameters.AddIfHasValue("recipientPublicKey", RecipientPublicKey);
            queryParameters.AddIfHasValue(Parameters.ReferencedTransactionFullHash, ReferencedTransactionFullHash);
            AddUnencryptedMessage(queryParameters);
            AddEncryptedMessage(queryParameters);
            AddToSelfMessage(queryParameters);
            Phasing?.AppendToQueryParameters(queryParameters);
        }

        internal virtual void AppendToQueryParameters(Dictionary<string, List<string>> queryParameters)
        {
            var dictionary = new Dictionary<string, string>();
            AppendToQueryParameters(dictionary);
            dictionary.ToList().ForEach(kvp => queryParameters.Add(kvp.Key, new List<string>{kvp.Value}));
        }

        private void AddUnencryptedMessage(IDictionary<string, string> queryParameters)
        {
            if (Message != null)
            {
                queryParameters.Add("message", Message.Message);
                queryParameters.Add("messageIsText", Message.MessageIsText.ToString());
            }
        }

        private void AddEncryptedMessage(IDictionary<string, string> queryParameters)
        {
            MessageToBeEncrypted messageToBeEncrypted;
            AlreadyEncryptedMessage alreadyEncryptedMessage;

            if ((messageToBeEncrypted = EncryptedMessage as MessageToBeEncrypted) != null)
            {
                queryParameters.Add("messageToEncrypt", messageToBeEncrypted.Message);
                queryParameters.Add("messageToEncryptIsText", messageToBeEncrypted.MessageIsText.ToString());
            }
            else if ((alreadyEncryptedMessage = EncryptedMessage as AlreadyEncryptedMessage) != null)
            {
                queryParameters.Add("encryptedMessageData", alreadyEncryptedMessage.Message);
                queryParameters.Add("encryptedMessageNonce", ByteToHexStringConverter.ToHexString(alreadyEncryptedMessage.Nonce));
                queryParameters.Add("messageToEncryptIsText", alreadyEncryptedMessage.MessageIsText.ToString());
            }
        }

        private void AddToSelfMessage(IDictionary<string, string> queryParameters)
        {
            MessageToBeEncrypted messageToBeEncrypted;
            AlreadyEncryptedMessage alreadyEncryptedMessage;

            if ((messageToBeEncrypted = EncryptedMessageToSelf as MessageToBeEncrypted) != null)
            {
                queryParameters.Add("messageToEncryptToSelf", messageToBeEncrypted.Message);
                queryParameters.Add("messageToEncryptToSelfIsText", messageToBeEncrypted.MessageIsText.ToString());
            }
            else if ((alreadyEncryptedMessage = EncryptedMessageToSelf as AlreadyEncryptedMessage) != null)
            {
                queryParameters.Add("encryptToSelfMessageData", alreadyEncryptedMessage.Message);
                queryParameters.Add("encryptToSelfMessageNonce", ByteToHexStringConverter.ToHexString(alreadyEncryptedMessage.Nonce));
                queryParameters.Add("messageToEncryptToSelfIsText", alreadyEncryptedMessage.MessageIsText.ToString());
            }
        }

        public class UnencryptedMessage
        {
            public bool MessageIsText { get; }
            public string Message { get; }

            public UnencryptedMessage(string message)
            {
                MessageIsText = true;
                Message = message;
            }

            public UnencryptedMessage(IEnumerable<byte> messageBytes)
            {
                MessageIsText = false;
                Message = ByteToHexStringConverter.ToHexString(messageBytes);
            }
        }

        public abstract class AbstractEncryptedMessage
        {
            public bool MessageIsText { get; internal set; }
            public string Message { get; }

            internal AbstractEncryptedMessage(string message, bool messageIsText)
            {
                MessageIsText = messageIsText;
                Message = message;
            }
        }

        public sealed class AlreadyEncryptedMessage : AbstractEncryptedMessage
        {
            public IEnumerable<byte> Nonce { get; }

            public AlreadyEncryptedMessage(IEnumerable<byte> messageBytes, IEnumerable<byte> encryptedMessageNonce, bool messageIsText)
                : this(ByteToHexStringConverter.ToHexString(messageBytes), encryptedMessageNonce, messageIsText)
            {
            }

            public AlreadyEncryptedMessage(string message, IEnumerable<byte> encryptedMessageNonce, bool messageIsText)
                : base(message, messageIsText)
            {
                Nonce = encryptedMessageNonce;
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