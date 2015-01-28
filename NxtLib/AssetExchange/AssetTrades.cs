using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetTrades : BaseReply
    {
        private bool _processed = false;
        public List<AssetTrade> Trades { get; set; }

        public override void PostProcess()
        {
            if (_processed)
            {
                return;
            }
            
            Trades.ForEach(t => t.Price = AssetAmount.CreateAmountFromNqt(t.PriceNqt, t.Decimals));

            _processed = true;
        }
    }
}