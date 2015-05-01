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

        public async Task<DecryptedReply> DecryptFrom(string accountId, BinaryHexString data, bool messageIsText, BinaryHexString nonce,
            bool uncompressDecryptedMessage, string secretPhrase)
        {
            return await DecryptFrom(accountId, data.ToHexString(), messageIsText, nonce.ToHexString(), 
                uncompressDecryptedMessage, secretPhrase);
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

        public async Task<PrunableMessageReply> GetPrunableMessage(ulong transactionId, string secretPhrase = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("secretPhrase", secretPhrase, queryParameters);
            return await Get<PrunableMessageReply>("getPrunableMessage", queryParameters);
        }

        public async Task<PrunableMessagesReply> GetPrunableMessages(string accountId, string otherAccountId = null, string secretPhrase = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", accountId}};
            AddToParametersIfHasValue("otherAccount", otherAccountId, queryParameters);
            AddToParametersIfHasValue("secretPhrase", secretPhrase, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<PrunableMessagesReply>("getPrunableMessages", queryParameters);
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

        public async Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId, string message)
        {
            return await VerifyPrunableMessage(transactionId, message, true);
        }

        public async Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId, IEnumerable<byte> data)
        {
            return await VerifyPrunableMessage(transactionId, ByteToHexStringConverter.ToHexString(data), false);
        }

        private async Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId, string message, bool messageIsText)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()},
                {"message", message},
                {"messageIsText", messageIsText.ToString()},
                {"messageIsPrunable", true.ToString()}
            };
            return await Get<VerifyPrunableMessageReply>("verifyPrunableMessage", queryParameters);
        }

        public async Task<VerifyPrunableEncryptedMessageReply> VerifyPrunableEncryptedMessage(ulong transactionId, BinaryHexString encryptedMessageData, 
            BinaryHexString encryptedMessageNonce, bool? messageToEncryptIsText = null, bool? compressMessageToEncrypt = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()},
                {"encryptedMessageData", encryptedMessageData.ToHexString()},
                {"encryptedMessageNonce", encryptedMessageNonce.ToHexString()},
                {"encryptedMessageIsPrunable", true.ToString()}
            };
            AddToParametersIfHasValue("messageToEncryptIsText", messageToEncryptIsText, queryParameters);
            AddToParametersIfHasValue("compressMessageToEncrypt", compressMessageToEncrypt, queryParameters);
            return await Get<VerifyPrunableEncryptedMessageReply>("verifyPrunableMessage", queryParameters);
        }
    }
}
