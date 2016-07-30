using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using NxtLib.Local;

namespace NxtLib.Internal.LocalSign
{
    internal class Crypto
    {
        private readonly StringConverter _stringConverter = new StringConverter();

        private static byte[] ComputeHash(byte[] value)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(value);
            }
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

        internal byte[] GetSharedSecret(byte[] theirPublicKey, byte[] nonce, string secretPhrase)
        {
            return GetSharedSecret(theirPublicKey, nonce, GetPrivateKeyBytes(secretPhrase));
        }

        private byte[] GetSharedSecret(byte[] theirPublicKey, byte[] nonce, byte[] myPrivateKey)
        {
            var sharedSecret = new byte[32];
            Curve25519.Curve(sharedSecret, myPrivateKey, theirPublicKey);
            for (var i = 0; i < 32; i++)
            {
                sharedSecret[i] ^= nonce[i];
            }
            return ComputeHash(sharedSecret);
        }

        internal byte[] AesEncryptTo(byte[] recipient, byte[] message, byte[] nonce, string secretPhrase)
        {
            var senderSecretBytes = GetPrivateKeyBytes(secretPhrase);
            var key = GetSharedSecret(recipient, nonce, senderSecretBytes);

            using (var ms = new MemoryStream())
            using (var cryptor = Aes.Create())
            {
                if (cryptor == null)
                {
                    throw new CryptographicException("Unable to create AES managed encryption object.");
                }

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

        internal byte[] AesDecryptFrom(byte[] sender, byte[] data, byte[] nonce, string secretPhrase)
        {
            var sharedSecret = GetSharedSecret(sender, nonce, secretPhrase);
            return AesDecrypt(data, nonce, sharedSecret);
        }

        internal byte[] AesDecrypt(byte[] data, byte[] nonce, byte[] key)
        {
            if (data.Length < 16 || data.Length % 16 != 0)
            {
                throw new ArgumentException("Invalid ciphertext", nameof(data));
            }

            var iv = new byte[16];
            var encryptedContent = new byte[data.Length - 16];

            Buffer.BlockCopy(data, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(data, iv.Length, encryptedContent, 0, encryptedContent.Length);

            using (var ms = new MemoryStream())
            using (var cryptor = Aes.Create())
            {
                if (cryptor == null)
                {
                    throw new CryptographicException("Unable to create AES managed encryption object.");
                }

                cryptor.Mode = CipherMode.CBC;
                cryptor.Padding = PaddingMode.PKCS7;
                cryptor.KeySize = 128;
                cryptor.BlockSize = 128;

                using (CryptoStream cs = new CryptoStream(ms, cryptor.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedContent, 0, encryptedContent.Length);

                }
                return ms.ToArray();
            }
        }

        internal string GenerateToken(string secretPhrase, byte[] message, int timestamp)
        {
            var data = new byte[message.Length + 32 + 4];

            Buffer.BlockCopy(message, 0, data, 0, message.Length);
            Buffer.BlockCopy(GetPublicKey(secretPhrase).ToBytes().ToArray(), 0, data, message.Length, 32);
            
            data[message.Length + 32] = (byte)timestamp;
            data[message.Length + 32 + 1] = (byte)(timestamp >> 8);
            data[message.Length + 32 + 2] = (byte)(timestamp >> 16);
            data[message.Length + 32 + 3] = (byte)(timestamp >> 24);

            var token = new byte[100];
            Buffer.BlockCopy(data, message.Length, token, 0, 32 + 4);
            Buffer.BlockCopy(Sign(data, secretPhrase), 0, token, 32 + 4, 64);

            var buf = new StringBuilder();
            for (var ptr = 0; ptr < 100; ptr += 5)
            {
                var number = ((long)token[ptr] & 0xFF) | ((long)(token[ptr + 1] & 0xFF) << 8) | ((long)(token[ptr + 2] & 0xFF) << 16)
                        | ((long)(token[ptr + 3] & 0xFF) << 24) | ((long)(token[ptr + 4] & 0xFF) << 32);
                
                buf.Append(_stringConverter.ToBase32String(number).PadLeft(8, '0'));
            }

            var tokenString = buf.ToString();
            return tokenString;
        }

        public LocalDecodedToken DecodeToken(byte[] messageBytes, string token)
        {
            var tokenBytes = new byte[100];
            int i = 0, j = 0;

            for (; i < token.Length; i += 8, j += 5)
            {
                var number = _stringConverter.FromBase32String(token.Substring(i, 8));
                tokenBytes[j] = (byte) number;
                tokenBytes[j + 1] = (byte) (number >> 8);
                tokenBytes[j + 2] = (byte) (number >> 16);
                tokenBytes[j + 3] = (byte) (number >> 24);
                tokenBytes[j + 4] = (byte) (number >> 32);
            }

            if (i != 160)
            {
                throw new ArgumentException($"Invalid token string: {token}", nameof(token));
            }
            
            var publicKey = tokenBytes.Take(32).ToArray();
            var timestamp = (tokenBytes[32] & 0xFF) | ((tokenBytes[33] & 0xFF) << 8) | ((tokenBytes[34] & 0xFF) << 16) | ((tokenBytes[35] & 0xFF) << 24);
            var signature = tokenBytes.Skip(32 + 4).ToArray();

            var data = new byte[messageBytes.Length + 32 + 4];
            Buffer.BlockCopy(messageBytes, 0, data, 0, messageBytes.Length);
            Buffer.BlockCopy(tokenBytes, 0, data, messageBytes.Length, 36);

            var isValid = Verify(signature, data, publicKey, true);
            return new LocalDecodedToken(publicKey, new DateTimeConverter().GetFromNxtTime(timestamp), isValid);
        }

        private bool Verify(byte[] signature, byte[] message, byte[] publicKey, bool enforceCanonical)
        {
            try
            {
                if (signature.Length != 64 ||
                    (enforceCanonical && !Curve25519.IsCanonicalSignature(signature)) ||
                    (enforceCanonical && !Curve25519.IsCanonicalPublicKey(publicKey)))
                {
                    return false;
                }

                var y = new byte[32];
                var v = signature.Take(32).ToArray();
                var h = signature.Skip(32).ToArray();

                Curve25519.Verify(y, v, h, publicKey);
                var h2 = HashIncremental(message, y);
                return h.SequenceEqual(h2);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private byte[] GetPrivateKeyBytes(string secretPhrase)
        {
            using (var sha256 = SHA256.Create())
            {
                var privateKeyBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(secretPhrase));
                Curve25519.Clamp(privateKeyBytes);
                return privateKeyBytes;
            }
        }

#if (NET40 || NET45)

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

        private byte[] HashIncremental(byte[] message, byte[] other)
        {
            using (var sha256 = SHA256.Create())
            {
                var m = sha256.ComputeHash(message);
                sha256.TransformBlock(m, 0, m.Length, m, 0);
                sha256.TransformFinalBlock(other, 0, other.Length);
                return sha256.Hash;
            }
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

        private byte[] HashIncremental(byte[] message, byte[] other)
        {
            using (var incrementalHash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256))
            using (var sha256 = SHA256.Create())
            {
                var m = sha256.ComputeHash(message);
                incrementalHash.AppendData(m);
                incrementalHash.AppendData(other);
                return incrementalHash.GetHashAndReset();
            }
        }

#endif
    }
}
