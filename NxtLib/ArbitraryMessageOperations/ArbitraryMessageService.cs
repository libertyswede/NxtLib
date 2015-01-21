using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.ArbitraryMessageOperations
{
    public interface IArbitraryMessageService
    {
        Task<DecryptedDataReply> DecryptDataFrom(string senderAccountId, string data, IEnumerable<byte> nonce, string secretPhrase);
        Task<DecryptedMessageReply> DecryptMessageFrom(string senderAccountId, string data, IEnumerable<byte> nonce, string secretPhrase);
        Task<EncryptedDataReply> EncryptTo(string recipient, string messageToEncrypt, string secretPhrase);
        Task<EncryptedDataReply> EncryptTo(string recipient, IEnumerable<byte> messageToEncrypt, string secretPhrase);
        Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase);
        Task<TransactionCreated> SendMessage(CreateTransactionParameters parameters, string recipient = null);
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

        public async Task<DecryptedDataReply> DecryptDataFrom(string senderAccountId, string data, IEnumerable<byte> nonce, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", senderAccountId},
                {"data", data},
                {"nonce", ByteToHexStringConverter.ToHexString(nonce)},
                {"secretPhrase", secretPhrase },
                {"decryptedMessageIsText", false.ToString()}
            };
            return await Get<DecryptedDataReply>("decryptFrom", queryParameters);
        }

        public async Task<DecryptedMessageReply> DecryptMessageFrom(string senderAccountId, string data, IEnumerable<byte> nonce, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", senderAccountId},
                {"data", data},
                {"nonce", ByteToHexStringConverter.ToHexString(nonce)},
                {"secretPhrase", secretPhrase },
                {"decryptedMessageIsText", true.ToString()}
            };
            return await Get<DecryptedMessageReply>("decryptFrom", queryParameters);
        }

        public async Task<EncryptedDataReply> EncryptTo(string recipient, IEnumerable<byte> messageToEncrypt, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"recipient", recipient},
                {"messageToEncrypt", ByteToHexStringConverter.ToHexString(messageToEncrypt)},
                {"secretPhrase", secretPhrase},
                {"messageToEncryptIsText", false.ToString()}
            };
            return await Get<EncryptedDataReply>("encryptTo", queryParameters);
        }

        public async Task<EncryptedDataReply> EncryptTo(string recipient, string messageToEncrypt, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"recipient", recipient},
                {"messageToEncrypt", messageToEncrypt},
                {"secretPhrase", secretPhrase},
                {"messageToEncryptIsText", true.ToString()}
            };
            return await Get<EncryptedDataReply>("encryptTo", queryParameters);
        }

        public async Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()},
                {"secretPhrase", secretPhrase}
            };
            return await Get<ReadMessageReply>("readMessage", queryParameters);
        }

        public async Task<TransactionCreated> SendMessage(CreateTransactionParameters parameters, string recipient = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("recipient", recipient, queryParameters);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("sendMessage", queryParameters);
        }
    }
}
