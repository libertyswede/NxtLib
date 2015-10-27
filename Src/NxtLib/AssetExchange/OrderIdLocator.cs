using NxtLib.Internal;

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
            return new OrderIdLocator(Parameters.AskOrder, askOrderId.ToString()) {AskOrderId = askOrderId};
        }

        public static OrderIdLocator ByBidOrderId(ulong bidOrderId)
        {
            return new OrderIdLocator(Parameters.BidOrder, bidOrderId.ToString()) { BidOrderId = bidOrderId };
        }
    }
}