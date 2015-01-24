using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib
{
    public class BinaryHexString
    {
        private readonly string _hexData;

        public BinaryHexString(string hexData)
        {
            _hexData = hexData;
        }

        public BinaryHexString(IEnumerable<byte> data)
        {
            _hexData = ByteToHexStringConverter.ToHexString(data);
        }

        public string ToHexString()
        {
            return _hexData;
        }

        public IEnumerable<byte> ToBytes()
        {
            return ByteToHexStringConverter.ToBytes(_hexData);
        }
    }
}