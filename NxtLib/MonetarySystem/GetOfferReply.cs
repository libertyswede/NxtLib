namespace NxtLib.MonetarySystem
{
    public class GetOfferReply : BaseReply
    {
        public CurrencyExchangeOffer BuyOffer { get; set; }
        public CurrencyExchangeOffer SellOffer { get; set; }
    }
}