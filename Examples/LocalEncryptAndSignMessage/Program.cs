using System;
using NxtLib;
using NxtLib.Local;
using NxtLib.Messages;
using NxtLib.Transactions;

namespace LocalEncryptAndSignMessage
{
    public class Program
    {
        private const string SecretPhrase = "bloom harmony cousin swim morning then desire patience coat worst shy click";
        private const string SenderPublicKey = "790ecfa4b3591c893e300dfa5261eb6644d850ffabe28f3be96e938859333464";
        private const string Recipient = "NXT-HMVV-XMBN-GYXK-22BKK";
        private const string RecipientPublicKey = "4e919871578a02cb2afc600c5c03414aa026d93a338ad0d098513ea0fe1b3056";

        public static void Main()
        {
            var localMessageService = new LocalMessageService();
            var localTransactionService = new LocalTransactionService();
            var messageService = new MessageService();
            var transactionService = new TransactionService();
            const bool useCompression = true;

            // Encrypt message to send locally
            const string message = "Sending a permanent message";
            var nonce = localMessageService.CreateNonce();
            var encrypted = localMessageService.EncryptTextTo(RecipientPublicKey, message, nonce, useCompression, SecretPhrase);

            // Encrypt message to self locally
            const string messageToSelf = "Note to self: sending a permanent message";
            var nonceToSelf = localMessageService.CreateNonce();
            var encryptedToSelf = localMessageService.EncryptTextTo(SenderPublicKey, messageToSelf, nonceToSelf, useCompression, SecretPhrase);

            // Prepare the transaction with your public key
            var parameters = new CreateTransactionByPublicKey(1440, Amount.Zero, SenderPublicKey)
            {
                EncryptedMessage = new CreateTransactionParameters.AlreadyEncryptedMessage(encrypted, nonce, true, useCompression),
                EncryptedMessageToSelf = new CreateTransactionParameters.AlreadyEncryptedMessageToSelf(encryptedToSelf, nonceToSelf, true, useCompression)
            };
            var unsigned = messageService.SendMessage(parameters, Recipient).Result;

            // Verify the unsigned transaction bytes from the node (only needed if you cannot trust the node)
            localTransactionService.VerifySendMessageTransactionBytes(unsigned, parameters, Recipient);

            // Sign and broadcast
            var signed = localTransactionService.SignTransaction(unsigned, SecretPhrase);
            var result = transactionService.BroadcastTransaction(new TransactionParameter(signed.ToString())).Result;
            var transactionId = result.TransactionId;
            Console.WriteLine($"Sent transaction: {transactionId}");

            Console.ReadLine();
        }
    }
}
