using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Messages
{
    public class MessageService : BaseService, IMessageService
    {
        public MessageService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public MessageService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<DecryptedReply> DecryptFrom(string accountId, BinaryHexString data,
            bool decryptedMessageIsText, BinaryHexString nonce,
            bool uncompressDecryptedMessage, string secretPhrase)
        {
            return await DecryptFrom(accountId, data.ToHexString(), decryptedMessageIsText, nonce.ToHexString(),
                uncompressDecryptedMessage, secretPhrase);
        }

        public async Task<DecryptedReply> DecryptFrom(string accountId, EncryptedMessage encryptedMessage,
            string secretPhrase)
        {
            return await DecryptFrom(accountId, encryptedMessage.Data.ToHexString(), encryptedMessage.IsText,
                encryptedMessage.Nonce.ToHexString(), encryptedMessage.IsCompressed, secretPhrase);
        }

        public async Task<EncryptedDataReply> EncryptTo(string recipientAccountId, string messageToEncrypt,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            return await EncryptTo(recipientAccountId, messageToEncrypt, true, compressMessageToEncrypt, secretPhrase);
        }

        public async Task<EncryptedDataReply> EncryptTo(string recipientAccountId, IEnumerable<byte> messageToEncrypt,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            return await EncryptTo(recipientAccountId, ByteToHexStringConverter.ToHexString(messageToEncrypt), false,
                compressMessageToEncrypt, secretPhrase);
        }

        public async Task<PrunableMessagesReply> GetAllPrunableMessages(int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("timestamp", timestamp, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<PrunableMessagesReply>("getAllPrunableMessages", queryParameters);
        }

        public async Task<PrunableMessageReply> GetPrunableMessage(ulong transactionId, string secretPhrase = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("secretPhrase", secretPhrase, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<PrunableMessageReply>("getPrunableMessage", queryParameters);
        }

        public async Task<PrunableMessagesReply> GetPrunableMessages(string accountId, string otherAccountId = null,
            string secretPhrase = null, int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", accountId}};
            AddToParametersIfHasValue("otherAccount", otherAccountId, queryParameters);
            AddToParametersIfHasValue("secretPhrase", secretPhrase, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("timestamp", timestamp, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<PrunableMessagesReply>("getPrunableMessages", queryParameters);
        }

        public async Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("secretPhrase", secretPhrase, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<ReadMessageReply>("readMessage", queryParameters);
        }

        public async Task<TransactionCreatedReply> SendMessage(CreateTransactionParameters parameters,
            string recipient = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("recipient", recipient, queryParameters);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("sendMessage", queryParameters);
        }

        public async Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId, string message,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            return await VerifyPrunableMessage(transactionId, message, true, requireBlock, requireLastBlock);
        }

        public async Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId,
            IEnumerable<byte> message, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            return await VerifyPrunableMessage(transactionId, ByteToHexStringConverter.ToHexString(message), 
                false, requireBlock, requireLastBlock);
        }

        public async Task<VerifyPrunableEncryptedMessageReply> VerifyPrunableEncryptedMessage(ulong transactionId,
            BinaryHexString encryptedMessageData, BinaryHexString encryptedMessageNonce,
            bool? messageToEncryptIsText = null, bool? compressMessageToEncrypt = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()},
                {"encryptedMessageData", encryptedMessageData.ToHexString()},
                {"encryptedMessageNonce", encryptedMessageNonce.ToHexString()}
            };
            AddToParametersIfHasValue("messageToEncryptIsText", messageToEncryptIsText, queryParameters);
            AddToParametersIfHasValue("compressMessageToEncrypt", compressMessageToEncrypt, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<VerifyPrunableEncryptedMessageReply>("verifyPrunableMessage", queryParameters);
        }

        private async Task<DecryptedReply> DecryptFrom(string accountId, string data, bool decryptedMessageIsText,
            string nonce,
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

        private async Task<EncryptedDataReply> EncryptTo(string recipientAccountId, string messageToEncrypt,
            bool messageToEncryptIsText,
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

        private async Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId, string message,
            bool messageIsText, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()},
                {"message", message},
                {"messageIsText", messageIsText.ToString()},
                {"messageIsPrunable", true.ToString()}
            };
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<VerifyPrunableMessageReply>("verifyPrunableMessage", queryParameters);
        }
    }
}