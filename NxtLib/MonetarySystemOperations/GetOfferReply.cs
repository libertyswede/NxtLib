namespace NxtLib.MonetarySystemOperations
{
    public class GetOfferReply : BaseReply
    {
        public CurrencyExchangeOffer BuyOffer { get; set; }
        public CurrencyExchangeOffer SellOffer { get; set; }
    }
}