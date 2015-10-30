using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.Messages
{
    public interface IMessageService
    {
        Task<DecryptedTextReply> DecryptTextFrom(Account account, BinaryHexString data, BinaryHexString nonce, 
            bool uncompressDecryptedMessage, string secretPhrase);

        Task<DecryptedDataReply> DecryptDataFrom(Account account, BinaryHexString data, BinaryHexString nonce,
            bool uncompressDecryptedMessage, string secretPhrase);

        Task<EncryptedDataReply> EncryptTextTo(Account recipient, string messageToEncrypt, bool compressMessageToEncrypt,
            string secretPhrase);

        Task<EncryptedDataReply> EncryptDataTo(Account recipient, BinaryHexString messageToEncrypt, bool compressMessageToEncrypt, string secretPhrase);

        Task<PrunableMessagesReply> GetAllPrunableMessages(int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<PrunableMessageReply> GetPrunableMessage(ulong transactionId, string secretPhrase = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<PrunableMessagesReply> GetPrunableMessages(Account account, Account otherAccount = null,
            string secretPhrase = null, int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ReadMessageReply> ReadMessage(ulong transactionId, string secretPhrase = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> SendMessage(CreateTransactionParameters parameters, Account recipient = null);

        Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId, string message,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<VerifyPrunableMessageReply> VerifyPrunableMessage(ulong transactionId, IEnumerable<byte> message,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<VerifyPrunableEncryptedMessageReply> VerifyPrunableEncryptedMessage(ulong transactionId,
            BinaryHexString encryptedMessageData, BinaryHexString encryptedMessageNonce,
            bool? messageToEncryptIsText = null, bool? compressMessageToEncrypt = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);
    }
}