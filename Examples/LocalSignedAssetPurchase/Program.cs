using System;
using Newtonsoft.Json.Linq;
using NxtLib;
using NxtLib.AssetExchange;
using NxtLib.Local;
using NxtLib.Transactions;

namespace LocalSignedAssetPurchase
{
    class Program
    {
        private const string SecretPhrase = "secretPhrase";
        private const ulong DeBuNeAssetId = 6926770479287491943;

        static void Main(string[] args)
        {
            var localCrypto = new LocalCrypto();
            
            var publicKey = GeneratePublicKey(localCrypto);
            var unsignedBidOrder = PlaceUnsignedBidOrder(publicKey);
            var json = localCrypto.SignTransaction(unsignedBidOrder, SecretPhrase);
            BroadcastTransaction(json);

            Console.ReadLine();
        }

        private static void BroadcastTransaction(JObject json)
        {
            var transactionService = new TransactionService();
            var broadcastReply = transactionService.BroadcastTransaction(new TransactionParameter(json)).Result;
            Console.WriteLine("Transaction created, transactionId: " + broadcastReply.TransactionId);
        }

        private static TransactionCreatedReply PlaceUnsignedBidOrder(BinaryHexString publicKey)
        {
            var service = new AssetExchangeService();
            var asset = service.GetAsset(DeBuNeAssetId).Result;
            var assetQntFactor = (long) Math.Pow(10, asset.Decimals);
            var createTransaction = new CreateTransactionByPublicKey(1440, Amount.OneNxt, publicKey);
            return service.PlaceBidOrder(DeBuNeAssetId, 1*assetQntFactor, 
                Amount.CreateAmountFromNxt(30M/assetQntFactor), createTransaction).Result;
        }

        private static BinaryHexString GeneratePublicKey(ILocalCrypto localCrypto)
        {
            var publicKey = localCrypto.GetPublicKey(SecretPhrase);
            Console.WriteLine("My public key is: " + publicKey.ToHexString());
            return publicKey;
        }
    }
}
