using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib.ArbitraryMessageOperations
{
    public class ReadMessageReply : BaseReply
    {
        public string DecryptedMessage { get; set; }
        public string DecryptedMessageToSelf { get; set; }
        public string Message { get; set; }

        public IEnumerable<byte> DecryptedMessageAsBytes()
        {
            return ByteToHexStringConverter.ToBytes(DecryptedMessage);
        }

        public IEnumerable<byte> DecryptedMessageToSelfAsBytes()
        {
            return ByteToHexStringConverter.ToBytes(DecryptedMessageToSelf);
        }

        public IEnumerable<byte> MessageAsBytes()
        {
            return ByteToHexStringConverter.ToBytes(Message);
        }
    }
}