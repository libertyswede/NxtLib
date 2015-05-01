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

        public async Task<DecryptedReply> DecryptFrom(string accountId, string message, BinaryHexString nonce,
            bool uncompressDecryptedMessage, string secretPhrase)
        {
            return await DecryptFrom(accountId, message, true, nonce.ToHexString(), 
                uncompressDecryptedMessage, secretPhrase);
        }

        public async Task<DecryptedReply> DecryptFrom(string accountId, IEnumerable<byte> data,
            BinaryHexString nonce, bool uncompressDecryptedMessage, string secretPhrase)
        {
            return await DecryptFrom(accountId, ByteToHexStringConverter.ToHexString(data), false, 
                nonce.ToHexString(), uncompressDecryptedMessage, secretPhrase);
        }
        
        public async Task<DecryptedReply> DecryptFrom(string accountId, EncryptedMessage encryptedMessage, string secretPhrase)
        {
            return await DecryptFrom(accountId, encryptedMessage.Data.ToHexString(), encryptedMessage.IsText, 
                encryptedMessage.Nonce.ToHexString(), encryptedMessage.IsCompressed, secretPhrase);
        }

        private async Task<DecryptedReply> DecryptFrom(string accountId, string data, bool decryptedMessageIsText, string nonce,
            bool uncompressDecryptedMessage, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", accountId},
                {"data", data},
                {"nonce", nonce},
                {"secretPhrase", secretPhrase},
                {"uncompressDecryptedMessage", uncompressDecryptedMessage.ToString()},
                {"decryptedMessageIsText", decryptedMessageIsText.ToString()}
            };
            return await Get<DecryptedReply>("decryptFrom", queryParameters);
        }

        public async Task<EncryptedDataReply> EncryptTo(string recipientAccountId, string message,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            return await EncryptTo(recipientAccountId, message, true, compressMessageToEncrypt, secretPhrase);
        }

        public async Task<EncryptedDataReply> EncryptTo(string recipientAccountId, IEnumerable<byte> data,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            return await EncryptTo(recipientAccountId, ByteToHexStringConverter.ToHexString(data), false,
                compressMessageToEncrypt, secretPhrase);
        }

        public async Task<PrunableMessagesReply> GetAllPrunableMessages(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<PrunableMessagesReply>("getAllPrunableMessages", queryParameters);
        }

        private async Task<EncryptedDataReply> EncryptTo(string recipientAccountId, string messageToEncrypt, bool messageToEncryptIsText,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"recipient", recipientAccountId},
                {"messageToEncrypt", messageToEncrypt},
                {"secretPhrase", secretPhrase},
                {"compressMessageToEncrypt", compressMessageToEncrypt.ToString()},
                {"messageToEncryptIsText", messageToEncryptIsText.ToString()}
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
