using System;

namespace NxtLib.Local
{
    public class LocalDecodedToken
    {
        public BinaryHexString PublicKey { get; }
        public DateTime Timestamp { get; }
        public bool Valid { get; }

        internal LocalDecodedToken(BinaryHexString publicKey, DateTime timestamp, bool valid)
        {
            PublicKey = publicKey;
            Timestamp = timestamp;
            Valid = valid;
        }
    }
}