using Newtonsoft.Json;
using NxtLib.Internal;
using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    [JsonConverter(typeof(AccountCurrencyConverter))]
    public class GetAccountCurrenciesReply : BaseReply
    {
        public List<AccountCurrency> AccountCurrencies { get; set; }
    }
}