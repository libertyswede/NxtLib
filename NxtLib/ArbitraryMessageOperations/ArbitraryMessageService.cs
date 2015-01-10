using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.ArbitraryMessageOperations
{
    public interface IArbitraryMessageService
    {
        Task<DecryptedMessage> DecryptFrom(string accountId, string data, string nonce, string secretPhrase, bool? decryptedMessageIsText = null);
        Task<EncryptedMessage> EncryptTo(string recipient, string messageToEncrypt, string secretPhrase, bool? messageToEncryptIsText);
        Task<ReadMessage> ReadMessage(ulong transactionId, string secretPhrase);
        Task<TransactionCreated> SendMessage(CreateTransactionParameters parameters, MessagesToSend messagesToSend, string recipient = null);
    }

    public class ArbitraryMessageService : BaseService, IArbitraryMessageService
    {
        public ArbitraryMessageService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public ArbitraryMessageService(IDateTimeConverter dateTimeConverter) 
            : base(dateTimeConverter)
        {
        }

        public async Task<DecryptedMessage> DecryptFrom(string accountId, string data, string nonce, string secretPhrase, bool? decryptedMessageIsText = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", accountId},
                {"data", data},
                {"nonce", nonce},
                {"secretPhrase", secretPhrase }
            };
            if (decryptedMessageIsText.HasValue)
            {
                queryParameters.Add("decryptedMessageIsText", decryptedMessageIsText.Value.ToString());
            }
            return await Get<DecryptedMessage>("decryptFrom", queryParameters);
        }

        public async Task<EncryptedMessage> EncryptTo(string recipient, string messageToEncrypt, string secretPhrase, bool? messageToEncryptIsText)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"recipient", recipient},
                {"messageToEncrypt", messageToEncrypt},
                {"secretPhrase", secretPhrase}
            };
            if (messageToEncryptIsText.HasValue)
            {
                queryParameters.Add("messageToEncryptIsText", messageToEncryptIsText.ToString());
            }
            return await Get<EncryptedMessage>("encryptTo", queryParameters);
        }

        public async Task<ReadMessage> ReadMessage(ulong transactionId, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()},
                {"secretPhrase", secretPhrase}
            };
            return await Get<ReadMessage>("readMessage", queryParameters);
        }

        public async Task<TransactionCreated> SendMessage(CreateTransactionParameters parameters, MessagesToSend messagesToSend, string recipient = null)
        {
            var queryParameters = messagesToSend.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            if (!string.IsNullOrEmpty(recipient))
            {
                queryParameters.Add("recipient", recipient);
            }
            return await Post<TransactionCreated>("sendMessage", queryParameters);
        }
    }
}
