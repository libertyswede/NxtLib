using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public class AccountAssetsReply : BaseReply
    {
        [JsonProperty(PropertyName = Parameters.AccountAssets)]
        [JsonConverter(typeof(AccountAssetConverter))]
        public List<AccountAsset> AccountAssetList { get; set; } = new List<AccountAsset>();
    }
}