using System;
using System.IO;
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

        internal byte[] AesEncryptTo(byte[] recipient, byte[] message, byte[] nonce, string secretPhrase)
        {
            var senderSecretBytes = GetPrivateKeyBytes(secretPhrase);
            var key = GenerateAesKey(senderSecretBytes, recipient, nonce);

            using (var ms = new MemoryStream())
            using (var cryptor = Aes.Create())
            {
                cryptor.Mode = CipherMode.CBC;
                cryptor.Padding = PaddingMode.PKCS7;
                cryptor.KeySize = 128;
                cryptor.BlockSize = 128;

                var iv = cryptor.IV;

                using (var cs = new CryptoStream(ms, cryptor.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    cs.Write(message, 0, message.Length);
                }
                var encryptedContent = ms.ToArray();
                var result = new byte[iv.Length + encryptedContent.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encryptedContent, 0, result, iv.Length, encryptedContent.Length);
                return result;
            }
        }

        private byte[] GetPrivateKeyBytes(string secretPhrase)
        {
            var privateKeyBytes = _sha256.ComputeHash(Encoding.UTF8.GetBytes(secretPhrase));
            Curve25519.Clamp(privateKeyBytes);
            return privateKeyBytes;
        }

        private byte[] GenerateAesKey(byte[] senderSecretBytes, byte[] recipientPublicKeyBytes, byte[] nonce)
        {
            var dhSharedSecret = new byte[32];
            Curve25519.Curve(dhSharedSecret, senderSecretBytes, recipientPublicKeyBytes);
            for (var i = 0; i < 32; i++)
            {
                dhSharedSecret[i] ^= nonce[i];
            }
            var key = _sha256.ComputeHash(dhSharedSecret);
            return key;
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
