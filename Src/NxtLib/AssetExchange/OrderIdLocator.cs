namespace NxtLib.AssetExchange
{
    public class OrderIdLocator : LocatorBase
    {
        public ulong? AskOrderId { get; private set; }
        public ulong? BidOrderId { get; private set; }

        private OrderIdLocator(string key, string value) : base(key, value)
        {
        }

        public static OrderIdLocator ByAskOrderId(ulong askOrderId)
        {
            return new OrderIdLocator("askOrder", askOrderId.ToString()) {AskOrderId = askOrderId};
        }

        public static OrderIdLocator ByBidOrderId(ulong bidOrderId)
        {
            return new OrderIdLocator("bidOrder", bidOrderId.ToString()) { BidOrderId = bidOrderId };
        }
    }
}