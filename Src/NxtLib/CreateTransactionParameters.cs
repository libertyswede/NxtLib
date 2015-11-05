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
        public AbstractEncryptedMessageToSelf EncryptedMessageToSelf { get; set; }
        public CreateTransactionPhasing Phasing { get; set; }

        protected CreateTransactionParameters(bool broadcast, short deadline, Amount fee)
        {
            Broadcast = broadcast;
            Deadline = deadline;
            Fee = fee;
        }

        internal virtual void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            queryParameters.Add(Parameters.Broadcast, Broadcast.ToString());
            queryParameters.Add(Parameters.Deadline, Deadline.ToString());
            queryParameters.Add(Parameters.FeeNqt, Fee.Nqt.ToString());
            queryParameters.AddIfHasValue(Parameters.RecipientPublicKey, RecipientPublicKey);
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
                queryParameters.Add(Parameters.Message, Message.Message);
                queryParameters.Add(Parameters.MessageIsText, Message.MessageIsText.ToString());
                queryParameters.Add(Parameters.MessageIsPrunable, Message.IsPrunable.ToString());
            }
        }

        private void AddEncryptedMessage(IDictionary<string, string> queryParameters)
        {
            MessageToBeEncrypted messageToBeEncrypted;
            AlreadyEncryptedMessage alreadyEncryptedMessage;

            queryParameters.Add(Parameters.EncryptedMessageIsPrunable, EncryptedMessage.IsPrunable.ToString());
            queryParameters.Add(Parameters.MessageToEncryptIsText, EncryptedMessage.MessageIsText.ToString());

            if ((messageToBeEncrypted = EncryptedMessage as MessageToBeEncrypted) != null)
            {
                queryParameters.Add(Parameters.MessageToEncrypt, messageToBeEncrypted.Message);
            }
            else if ((alreadyEncryptedMessage = EncryptedMessage as AlreadyEncryptedMessage) != null)
            {
                queryParameters.Add(Parameters.EncryptedMessageData, alreadyEncryptedMessage.Message);
                queryParameters.Add(Parameters.EncryptedMessageNonce, ByteToHexStringConverter.ToHexString(alreadyEncryptedMessage.Nonce));
            }
        }

        private void AddToSelfMessage(IDictionary<string, string> queryParameters)
        {
            MessageToBeEncryptedToSelf messageToBeEncrypted;
            AlreadyEncryptedMessageToSelf alreadyEncryptedMessage;

            queryParameters.Add(Parameters.MessageToEncryptToSelfIsText, EncryptedMessageToSelf.MessageIsText.ToString());

            if ((messageToBeEncrypted = EncryptedMessageToSelf as MessageToBeEncryptedToSelf) != null)
            {
                queryParameters.Add(Parameters.MessageToEncryptToSelf, messageToBeEncrypted.Message);
            }
            else if ((alreadyEncryptedMessage = EncryptedMessageToSelf as AlreadyEncryptedMessageToSelf) != null)
            {
                queryParameters.Add(Parameters.EncryptToSelfMessageData, alreadyEncryptedMessage.Message);
                queryParameters.Add(Parameters.EncryptToSelfMessageNonce, ByteToHexStringConverter.ToHexString(alreadyEncryptedMessage.Nonce));
            }
        }

        public class UnencryptedMessage
        {
            public bool MessageIsText { get; }
            public string Message { get; }
            public bool IsPrunable { get; }

            public UnencryptedMessage(string message, bool isPrunable = false)
            {
                MessageIsText = true;
                Message = message;
                IsPrunable = isPrunable;
            }

            public UnencryptedMessage(IEnumerable<byte> messageBytes, bool isPrunable = false)
            {
                MessageIsText = false;
                Message = ByteToHexStringConverter.ToHexString(messageBytes);
                IsPrunable = isPrunable;
            }
        }

        public abstract class AbstractEncryptedMessage
        {
            public bool MessageIsText { get; internal set; }
            public string Message { get; }
            public bool IsPrunable { get; }

            internal AbstractEncryptedMessage(string message, bool messageIsText, bool isPrunable)
            {
                MessageIsText = messageIsText;
                IsPrunable = isPrunable;
                Message = message;
            }
        }

        public sealed class AlreadyEncryptedMessage : AbstractEncryptedMessage
        {
            public IEnumerable<byte> Nonce { get; }

            public AlreadyEncryptedMessage(IEnumerable<byte> messageBytes, IEnumerable<byte> encryptedMessageNonce, bool isPrunable = false)
                : base(ByteToHexStringConverter.ToHexString(messageBytes), false, isPrunable)
            {
                Nonce = encryptedMessageNonce;
            }

            public AlreadyEncryptedMessage(string message, IEnumerable<byte> encryptedMessageNonce, bool isPrunable = false)
                : base(message, true, isPrunable)
            {
                Nonce = encryptedMessageNonce;
            }
        }

        public sealed class MessageToBeEncrypted : AbstractEncryptedMessage
        {
            public MessageToBeEncrypted(IEnumerable<byte> messageBytes, bool isPrunable = false)
                : base(ByteToHexStringConverter.ToHexString(messageBytes), false, isPrunable)
            {
            }

            public MessageToBeEncrypted(string message, bool isPrunable = false) 
                : base(message, true, isPrunable)
            {
            }
        }

        public abstract class AbstractEncryptedMessageToSelf
        {
            public bool MessageIsText { get; internal set; }
            public string Message { get; }

            internal AbstractEncryptedMessageToSelf(string message, bool messageIsText)
            {
                MessageIsText = messageIsText;
                Message = message;
            }
        }

        public sealed class AlreadyEncryptedMessageToSelf : AbstractEncryptedMessageToSelf
        {
            public IEnumerable<byte> Nonce { get; }

            public AlreadyEncryptedMessageToSelf(IEnumerable<byte> messageBytes, IEnumerable<byte> encryptedMessageNonce)
                : base(ByteToHexStringConverter.ToHexString(messageBytes), false)
            {
                Nonce = encryptedMessageNonce;
            }

            public AlreadyEncryptedMessageToSelf(string message, IEnumerable<byte> encryptedMessageNonce)
                : base(message, true)
            {
                Nonce = encryptedMessageNonce;
            }
        }

        public sealed class MessageToBeEncryptedToSelf : AbstractEncryptedMessageToSelf
        {
            public MessageToBeEncryptedToSelf(IEnumerable<byte> messageBytes)
                : base(ByteToHexStringConverter.ToHexString(messageBytes), false)
            {
            }

            public MessageToBeEncryptedToSelf(string message)
                : base(message, true)
            {
            }
        }
    }
}