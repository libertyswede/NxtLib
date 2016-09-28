using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.MonetarySystem;
using System;

namespace NxtLib.Internal
{
    internal class AccountCurrencyConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
            {
                throw new ArgumentException("Invalid token type, must be StartObject");
            }
            if (objectType != typeof(GetAccountCurrenciesReply))
            {
                throw new ArgumentException("Can only convert to GetAccountCurrenciesReply");
            }
            var accountCurrenciesReply = new GetAccountCurrenciesReply();
            var jObject = JObject.Load(reader);

            var accountCurrencies = jObject.SelectToken(Parameters.AccountCurrencies) as JArray;
            if (accountCurrencies != null)
            {
                foreach (var accountCurrencyJson in accountCurrencies)
                {
                    var accountCurrency = JsonConvert.DeserializeObject<AccountCurrency>(accountCurrencyJson.ToString());
                    accountCurrenciesReply.AccountCurrencies.Add(accountCurrency);
                }
            }
            else
            {
                var accountCurrency = JsonConvert.DeserializeObject<AccountCurrency>(jObject.ToString());
                accountCurrenciesReply.AccountCurrencies.Add(accountCurrency);
            }

            return accountCurrenciesReply;
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
