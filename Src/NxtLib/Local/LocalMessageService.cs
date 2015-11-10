using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NxtLib.Internal.LocalSign;

namespace NxtLib.Local
{
    public interface ILocalMessageService
    {
        BinaryHexString CreateNonce();
        BinaryHexString EncryptTextTo(BinaryHexString recipientPublicKey, string message, BinaryHexString nonce, bool compress, string secretPhrase);
        BinaryHexString EncryptDataTo(BinaryHexString recipientPublicKey, BinaryHexString data, BinaryHexString nonce, bool compress, string secretPhrase);
        string DecryptTextFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompress, string secretPhrase);
        byte[] DecryptDataFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompress, string secretPhrase);
    }

    public class LocalMessageService : ILocalMessageService
    {
        private readonly Compressor _compressor = new Compressor();
        private readonly Crypto _crypto = new Crypto();

        public BinaryHexString CreateNonce()
        {
            var nonce = new byte[32];
            var random = RandomNumberGenerator.Create();
            random.GetBytes(nonce);
            return nonce;
        }

        public BinaryHexString EncryptTextTo(BinaryHexString recipientPublicKey, string message, BinaryHexString nonce, bool compress, string secretPhrase)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            return EncryptDataTo(recipientPublicKey, messageBytes, nonce, compress, secretPhrase);
        }

        public BinaryHexString EncryptDataTo(BinaryHexString recipientPublicKey, BinaryHexString data, BinaryHexString nonce, bool compress, string secretPhrase)
        {
            var recipientPublicKeyBytes = recipientPublicKey.ToBytes().ToArray();
            var nonceBytes = nonce.ToBytes().ToArray();
            var dataBytes = data.ToBytes().ToArray();
            if (compress)
            {
                dataBytes = _compressor.GzipCompress(dataBytes);
            }
            return _crypto.AesEncryptTo(recipientPublicKeyBytes, dataBytes, nonceBytes, secretPhrase);
        }

        public string DecryptTextFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompress, string secretPhrase)
        {
            var decrypted = DecryptDataFrom(senderPublicKey, data, nonce, uncompress, secretPhrase);
            var message = Encoding.UTF8.GetString(decrypted, 0, decrypted.Length);
            return message;
        }

        public byte[] DecryptDataFrom(BinaryHexString senderPublicKey, BinaryHexString data, BinaryHexString nonce, bool uncompress, string secretPhrase)
        {
            var senderPublicKeyBytes = senderPublicKey.ToBytes().ToArray();
            var dataBytes = data.ToBytes().ToArray();
            var nonceBytes = nonce.ToBytes().ToArray();
            var decrypted = _crypto.AesDecryptFrom(senderPublicKeyBytes, dataBytes, nonceBytes, secretPhrase);
            if (uncompress)
            {
                decrypted = _compressor.GzipUncompress(decrypted);
            }
            return decrypted;
        }
    }
}