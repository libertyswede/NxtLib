using System;
using NxtLib;
using NxtLib.Accounts;
using NxtLib.AssetExchange;
using NxtLib.Local;
using NxtLib.Transactions;

namespace LocalSignedAssetPurchase
{
    // A video describing this code in detail can be found here: https://www.youtube.com/watch?v=_H_xbLSSGkY

    public class Program
    {
        private const string SecretPhrase = "secretPhrase"; // Set your secret phrase here!
        private const ulong DeBuNeAssetId = 6926770479287491943;
        private const string NxtUri = "http://178.21.114.156/nxt";

        public static void Main(string[] args)
        {
            var localAccountService = new LocalAccountService();
            var localTransactionService = new LocalTransactionService();

            // Step 1, Locally generate a public key from the secret phrase
            var account = localAccountService.GetAccount(AccountIdLocator.BySecretPhrase(SecretPhrase));
            var publicKey = account.PublicKey;

            // Step 2, Place unsigned bid order
            var assetService = new AssetExchangeService(NxtUri);
            var qntFactor = (int)Math.Pow(10, 4); // DeBuNe asset use 4 decimals
            var createTransaction = new CreateTransactionByPublicKey(1440, Amount.OneNxt, publicKey);
            var unsignedBidOrder = assetService.PlaceBidOrder(DeBuNeAssetId, 1 * qntFactor, Amount.CreateAmountFromNxt(26.9M / qntFactor), createTransaction).Result;

            // Step 3, Sign the bid order locally
            var signedBidOrder = localTransactionService.SignTransaction(unsignedBidOrder, SecretPhrase);

            // Step 4, Broadcast the signed bid order
            var transactionService = new TransactionService(NxtUri);
            var broadcastResult = transactionService.BroadcastTransaction(new TransactionParameter(signedBidOrder.ToString())).Result;
            Console.WriteLine("Transaction id: " + broadcastResult.TransactionId);

            Console.ReadLine();
        }
    }
}
