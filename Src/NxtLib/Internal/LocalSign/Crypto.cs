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
        private readonly SHA256 _sha256;
        private readonly StringConverter _stringConverter = new StringConverter();

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

        internal byte[] AesDecryptFrom(byte[] sender, byte[] data, byte[] nonce, string secretPhrase)
        {
            if (data.Length < 16 || data.Length % 16 != 0)
            {
                throw new ArgumentException("Invalid ciphertext", nameof(data));
            }
            
            var iv = new byte[16];
            var encryptedContent = new byte[data.Length - 16];
            var secretBytes = GetPrivateKeyBytes(secretPhrase);
            var key = GenerateAesKey(secretBytes, sender, nonce);
            
            Buffer.BlockCopy(data, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(data, iv.Length, encryptedContent, 0, encryptedContent.Length);
    
            using (var ms = new MemoryStream())
            using (var cryptor = Aes.Create())
            {
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

        internal string GenerateToken(string secretPhrase, byte[] message, DateTime dateTime)
        {
            var data = new byte[message.Length + 32 + 4];

            Buffer.BlockCopy(message, 0, data, 0, message.Length);
            Buffer.BlockCopy(GetPublicKey(secretPhrase).ToBytes().ToArray(), 0, data, message.Length, 32);
            var timestamp = DateTimeConverter.GetNxtTime(dateTime);
            
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

                var number = ((long)(token[ptr] & 0xFF)) | (((long)(token[ptr + 1] & 0xFF)) << 8) | (((long)(token[ptr + 2] & 0xFF)) << 16)
                        | (((long)(token[ptr + 3] & 0xFF)) << 24) | (((long)(token[ptr + 4] & 0xFF)) << 32);

                if (number < 32)
                {
                    buf.Append("0000000");
                }
                else if (number < 1024)
                {
                    buf.Append("000000");
                }
                else if (number < 32768)
                {
                    buf.Append("00000");
                }
                else if (number < 1048576)
                {
                    buf.Append("0000");
                }
                else if (number < 33554432)
                {
                    buf.Append("000");
                }
                else if (number < 1073741824)
                {
                    buf.Append("00");
                }
                else if (number < 34359738368L)
                {
                    buf.Append("0");
                }

                buf.Append(_stringConverter.ToBase32String(number));
            }

            var tokenString = buf.ToString();
            return tokenString;
        }

        private byte[] GetPrivateKeyBytes(string secretPhrase)
        {
            var privateKeyBytes = _sha256.ComputeHash(Encoding.UTF8.GetBytes(secretPhrase));
            Curve25519.Clamp(privateKeyBytes);
            return privateKeyBytes;
        }

        private byte[] GenerateAesKey(byte[] myPrivateKey, byte[] theirPublicKey, byte[] nonce)
        {
            var dhSharedSecret = new byte[32];
            Curve25519.Curve(dhSharedSecret, myPrivateKey, theirPublicKey);
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
