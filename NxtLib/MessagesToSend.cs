using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib
{
    public class MessagesToSend
    {
        internal Dictionary<string, string> QueryParameters { get; private set; }

        public MessagesToSend()
        {
            QueryParameters = new Dictionary<string, string>();
        }

        public void AddMessage(string message)
        {
            AddMessage(message, true);
        }

        public void AddMessage(IEnumerable<byte> message)
        {
            AddMessage(ByteToHexStringConverter.ToHexString(message), false);
        }

        private void AddMessage(string message, bool messageIsText)
        {
            QueryParameters.Add("message", message);
            QueryParameters.Add("messageIsText", messageIsText.ToString());
        }

        public void AddMessageToEncrypt(string messageToEncrypt)
        {
            AddMessageToEncrypt(messageToEncrypt, true);
        }

        public void AddMessageToEncrypt(IEnumerable<byte> messageToEncrypt)
        {
            AddMessageToEncrypt(ByteToHexStringConverter.ToHexString(messageToEncrypt), false);
        }

        private void AddMessageToEncrypt(string messageToEncrypt, bool messageToEncryptIsText)
        {
            QueryParameters.Add("messageToEncrypt", messageToEncrypt);
            QueryParameters.Add("messageToEncryptIsText", messageToEncryptIsText.ToString());
        }

        public void AddMessageToEncryptToSelf(string messageToEncryptToSelf)
        {
            AddMessageToEncryptToSelf(messageToEncryptToSelf, true);
        }

        public void AddMessageToEncryptToSelf(IEnumerable<byte> messageToEncryptToSelf)
        {
            AddMessageToEncryptToSelf(ByteToHexStringConverter.ToHexString(messageToEncryptToSelf), false);
        }

        private void AddMessageToEncryptToSelf(string messageToEncryptToSelf, bool messageToEncryptToSelfIsText)
        {
            QueryParameters.Add("messageToEncryptToSelf", messageToEncryptToSelf);
            QueryParameters.Add("messageToEncryptToSelfIsText", messageToEncryptToSelfIsText.ToString());
        }

        public void AddEncryptedMessageData(string encryptedMessageData, IEnumerable<byte> encryptedMessageNonce, bool encryptedMessageIsText)
        {
            QueryParameters.Add("encryptedMessageData", encryptedMessageData);
            QueryParameters.Add("encryptedMessageNonce", ByteToHexStringConverter.ToHexString(encryptedMessageNonce));
            QueryParameters.Add("messageToEncryptIsText", encryptedMessageIsText.ToString());
        }

        public void AddEncryptedMessageData(IEnumerable<byte> encryptedMessageData, IEnumerable<byte> encryptedMessageNonce, bool encryptedMessageIsText)
        {
            AddEncryptedMessageData(ByteToHexStringConverter.ToHexString(encryptedMessageData), encryptedMessageNonce, encryptedMessageIsText);
        }

        public void AddEncryptToSelfMessageData(IEnumerable<byte> encryptToSelfMessageData, IEnumerable<byte> encryptToSelfMessageNonce, bool encryptToSelfMessageIsText)
        {
            AddEncryptToSelfMessageData(ByteToHexStringConverter.ToHexString(encryptToSelfMessageData),
                encryptToSelfMessageNonce, encryptToSelfMessageIsText);
        }

        public void AddEncryptToSelfMessageData(string encryptToSelfMessageData, IEnumerable<byte> encryptToSelfMessageNonce, bool encryptToSelfMessageIsText)
        {
            QueryParameters.Add("encryptToSelfMessageData", encryptToSelfMessageData);
            QueryParameters.Add("encryptToSelfMessageNonce", ByteToHexStringConverter.ToHexString(encryptToSelfMessageNonce));
            QueryParameters.Add("messageToEncryptToSelfIsText", encryptToSelfMessageIsText.ToString());
        }
    }
}