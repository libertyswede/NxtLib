using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.Messages
{
    public interface IMessageService
    {
        Task<DecryptedReply> DecryptFrom(string accountId, string message, BinaryHexString nonce, 
            bool uncompressDecryptedMessage, string secretPhrase);

        Task<DecryptedReply> DecryptFrom(string accountId, IEnumerable<byte> data, BinaryHexString nonce, 
            bool uncompressDecryptedMessage, string secretPhrase);

        Task<DecryptedReply> DecryptFrom(string accountId, EncryptedMessage encryptedMessage, string secretPhrase);

        Task<EncryptedDataReply> EncryptTo(string recipientAccountId, string message, bool compressMessageToEncrypt, string secretPhrase);

        Task<EncryptedDataReply> EncryptTo(string recipientAccountId, IEnumerable<byte> data, bool compressMessageToEncrypt, string secretPhrase);

        Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase = null);

        Task<TransactionCreatedReply> SendMessage(CreateTransactionParameters parameters, string recipient = null);
    }
}