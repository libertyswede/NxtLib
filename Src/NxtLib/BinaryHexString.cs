using System;
using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib
{
    public class BinaryHexString : IEquatable<BinaryHexString>
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
            return ByteToHexStringConverter.ToBytesFromHexString(_hexData);
        }

        public bool Equals(BinaryHexString other)
        {
            return other != null && other.ToHexString().Equals(ToHexString());
        }

        public override bool Equals(object obj)
        {
            var other = obj as BinaryHexString;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return ToHexString().GetHashCode();
        }

        public override string ToString()
        {
            return ToHexString();
        }
    }
}