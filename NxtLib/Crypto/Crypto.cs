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

        public byte[] Sign(byte[] message, string secretPhrase)
        {
            var p = new byte[32];
            var s = new byte[32];

            var sha256 = SHA256.Create();
            Curve25519.Keygen(p, s, sha256.ComputeHash(Encoding.UTF8.GetBytes(secretPhrase)));

            var m = sha256.ComputeHash(message);
            // Java code below
            //byte[] P = new byte[32];
            //byte[] s = new byte[32];
            //MessageDigest digest = Crypto.sha256();
            //Curve25519.keygen(P, s, digest.digest(Convert.toBytes(secretPhrase)));

            //byte[] m = digest.digest(message);

            //digest.update(m);
            //byte[] x = digest.digest(s);

            //byte[] Y = new byte[32];
            //Curve25519.keygen(Y, null, x);

            //digest.update(m);
            //byte[] h = digest.digest(Y);

            //byte[] v = new byte[32];
            //Curve25519.sign(v, h, x, s);

            //byte[] signature = new byte[64];
            //System.arraycopy(v, 0, signature, 0, 32);
            //System.arraycopy(h, 0, signature, 32, 32);

            //return signature;
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
