using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace NxtLib.Internal.LocalSign
{
    internal class Crypto
    {
        private readonly SHA256 _sha256;

        public Crypto()
        {
            _sha256 = SHA256.Create();
        }

        private byte[] ComputeHash(byte[] value)
        {
            return _sha256.ComputeHash(value);
        }

        internal BinaryHexString GetPublicKey(string secretPhrase)
        {
            var publicKey = new byte[32];
            var encodedSecretPhrase = Encoding.UTF8.GetBytes(secretPhrase);
            var hashedSecretPhrase = ComputeHash(encodedSecretPhrase);
            Curve25519.Keygen(publicKey, null, hashedSecretPhrase);
            var binaryHexString = new BinaryHexString(publicKey);
            return binaryHexString;
        }

        internal ulong GetAccountIdFromPublicKey(BinaryHexString publicKey)
        {
            var publicKeyHash = ComputeHash(publicKey.ToBytes().ToArray());
            var bigInteger = new BigInteger(publicKeyHash.Take(8).ToArray());
            return (ulong)(long)bigInteger;
        }

#if NET45

        public byte[] Sign(byte[] message, string secretPhrase)
        {
            var p = new byte[32];
            var s = new byte[32];

            var sha256 = SHA256.Create();
            Curve25519.Keygen(p, s, sha256.ComputeHash(Encoding.UTF8.GetBytes(secretPhrase)));

            var m = sha256.ComputeHash(message);
            sha256.TransformBlock(m, 0, m.Length, m, 0);
            sha256.TransformFinalBlock(s, 0, s.Length);
            var x = sha256.Hash;

            var y = new byte[32];
            Curve25519.Keygen(y, null, x);

            sha256 = SHA256.Create();
            sha256.TransformBlock(m, 0, m.Length, m, 0);
            sha256.TransformFinalBlock(y, 0, y.Length);
            var h = sha256.Hash;

            var v = new byte[32];
            Curve25519.Sign(v, h, x, s);

            var signature = v.Concat(h).ToArray();
            return signature;
        }

#elif DOTNET

        internal byte[] Sign(byte[] message, string secretPhrase)
        {
            var p = new byte[32];
            var s = new byte[32];

            using (var incrementalHash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256))
            using (var sha256 = SHA256.Create())
            {
                Curve25519.Keygen(p, s, sha256.ComputeHash(Encoding.UTF8.GetBytes(secretPhrase)));

                var m = sha256.ComputeHash(message);
                incrementalHash.AppendData(m);
                incrementalHash.AppendData(s);
                var x = incrementalHash.GetHashAndReset();

                var y = new byte[32];
                Curve25519.Keygen(y, null, x);

                incrementalHash.AppendData(m);
                incrementalHash.AppendData(y);
                var h = incrementalHash.GetHashAndReset();

                var v = new byte[32];
                Curve25519.Sign(v, h, x, s);

                var signature = v.Concat(h).ToArray();
                return signature;
            }
        }

#endif
    }
}
