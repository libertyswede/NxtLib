using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib.MonetarySystem
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
            queryParameters.Add(Parameters.BuyRateNqt, BuyRate.Nqt.ToString());
            queryParameters.Add(Parameters.Currency, CurrencyId.ToString());
            queryParameters.Add(Parameters.ExpirationHeight, ExpirationHeight.ToString());
            queryParameters.Add(Parameters.InitialBuySupply, InitialBuySupply.ToString());
            queryParameters.Add(Parameters.InitialSellSupply, InitialSellSupply.ToString());
            queryParameters.Add(Parameters.SellRateNqt, SellRate.Nqt.ToString());
            queryParameters.Add(Parameters.TotalBuyLimit, TotalBuyLimit.ToString());
            queryParameters.Add(Parameters.TotalSellLimit, TotalSellLimit.ToString());

        }
    }
}