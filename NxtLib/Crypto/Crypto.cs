using System.Security.Cryptography;
using System.Text;

namespace NxtLib.Crypto
{
    internal class Crypto : ICrypto
    {
        private readonly SHA256 _sha256;
        public byte[] Hash { get { return _sha256.Hash; } }

        public Crypto()
        {
            _sha256 = SHA256.Create();
        }

        public byte[] ComputeHash(byte[] value)
        {
            return _sha256.ComputeHash(value);
        }

        public BinaryHexString GetPublicKey(string secretPhrase)
        {
            var publicKey = new byte[32];
            var encodedSecretPhrase = Encoding.UTF8.GetBytes(secretPhrase);
            var hashedSecretPhrase = ComputeHash(encodedSecretPhrase);
            Curve25519.Keygen(publicKey, null, hashedSecretPhrase);
            var binaryHexString = new BinaryHexString(publicKey);
            return binaryHexString;
        }
    }
}
