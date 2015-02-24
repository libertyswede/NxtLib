using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace NxtLib.Internal.LocalSign
{
    // Crypto class specific for WinRT environment
    internal class Crypto
    {
        private readonly HashAlgorithmProvider _hashAlgorithmProvider;

        internal Crypto()
        {
            _hashAlgorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
        }

        private byte[] ComputeHash(string message)
        {
            return ComputerHash(CryptographicBuffer.ConvertStringToBinary(message, BinaryStringEncoding.Utf8));
        }

        private byte[] ComputeHash(byte[] message)
        {
            return ComputerHash(CryptographicBuffer.CreateFromByteArray(message));
        }

        private byte[] ComputerHash(IBuffer binaryMessage)
        {
            var hash = _hashAlgorithmProvider.HashData(binaryMessage);
            if (hash.Length != _hashAlgorithmProvider.HashLength)
            {
                throw new Exception("There was an error creating the hash");
            }
            return hash.ToArray();
        }

        public byte[] Sign(byte[] message, string secretPhrase)
        {
            var p = new byte[32];
            var s = new byte[32];

            Curve25519.Keygen(p, s, ComputeHash(secretPhrase));

            var m = ComputeHash(message);
            var sha256 = _hashAlgorithmProvider.CreateHash();
            sha256.Append(CryptographicBuffer.CreateFromByteArray(m));
            sha256.Append(CryptographicBuffer.CreateFromByteArray(s));
            var x = sha256.GetValueAndReset().ToArray();

            var y = new byte[32];
            Curve25519.Keygen(y, null, x);

            sha256.Append(CryptographicBuffer.CreateFromByteArray(m));
            sha256.Append(CryptographicBuffer.CreateFromByteArray(y));
            var h = sha256.GetValueAndReset().ToArray();

            var v = new byte[32];
            Curve25519.Sign(v, h, x, s);

            var signature = v.Concat(h).ToArray();
            return signature;
        }

        public BinaryHexString GetPublicKey(string secretPhrase)
        {
            var publicKey = new byte[32];
            var hashedSecretPhrase = ComputeHash(secretPhrase);
            Curve25519.Keygen(publicKey, null, hashedSecretPhrase);
            var binaryHexString = new BinaryHexString(publicKey);
            return binaryHexString;
        }
    }
}
