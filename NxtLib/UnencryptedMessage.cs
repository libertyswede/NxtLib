using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib
{
    public class UnencryptedMessage
    {
        private readonly string _message;

        public UnencryptedMessage(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }

        public IEnumerable<byte> ToBytes()
        {
            return ByteToHexStringConverter.ToBytes(_message);
        }
    }
}