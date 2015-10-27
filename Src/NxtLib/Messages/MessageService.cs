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
            : base(baseAddress)
        {
        }

        public async Task<DecryptedReply> DecryptFrom(Account account, BinaryHexString data,
            bool decryptedMessageIsText, BinaryHexString nonce,
            bool uncompressDecryptedMessage, string secretPhrase)
        {
            return await DecryptFrom(account, data.ToHexString(), decryptedMessageIsText, nonce.ToHexString(),
                uncompressDecryptedMessage, secretPhrase);
        }

        public async Task<DecryptedReply> DecryptFrom(Account account, EncryptedMessage encryptedMessage,
            string secretPhrase)
        {
            return await DecryptFrom(account, encryptedMessage.Data.ToHexString(), encryptedMessage.IsText,
                encryptedMessage.Nonce.ToHexString(), encryptedMessage.IsCompressed, secretPhrase);
        }

        public async Task<EncryptedDataReply> EncryptTo(Account recipient, string messageToEncrypt,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            return await EncryptTo(recipient, messageToEncrypt, true, compressMessageToEncrypt, secretPhrase);
        }

        public async Task<EncryptedDataReply> EncryptTo(Account recipient, IEnumerable<byte> messageToEncrypt,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            return await EncryptTo(recipient, ByteToHexStringConverter.ToHexString(messageToEncrypt), false,
                compressMessageToEncrypt, secretPhrase);
        }

        public async Task<PrunableMessagesReply> GetAllPrunableMessages(int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(timestamp), timestamp);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<PrunableMessagesReply>("getAllPrunableMessages", queryParameters);
        }

        public async Task<PrunableMessageReply> GetPrunableMessage(ulong transactionId, string secretPhrase = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(nameof(secretPhrase), secretPhrase);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<PrunableMessageReply>("getPrunableMessage", queryParameters);
        }

        public async Task<PrunableMessagesReply> GetPrunableMessages(Account account, Account otherAccount = null,
            string secretPhrase = null, int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(nameof(otherAccount), otherAccount);
            queryParameters.AddIfHasValue(nameof(secretPhrase), secretPhrase);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(timestamp), timestamp);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<PrunableMessagesReply>("getPrunableMessages", queryParameters);
        }

        public async Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(nameof(secretPhrase), secretPhrase);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<ReadMessageReply>("readMessage", queryParameters);
        }

        public async Task<TransactionCreatedReply> SendMessage(CreateTransactionParameters parameters,
            Account recipient = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(nameof(recipient), recipient);
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
                {Parameters.Transaction, transactionId.ToString()},
                {nameof(encryptedMessageData), encryptedMessageData.ToHexString()},
                {nameof(encryptedMessageNonce), encryptedMessageNonce.ToHexString()}
            };
            queryParameters.AddIfHasValue(nameof(messageToEncryptIsText), messageToEncryptIsText);
            queryParameters.AddIfHasValue(nameof(compressMessageToEncrypt), compressMessageToEncrypt);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<VerifyPrunableEncryptedMessageReply>("verifyPrunableMessage", queryParameters);
        }

        private async Task<DecryptedReply> DecryptFrom(Account account, string data, bool decryptedMessageIsText,
            string nonce,
            bool uncompressDecryptedMessage, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(account), account.AccountId.ToString()},
                {nameof(data), data},
                {nameof(nonce), nonce},
                {nameof(secretPhrase), secretPhrase},
                {nameof(uncompressDecryptedMessage), uncompressDecryptedMessage.ToString()},
                {nameof(decryptedMessageIsText), decryptedMessageIsText.ToString()}
            };
            return await Get<DecryptedReply>("decryptFrom", queryParameters);
        }

        private async Task<EncryptedDataReply> EncryptTo(Account recipient, string messageToEncrypt,
            bool messageToEncryptIsText, bool compressMessageToEncrypt, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(recipient), recipient.AccountId.ToString()},
                {nameof(messageToEncrypt), messageToEncrypt},
                {nameof(secretPhrase), secretPhrase},
                {nameof(compressMessageToEncrypt), compressMessageToEncrypt.ToString()},
                {nameof(messageToEncryptIsText), messageToEncryptIsText.ToString()}
            };
            return await Get<EncryptedDataReply>("encryptTo", queryParameters);
        }

        private async Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId, string message,
            bool messageIsText, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Transaction, transactionId.ToString()},
                {nameof(message), message},
                {nameof(messageIsText), messageIsText.ToString()},
                {Parameters.MessageIsPrunable, true.ToString()}
            };
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<VerifyPrunableMessageReply>("verifyPrunableMessage", queryParameters);
        }
    }
}