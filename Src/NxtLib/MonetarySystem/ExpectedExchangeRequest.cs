namespace NxtLib.MonetarySystem
{
    public class ExpectedExchangeRequest : ExchangeRequest
    {
        public int Height { get; set; }
        public bool Phased { get; set; }
    }
}