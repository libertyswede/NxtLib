using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.AssetExchange
{
    public class AccountAssetsReply : BaseReply
    {
        [JsonProperty(PropertyName = "accountAssets")]
        public List<AccountAsset> AccountAssetList { get; set; }
    }
}