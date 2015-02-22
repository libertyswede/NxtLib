using System.Linq;
using System.Text;
using NxtLib.Internal;

namespace NxtLib
{
    public class UnencryptedMessage
    {
        public byte[] MessageBytes { get; private set; }
        public string MessageText { get; private set; }
        public bool MessageIsText { get; private set; }

        public UnencryptedMessage(string messageText, bool messageIsText = true)
        {
            MessageBytes = messageIsText
                ? Encoding.UTF8.GetBytes(messageText)
                : ByteToHexStringConverter.ToBytes(messageText).ToArray();
            MessageText = messageText;
            MessageIsText = messageIsText;
        }

        public UnencryptedMessage(byte[] messageBytes, bool messageIsText = false)
        {
            MessageBytes = messageBytes;
            MessageText = messageIsText
                ? Encoding.UTF8.GetString(messageBytes, 0, messageBytes.Length)
                : ByteToHexStringConverter.ToHexString(messageBytes);
            MessageIsText = messageIsText;
        }
    }
}