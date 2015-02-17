using System;

namespace LocalSignedAssetPurchase
{
    class Program
    {
        private const string SecretPhrase = "abc123";

        static void Main(string[] args)
        {
            // Step 1, generate public key locally
            var localCrypto = new LocalCrypto();
            var publicKey = localCrypto.GetPublicKey(SecretPhrase);
            Console.WriteLine("My public key is: " + publicKey.ToHexString());

            // Step 2, use public key to let a NXT server generate an unsigned transaction
            var assetExchangeService = new AssetExchangeService();
            var placeAskOrderReply = assetExchangeService.PlaceAskOrder(123, 1 * 100, Amount.FromNxt(30), new CreateTransactionByPublicKey(1440, Amount.OneNxt, publicKey)).Result;

            // Step 3, sign the transaction locally
            var json = localCrypto.SignTransaction(placeAskOrderReply, SecretPhrase);

            // Step 4, Broadcast the signed transaction
            var transactionService = new TransactionService();
            var broadcastReply = transactionService.BroadcastTransaction(new TransactionParameter(json)).Result;
            Console.WriteLine("Transaction created, transactionId: " + broadcastReply.TransactionId);
        }
    }
}
