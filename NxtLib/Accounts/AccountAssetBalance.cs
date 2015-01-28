﻿using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountAssetBalance
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Asset { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        public ulong BalanceQnt { get; set; }
    }
}