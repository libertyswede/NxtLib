using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public async Task<DecryptedTextReply> DecryptTextFrom(Account account, BinaryHexString data, BinaryHexString nonce,
            bool uncompressDecryptedMessage, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Account, account.AccountId.ToString()},
                {Parameters.Data, data.ToHexString()},
                {Parameters.Nonce, nonce.ToHexString()},
                {Parameters.SecretPhrase, secretPhrase},
                {Parameters.UncompressDecryptedMessage, uncompressDecryptedMessage.ToString()},
                {Parameters.DecryptedMessageIsText, true.ToString()}
            };
            return await Get<DecryptedTextReply>("decryptFrom", queryParameters);
        }

        public async Task<DecryptedDataReply> DecryptDataFrom(Account account, BinaryHexString data, BinaryHexString nonce, 
            bool uncompressDecryptedMessage, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Account, account.AccountId.ToString()},
                {Parameters.Data, data.ToHexString()},
                {Parameters.Nonce, nonce.ToHexString()},
                {Parameters.SecretPhrase, secretPhrase},
                {Parameters.UncompressDecryptedMessage, uncompressDecryptedMessage.ToString()},
                {Parameters.DecryptedMessageIsText, false.ToString()}
            };
            return await Get<DecryptedDataReply>("decryptFrom", queryParameters);
        }

        public async Task<IEnumerable<byte>> DownloadPrunableMessage(ulong transaction, string secretPhrase = null,
            bool? retrieve = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transaction.ToString()}};
            queryParameters.AddIfHasValue(Parameters.SecretPhrase, secretPhrase);
            queryParameters.AddIfHasValue(Parameters.Retrieve, retrieve);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);

            var url = BuildUrl("downloadPrunableMessage", queryParameters);

            using (var client = new HttpClient())
            using (var response = await client.GetAsync(url))
            using (var content = response.Content)
            {
                return await content.ReadAsByteArrayAsync();
            }
        }

        public async Task<EncryptedDataReply> EncryptTextTo(Account recipient, string messageToEncrypt,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            return await EncryptTo(recipient, messageToEncrypt, true, compressMessageToEncrypt, secretPhrase);
        }

        public async Task<EncryptedDataReply> EncryptDataTo(Account recipient, BinaryHexString messageToEncrypt,
            bool compressMessageToEncrypt, string secretPhrase)
        {
            return await EncryptTo(recipient, messageToEncrypt.ToHexString(), false,
                compressMessageToEncrypt, secretPhrase);
        }

        public async Task<PrunableMessagesReply> GetAllPrunableMessages(int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<PrunableMessagesReply>("getAllPrunableMessages", queryParameters);
        }

        public async Task<PrunableMessageReply> GetPrunableMessage(ulong transactionId, string secretPhrase = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.SecretPhrase, secretPhrase);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<PrunableMessageReply>("getPrunableMessage", queryParameters);
        }

        public async Task<PrunableMessagesReply> GetPrunableMessages(Account account, Account otherAccount = null,
            string secretPhrase = null, int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.OtherAccount, otherAccount);
            queryParameters.AddIfHasValue(Parameters.SecretPhrase, secretPhrase);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<PrunableMessagesReply>("getPrunableMessages", queryParameters);
        }

        public async Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.SecretPhrase, secretPhrase);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ReadMessageReply>("readMessage", queryParameters);
        }

        public async Task<TransactionCreatedReply> SendMessage(CreateTransactionParameters parameters,
            Account recipient = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Recipient, recipient);
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
                {Parameters.EncryptedMessageData, encryptedMessageData.ToHexString()},
                {Parameters.EncryptedMessageNonce, encryptedMessageNonce.ToHexString()}
            };
            queryParameters.AddIfHasValue(Parameters.MessageToEncryptIsText, messageToEncryptIsText);
            queryParameters.AddIfHasValue(Parameters.CompressMessageToEncrypt, compressMessageToEncrypt);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<VerifyPrunableEncryptedMessageReply>("verifyPrunableMessage", queryParameters);
        }

        private async Task<EncryptedDataReply> EncryptTo(Account recipient, string messageToEncrypt,
            bool messageToEncryptIsText, bool compressMessageToEncrypt, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Recipient, recipient.AccountId.ToString()},
                {Parameters.MessageToEncrypt, messageToEncrypt},
                {Parameters.SecretPhrase, secretPhrase},
                {Parameters.CompressMessageToEncrypt, compressMessageToEncrypt.ToString()},
                {Parameters.MessageToEncryptIsText, messageToEncryptIsText.ToString()}
            };
            return await Get<EncryptedDataReply>("encryptTo", queryParameters);
        }

        private async Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId, string message,
            bool messageIsText, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Transaction, transactionId.ToString()},
                {Parameters.Message, message},
                {Parameters.MessageIsText, messageIsText.ToString()},
                {Parameters.MessageIsPrunable, true.ToString()}
            };
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<VerifyPrunableMessageReply>("verifyPrunableMessage", queryParameters);
        }
    }
}