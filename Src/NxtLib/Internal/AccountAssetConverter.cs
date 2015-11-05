using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.AssetExchange;

namespace NxtLib.Internal
{
    internal class AccountAssetConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
            {
                throw new ArgumentException("Invalid token type, must be StartObject");
            }
            if (objectType != typeof (AccountAssetsReply))
            {
                throw new ArgumentException("Can only convert to AccountAssetsReply");
            }
            var accountAssetsReply = new AccountAssetsReply();
            var jObject = JObject.Load(reader);
            
            var accountAssets = jObject.SelectToken(Parameters.AccountAssets) as JArray;
            if (accountAssets != null)
            {
                foreach (var accountAssetJson in accountAssets)
                {
                    var accountAsset = JsonConvert.DeserializeObject<AccountAsset>(accountAssetJson.ToString());
                    accountAssetsReply.AccountAssets.Add(accountAsset);
                }
            }
            else
            {
                var accountAsset = JsonConvert.DeserializeObject<AccountAsset>(jObject.ToString());
                accountAssetsReply.AccountAssets.Add(accountAsset);
            }
            
            return accountAssetsReply;
        }
		
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}