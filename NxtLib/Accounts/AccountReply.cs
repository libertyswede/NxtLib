using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AccountOperations
{
    public class AccountReply : BaseReply
    {
        public List<AccountCurrency> AccountCurrencies { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }
        public List<AccountAssetBalance> AssetBalances { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "balanceNQT")]
        public Amount Balance { get; set; }
        public int CurrentLeasingHeightFrom { get; set; }
        public int CurrentLeasingHeightTo { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "currentLessee")]
        public ulong CurrentLesseeId { get; set; }
        public string CurrentLesseeRs { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(NxtAmountConverter))]
        [JsonProperty(PropertyName = "effectiveBalanceNxt")]
        public Amount EffectiveBalance { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "forgedBalanceNqt")]
        public Amount ForgedBalance { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "guaranteedBalanceNqt")]
        public Amount GuaranteedBalance { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public List<ulong> Lessors { get; set; }

        public List<LessorInfo> LessorsInfo { get; set; }
        public List<string> LessorsRs { get; set; }
        public int MessagePatternFlags { get; set; }
        public string MessagePatternRegex { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "nextLessee")]
        public ulong NextLesseeId { get; set; }
        public string NextLesseeRs { get; set; }
        public int NextLeasingHeightFrom { get; set; }
        public int NextLeasingHeightTo { get; set; }
        public string PublicKey { get; set; }
        public List<UnconfirmedAccountAssetBalance> UnconfirmedAssetBalances { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "unconfirmedBalanceNqt")]
        public Amount UnconfirmedBalance { get; set; }
    }
}