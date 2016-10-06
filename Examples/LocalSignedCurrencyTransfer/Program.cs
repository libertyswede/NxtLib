using NxtLib;
using NxtLib.Local;
using NxtLib.MonetarySystem;
using NxtLib.Transactions;
using System;

namespace LocalSignedCurrencyTransfer
{
    class Program
    {
        private const string SecretPhrase = "bloom harmony cousin swim morning then desire patience coat worst shy click";
        private const string SenderPublicKey = "790ecfa4b3591c893e300dfa5261eb6644d850ffabe28f3be96e938859333464";
        private const string Recipient = "NXT-HMVV-XMBN-GYXK-22BKK";
        private const ulong CurrencyId = 1294380573514520412UL;
        private const long Units = 10;

        public static void Main()
        {
            var monetarySystemService = new MonetarySystemService();
            var transactionService = new TransactionService();
            var localTransactionService = new LocalTransactionService();

            // Get the unsigned transaction bytes from the NRS server
            var parameters = new CreateTransactionByPublicKey(1440, Amount.Zero, SenderPublicKey);
            var unsigned = monetarySystemService.TransferCurrency(Recipient, CurrencyId, Units, parameters).Result;

            // Verify the unsigned transaction bytes from the node, will throw exception if bytes have been tampered with
            localTransactionService.VerifyTransferCurrencyTransactionBytes(unsigned, parameters, Recipient, CurrencyId, Units);

            // Sign the transaction locally and broadcast the signed bytes
            var signed = localTransactionService.SignTransaction(unsigned, SecretPhrase);
            var result = transactionService.BroadcastTransaction(new TransactionParameter(signed.ToString())).Result;
            var transactionId = result.TransactionId;
            Console.WriteLine($"Sent transaction: {transactionId}");

            Console.ReadLine();
        }
    }
}
