using System.Collections.Generic;

namespace NxtLib
{
    public class MessagesToSend
    {
        internal Dictionary<string, string> QueryParameters { get; private set; }

        public MessagesToSend()
        {
            QueryParameters = new Dictionary<string, string>();
        }

        public void AddMessage(string message, bool? messageIsText = null)
        {
            QueryParameters.Add("message", message);
            if (messageIsText.HasValue)
            {
                QueryParameters.Add("messageIsText", messageIsText.ToString());
            }
        }

        public void AddMessageToEncrypt(string messageToEncrypt, bool? messageToEncryptIsText = null)
        {
            QueryParameters.Add("messageToEncrypt", messageToEncrypt);
            if (messageToEncryptIsText.HasValue)
            {
                QueryParameters.Add("messageToEncryptIsText", messageToEncryptIsText.ToString());
            }
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