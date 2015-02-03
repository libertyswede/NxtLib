using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.Messages
{
    public interface IMessageService
    {
        Task<DecryptedDataReply> DecryptDataFrom(string senderAccountId, string data, IEnumerable<byte> nonce, string secretPhrase);
        Task<DecryptedMessageReply> DecryptMessageFrom(string senderAccountId, string data, IEnumerable<byte> nonce, string secretPhrase);
        Task<EncryptedDataReply> EncryptTo(string recipient, string messageToEncrypt, string secretPhrase);
        Task<EncryptedDataReply> EncryptTo(string recipient, IEnumerable<byte> messageToEncrypt, string secretPhrase);
        Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase = null);
        Task<TransactionCreated> SendMessage(CreateTransactionParameters parameters, string recipient = null);
    }
}