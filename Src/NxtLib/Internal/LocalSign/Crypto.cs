using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using AlreadyEncryptedMessage = NxtLib.CreateTransactionParameters.AlreadyEncryptedMessage;
using MessageToBeEncrypted = NxtLib.CreateTransactionParameters.MessageToBeEncrypted;

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

        public byte[] GenerateNonceBytes()
        {
            RandomNumberGenerator random = new RNGCryptoServiceProvider();
            var nonceBytes = new byte[32];
            random.GetBytes(nonceBytes);
            return nonceBytes;
        }

        public byte[] GetPrivateKey(string secretPhrase)
        {
            var s = _sha256.ComputeHash(ByteToHexStringConverter.ToBytes(secretPhrase).ToArray());
            Curve25519.Clamp(s);
            return s;
        }

        public byte[] AesEncrypt(byte[] message, byte[] secretPhrase, byte[] theirPublicKey, byte[] nonce)
        {
            var compressedMessage = Compress(message);

            var sharedSecret = new byte[32];
            Curve25519.Curve(sharedSecret, secretPhrase, theirPublicKey);
            for (var i = 0; i < 32; i++)
            {
                sharedSecret[i] ^= nonce[i];
            }
            var key = _sha256.ComputeHash(message);
            var iv = new byte[16];
            RandomNumberGenerator random = new RNGCryptoServiceProvider();
            random.GetBytes(iv);
            var aes = new PaddedBufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
            var ivAndKey = new ParametersWithIV(new KeyParameter(key), iv);
            aes.Init(true, ivAndKey);
            var output = new byte[aes.GetOutputSize(compressedMessage.Length)];
            var ciphertextLength = aes.ProcessBytes(compressedMessage, 0, compressedMessage.Length, output, 0);
            ciphertextLength += aes.DoFinal(output, ciphertextLength);
            var result = new byte[iv.Length + ciphertextLength];
            Array.Copy(iv, 0, result, 0, iv.Length);
            Array.Copy(output, 0, result, iv.Length, ciphertextLength);
            return result;
        }

        private static byte[] Compress(byte[] data)
        {
            using (var memoryStream = new MemoryStream())
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                gZipStream.Write(data, 0, data.Length);
                return memoryStream.ToArray();
            }
        }

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

        public BinaryHexString GetPublicKey(string secretPhrase)
        {
            var publicKey = new byte[32];
            var encodedSecretPhrase = Encoding.UTF8.GetBytes(secretPhrase);
            var hashedSecretPhrase = ComputeHash(encodedSecretPhrase);
            Curve25519.Keygen(publicKey, null, hashedSecretPhrase);
            var binaryHexString = new BinaryHexString(publicKey);
            return binaryHexString;
        }

        public ulong GetAccountIdFromPublicKey(BinaryHexString publicKey)
        {
            var publicKeyHash = ComputeHash(publicKey.ToBytes().ToArray());
            var bigInteger = new BigInteger(publicKeyHash.Take(8).ToArray());
            return (ulong)(long)bigInteger;
        }
    }
}
