namespace NxtLib.Transactions
{
    public class TransactionParameters
    {
        public string TransactionBytes { get; private set; }
        public string TransactionJson { get; private set; }

        private TransactionParameters()
        {
        }

        public static TransactionParameters CreateByBytes(string bytes)
        {
            return new TransactionParameters {TransactionBytes = bytes};
        }

        public static TransactionParameters CreateByJson(string json)
        {
            return new TransactionParameters {TransactionJson = json};
        }
    }
}