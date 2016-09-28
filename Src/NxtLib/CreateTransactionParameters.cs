using System.Collections.Generic;
using System.Linq;
using NxtLib.Internal;

namespace NxtLib
{
    public abstract class CreateTransactionParameters
    {
        public bool Broadcast { get; }
        public short Deadline { get; }
        public int? EcBlockHeight { get; set; }
        public ulong? EcBlockId { get; set; }
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
            queryParameters.AddIfHasValue(Parameters.EcBlockId, EcBlockId);
            queryParameters.AddIfHasValue(Parameters.EcBlockHeight, EcBlockHeight);
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

            if (EncryptedMessage != null)
            {
                queryParameters.Add(Parameters.EncryptedMessageIsPrunable, EncryptedMessage.IsPrunable.ToString());
                queryParameters.Add(Parameters.MessageToEncryptIsText, EncryptedMessage.MessageIsText.ToString());
                queryParameters.Add(Parameters.CompressMessageToEncrypt, EncryptedMessage.CompressMessage.ToString());
            }
            if ((messageToBeEncrypted = EncryptedMessage as MessageToBeEncrypted) != null)
            {
                queryParameters.Add(Parameters.MessageToEncrypt, messageToBeEncrypted.Message.ToHexString());
            }
            else if ((alreadyEncryptedMessage = EncryptedMessage as AlreadyEncryptedMessage) != null)
            {
                queryParameters.Add(Parameters.EncryptedMessageData, alreadyEncryptedMessage.Message.ToHexString());
                queryParameters.Add(Parameters.EncryptedMessageNonce, alreadyEncryptedMessage.Nonce.ToHexString());
            }
        }

        private void AddToSelfMessage(IDictionary<string, string> queryParameters)
        {
            MessageToBeEncryptedToSelf messageToBeEncrypted;
            AlreadyEncryptedMessageToSelf alreadyEncryptedMessage;

            if (EncryptedMessageToSelf != null)
            {
                queryParameters.Add(Parameters.MessageToEncryptToSelfIsText, EncryptedMessageToSelf.MessageIsText.ToString());
                queryParameters.Add(Parameters.CompressMessageToEncryptToSelf, EncryptedMessageToSelf.CompressMessage.ToString());
            }
            if ((messageToBeEncrypted = EncryptedMessageToSelf as MessageToBeEncryptedToSelf) != null)
            {
                queryParameters.Add(Parameters.MessageToEncryptToSelf, messageToBeEncrypted.Message.ToHexString());
            }
            else if ((alreadyEncryptedMessage = EncryptedMessageToSelf as AlreadyEncryptedMessageToSelf) != null)
            {
                queryParameters.Add(Parameters.EncryptToSelfMessageData, alreadyEncryptedMessage.Message.ToHexString());
                queryParameters.Add(Parameters.EncryptToSelfMessageNonce, alreadyEncryptedMessage.Nonce.ToHexString());
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
            public bool MessageIsText { get; }
            public bool CompressMessage { get; }
            public BinaryHexString Message { get; }
            public bool IsPrunable { get; }

            internal AbstractEncryptedMessage(BinaryHexString message, bool messageIsText, bool compressMessage, bool isPrunable)
            {
                MessageIsText = messageIsText;
                CompressMessage = compressMessage;
                IsPrunable = isPrunable;
                Message = message;
            }
        }

        public sealed class AlreadyEncryptedMessage : AbstractEncryptedMessage
        {
            public BinaryHexString Nonce { get; }

            public AlreadyEncryptedMessage(BinaryHexString message, BinaryHexString encryptedMessageNonce, bool isText, bool isCompressed, bool isPrunable = false)
                : base(message, isText, isCompressed, isPrunable)
            {
                Nonce = encryptedMessageNonce;
            }
        }

        public sealed class MessageToBeEncrypted : AbstractEncryptedMessage
        {
            public MessageToBeEncrypted(IEnumerable<byte> messageBytes, bool compressMessage, bool isPrunable = false)
                : base(ByteToHexStringConverter.ToHexString(messageBytes), false, compressMessage, isPrunable)
            {
            }

            public MessageToBeEncrypted(string message, bool compressMessage, bool isPrunable = false) 
                : base(message, true, compressMessage, isPrunable)
            {
            }
        }

        public abstract class AbstractEncryptedMessageToSelf
        {
            public bool MessageIsText { get; internal set; }
            public BinaryHexString Message { get; }
            public bool CompressMessage { get; }

            internal AbstractEncryptedMessageToSelf(BinaryHexString message, bool messageIsText, bool compressMessage)
            {
                MessageIsText = messageIsText;
                CompressMessage = compressMessage;
                Message = message;
            }
        }

        public sealed class AlreadyEncryptedMessageToSelf : AbstractEncryptedMessageToSelf
        {
            public BinaryHexString Nonce { get; }

            public AlreadyEncryptedMessageToSelf(BinaryHexString message, BinaryHexString messageNonce, bool isText, bool isCompressed)
                : base(message, isText, isCompressed)
            {
                Nonce = messageNonce;
            }
        }

        public sealed class MessageToBeEncryptedToSelf : AbstractEncryptedMessageToSelf
        {
            public MessageToBeEncryptedToSelf(IEnumerable<byte> messageBytes, bool compress)
                : base(ByteToHexStringConverter.ToHexString(messageBytes), false, compress)
            {
            }

            public MessageToBeEncryptedToSelf(string message, bool compress)
                : base(message, true, compress)
            {
            }
        }
    }
}