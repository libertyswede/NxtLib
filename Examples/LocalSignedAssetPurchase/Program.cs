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
        private const string NxtUri = "http://178.21.114.156/nxt";

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
            var transactionService = new TransactionService(NxtUri);
            var broadcastReply = transactionService.BroadcastTransaction(new TransactionParameter(json)).Result;
            Console.WriteLine("Transaction created, transactionId: " + broadcastReply.TransactionId);
        }

        private static TransactionCreatedReply PlaceUnsignedBidOrder(BinaryHexString publicKey)
        {
            var service = new AssetExchangeService(NxtUri);
            var qntFactor = (int) Math.Pow(10, 4);
            var createTransaction = new CreateTransactionByPublicKey(1440, Amount.OneNxt, publicKey);
            return service.PlaceBidOrder(DeBuNeAssetId, 1*qntFactor, Amount.CreateAmountFromNxt(26.9M/qntFactor), createTransaction).Result;
        }

        private static BinaryHexString GeneratePublicKey(ILocalCrypto localCrypto)
        {
            var publicKey = localCrypto.GetPublicKey(SecretPhrase);
            Console.WriteLine("My public key is: " + publicKey.ToHexString());
            return publicKey;
        }
    }
}
