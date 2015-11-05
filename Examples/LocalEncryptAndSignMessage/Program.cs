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
            var localCrypto = new LocalCrypto();
            var messageService = new MessageService();
            var transactionService = new TransactionService();

            // Encrypt message to send locally
            const string message = "Sending a permanent message";
            var nonce = localCrypto.CreateNonce();
            var encrypted = localCrypto.EncryptTextTo(RecipientPublicKey, message, nonce, true, SecretPhrase);

            // Encrypt message to self locally
            const string messageToSelf = "Note to self: sending a permanent message";
            var nonceToSelf = localCrypto.CreateNonce();
            var encryptedToSelf = localCrypto.EncryptTextTo(SenderPublicKey, messageToSelf, nonceToSelf, true, SecretPhrase);

            // Prepare the transaction with your public key
            var parameters = new CreateTransactionByPublicKey(1440, Amount.OneNxt, SenderPublicKey)
            {
                EncryptedMessage = new CreateTransactionParameters.AlreadyEncryptedMessage(encrypted, nonce, true),
                EncryptedMessageToSelf = new CreateTransactionParameters.AlreadyEncryptedMessageToSelf(encryptedToSelf, nonceToSelf, true)
            };
            var unsigned = messageService.SendMessage(parameters, Recipient).Result;

            // Sign and broadcast
            var signed = localCrypto.SignTransaction(unsigned, SecretPhrase);
            var result = transactionService.BroadcastTransaction(new TransactionParameter(signed.ToString())).Result;
            var transactionId = result.TransactionId;
            Console.WriteLine($"Sent transaction: {transactionId}");
        }
    }
}
