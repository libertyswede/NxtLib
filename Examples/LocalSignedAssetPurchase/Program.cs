using System;
using NxtLib;
using NxtLib.AssetExchange;
using NxtLib.Local;
using NxtLib.Transactions;

namespace LocalSignedAssetPurchase
{
    class Program
    {
        private const string SecretPhrase = "secretPhrase"; // Set your secret phrase here!
        private const ulong DeBuNeAssetId = 6926770479287491943;
        private const string NxtUri = "http://178.21.114.156/nxt";

        static void Main(string[] args)
        {
            // Step 1, Locally generate a public key from the secret phrase
            var localCrypto = new LocalCrypto();
            var publicKey = localCrypto.GetPublicKey(SecretPhrase);

            // Step 2, Place unsigned bid order
            var assetService = new AssetExchangeService(NxtUri);
            var qntFactor = (int)Math.Pow(10, 4); // DeBuNe asset use 4 decimals
            var createTransaction = new CreateTransactionByPublicKey(1440, Amount.OneNxt, publicKey);
            var unsignedBidOrder = assetService.PlaceBidOrder(DeBuNeAssetId, 1 * qntFactor, Amount.CreateAmountFromNxt(26.9M / qntFactor), createTransaction).Result;

            // Step 3, Sign the bid order locally
            var signedBidOrder = localCrypto.SignTransaction(unsignedBidOrder, SecretPhrase);

            // Step 4, Broadcast the signed bid order
            var transactionService = new TransactionService(NxtUri);
            var broadcastResult = transactionService.BroadcastTransaction(new TransactionParameter(signedBidOrder)).Result;
            Console.WriteLine("Transaction id: " + broadcastResult.TransactionId);

            Console.ReadLine();
        }
    }
}
