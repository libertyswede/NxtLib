using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    [JsonConverter(typeof(AccountAssetConverter))]
    public class AccountAssetsReply : BaseReply
    {
        public List<AccountAsset> AccountAssets { get; set; } = new List<AccountAsset>();
    }
}