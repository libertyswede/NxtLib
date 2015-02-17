using System;
using NxtLib;
using NxtLib.AssetExchange;
using NxtLib.Local;
using NxtLib.Transactions;

namespace LocalSignedAssetPurchase
{
    class Program
    {
        private const string SecretPhrase = "abc123";
        private const string TestnetUri = "http://localhost:6876/nxt";

        static void Main(string[] args)
        {
            // Step 1, generate public key locally
            var localCrypto = new LocalCrypto();
            var publicKey = localCrypto.GetPublicKey(SecretPhrase);
            Console.WriteLine("My public key is: " + publicKey.ToHexString());

            // Step 2, use public key to let a NXT server generate an unsigned transaction
            var assetExchangeService = new AssetExchangeService(TestnetUri);
            const int decimals = 8;
            var amount = Amount.CreateAmountFromNxt(1 / (decimal)Math.Pow(10, decimals));
            var quantity = (long) Math.Pow(10, decimals);
            var placeBidOrderReply = assetExchangeService.PlaceBidOrder(9944395557828084479, quantity, amount, new CreateTransactionByPublicKey(1440, Amount.OneNxt, publicKey)).Result;

            // Step 3, sign the transaction locally
            var json = localCrypto.SignTransaction(placeBidOrderReply, SecretPhrase);

            // Step 4, Broadcast the signed transaction
            var transactionService = new TransactionService(TestnetUri);
            var broadcastReply = transactionService.BroadcastTransaction(new TransactionParameter(json)).Result;
            Console.WriteLine("Transaction created, transactionId: " + broadcastReply.TransactionId);
        }
    }
}
