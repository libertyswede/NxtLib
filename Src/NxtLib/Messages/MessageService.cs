using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.Messages
{
    public class MessageService : BaseService, IMessageService
    {
        public MessageService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public MessageService(IDateTimeConverter dateTimeConverter) 
            : base(dateTimeConverter)
        {
        }

        public async Task<DecryptedDataReply> DecryptDataFrom(string senderAccountId, string data,
            IEnumerable<byte> nonce, bool uncompressDecryptedMessage, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", senderAccountId},
                {"data", data},
                {"nonce", ByteToHexStringConverter.ToHexString(nonce)},
                {"secretPhrase", secretPhrase},
                {"uncompressDecryptedMessage", uncompressDecryptedMessage.ToString()},
                {"decryptedMessageIsText", false.ToString()}
            };
            return await Get<DecryptedDataReply>("decryptFrom", queryParameters);
        }

        public async Task<DecryptedDataReply> DecryptDataFrom(string senderAccountId, EncryptedMessage encryptedMessage,
            string secretPhrase)
        {
            return await DecryptDataFrom(senderAccountId, encryptedMessage.Data.ToHexString(),
                encryptedMessage.Nonce.ToBytes(), encryptedMessage.IsCompressed, secretPhrase);
        }

        public async Task<DecryptedMessageReply> DecryptMessageFrom(string senderAccountId, string data,
            IEnumerable<byte> nonce, bool uncompressDecryptedMessage, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", senderAccountId},
                {"data", data},
                {"nonce", ByteToHexStringConverter.ToHexString(nonce)},
                {"secretPhrase", secretPhrase},
                {"uncompressDecryptedMessage", uncompressDecryptedMessage.ToString()},
                {"decryptedMessageIsText", true.ToString()}
            };
            return await Get<DecryptedMessageReply>("decryptFrom", queryParameters);
        }

        public async Task<DecryptedMessageReply> DecryptMessageFrom(string senderAccountId, EncryptedMessage encryptedMessage,
            string secretPhrase)
        {
            return await DecryptMessageFrom(senderAccountId, encryptedMessage.Data.ToHexString(), 
                encryptedMessage.Nonce.ToBytes(), encryptedMessage.IsCompressed, secretPhrase);
        }

        public async Task<EncryptedDataReply> EncryptTo(string recipient, IEnumerable<byte> messageToEncrypt,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            return await EncryptTo(recipient, ByteToHexStringConverter.ToHexString(messageToEncrypt), 
                compressMessageToEncrypt, secretPhrase);
        }

        public async Task<EncryptedDataReply> EncryptTo(string recipient, string messageToEncrypt,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"recipient", recipient},
                {"messageToEncrypt", messageToEncrypt},
                {"secretPhrase", secretPhrase},
                {"compressMessageToEncrypt", compressMessageToEncrypt.ToString()},
                {"messageToEncryptIsText", true.ToString()}
            };
            return await Get<EncryptedDataReply>("encryptTo", queryParameters);
        }

        public async Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("secretPhrase", secretPhrase, queryParameters);
            return await Get<ReadMessageReply>("readMessage", queryParameters);
        }

        public async Task<TransactionCreatedReply> SendMessage(CreateTransactionParameters parameters, string recipient = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("recipient", recipient, queryParameters);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("sendMessage", queryParameters);
        }
    }
}
