using System.Collections.Generic;

namespace NxtLib.MonetarySystemOperations
{
    public class PublishExchangeOfferParameters
    {
        public Amount BuyRate { get; set; }
        public ulong CurrencyId { get; set; }
        public int ExpirationHeight { get; set; }
        public long InitialBuySupply { get; set; }
        public long InitialSellSupply { get; set; }
        public Amount SellRate { get; set; }
        public long TotalBuyLimit { get; set; }
        public long TotalSellLimit { get; set; }

        internal void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            queryParameters.Add("buyRateNQT", BuyRate.Nqt.ToString());
            queryParameters.Add("currency", CurrencyId.ToString());
            queryParameters.Add("expirationHeight", ExpirationHeight.ToString());
            queryParameters.Add("initialBuySupply", InitialBuySupply.ToString());
            queryParameters.Add("initialSellSUpply", InitialSellSupply.ToString());
            queryParameters.Add("sellRateNQT", SellRate.Nqt.ToString());
            queryParameters.Add("totalBuyLimit", TotalBuyLimit.ToString());
            queryParameters.Add("totalSellLimit", TotalSellLimit.ToString());

        }
    }
}