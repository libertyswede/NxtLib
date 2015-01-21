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

        public void AddEncryptedMessageData(string encryptedMessageData, string encryptedMessageNonce)
        {
            QueryParameters.Add("encryptedMessageData", encryptedMessageData);
            QueryParameters.Add("encryptedMessageNonce", encryptedMessageNonce);
        }

        public void AddMessageToEncryptToSelf(string messageToEncryptToSelf, bool? messageToEncryptToSelfIsText = null)
        {
            QueryParameters.Add("messageToEncryptToSelf", messageToEncryptToSelf);
            if (messageToEncryptToSelfIsText.HasValue)
            {
                QueryParameters.Add("messageIsText", messageToEncryptToSelfIsText.ToString());
            }
        }

        public void AddEncryptToSelfMessageData(string encryptToSelfMessageData, string encryptToSelfMessageNonce)
        {
            QueryParameters.Add("encryptToSelfMessageData", encryptToSelfMessageData);
            QueryParameters.Add("encryptToSelfMessageNonce", encryptToSelfMessageNonce);
        }
    }
}