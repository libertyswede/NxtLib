using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.Messages
{
    public interface IMessageService
    {
        Task<DecryptedDataReply> DecryptDataFrom(string senderAccountId, string data, IEnumerable<byte> nonce,
            bool uncompressDecryptedMessage, string secretPhrase);

        Task<DecryptedDataReply> DecryptDataFrom(string senderAccountId, EncryptedMessage encryptedMessage,
            string secretPhrase);

        Task<DecryptedMessageReply> DecryptMessageFrom(string senderAccountId, string data, IEnumerable<byte> nonce,
            bool uncompressDecryptedMessage, string secretPhrase);

        Task<DecryptedMessageReply> DecryptMessageFrom(string senderAccountId, EncryptedMessage encryptedMessage, string secretPhrase);

        Task<EncryptedDataReply> EncryptTo(string recipient, string messageToEncrypt, bool compressMessageToEncrypt,
            string secretPhrase);

        Task<EncryptedDataReply> EncryptTo(string recipient, IEnumerable<byte> messageToEncrypt,
            bool compressMessageToEncrypt, string secretPhrase);

        Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase = null);
        Task<TransactionCreatedReply> SendMessage(CreateTransactionParameters parameters, string recipient = null);
    }
}