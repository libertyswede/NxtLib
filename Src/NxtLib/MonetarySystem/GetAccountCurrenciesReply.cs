using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class GetAccountCurrenciesReply : AccountCurrency, IBaseReply
    {
        public List<AccountCurrency> AccountCurrencies { get; set; }
        public IEnumerable<KeyValuePair<string, string>> PostData { get; set; }
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}