using System;
using Newtonsoft.Json.Linq;

namespace NxtLib
{
    public abstract class Appendix
    {
        protected const string AlgorithmKey = "algorithm";
        protected const string AliasKey = "alias";
        protected const string AmountNqtPerQntKey = "amountNQTPerQNT";
        protected const string AmountPerUnitNqtKey = "amountPerUnitNQT";
        protected const string AssetIdKey = "asset";
        protected const string BuyRateNqtKey = "buyRateNQT";
        protected const string CodeKey = "code";
        protected const string CommentKey = "comment";
        protected const string CounterKey = "counter";
        protected const string CurrencyKey = "currency";
        protected const string DataKey = "data";
        protected const string DecimalsKey = "decimals";
        protected const string DeliveryDeadlineTimestampKey = "deliveryDeadlineTimestamp";
        protected const string DeltaQuantityKey = "deltaQuantity";
        protected const string DescriptionKey = "description";
        protected const string DiscountKey = "discountNQT";
        protected const string EncryptedMessageKey = "encryptedMessage";
        protected const string EncryptToSelfMessageKey = "encryptToSelfMessage";
        protected const string ExpirationHeightKey = "expirationHeight";
        protected const string FinishHeightKey = "finishHeight";
        protected const string HoldingKey = "holding";
        protected const string GoodsIdKey = "goods";
        protected const string GoodsDataKey = "goodsData";
        protected const string GoodsIsTextKey = "goodsIsText";
        protected const string GoodsNonceKey = "goodsNonce";
        protected const string HeightKey = "height";
        protected const string InitialBuySupplyKey = "initialBuySupply";
        protected const string InitialSellSupplyKey = "initialSellSupply";
        protected const string InitialSupplyKey = "initialSupply";
        protected const string IssuanceHeightKey = "issuanceHeight";
        protected const string IsTextKey = "isText";
        protected const string MaxDifficultyKey = "maxDifficulty";
        protected const string MaxNumberOfOptionsKey = "maxNumberOfOptions";
        protected const string MaxRangeValueKey = "maxRangeValue";
        protected const string MaxSupplyKey = "maxSupply";
        protected const string MessageKey = "message";
        protected const string MessageIsTextKey = "messageIsText";
        protected const string MinBalanceKey = "minBalance";
        protected const string MinBalanceModelKey = "minBalanceModel";
        protected const string MinDifficultyKey = "minDifficulty";
        protected const string MinNumberOfOptionsKey = "minNumberOfOptions";
        protected const string MinRangeValueKey = "minRangeValue";
        protected const string MinReservePerUnitNqtKey = "minReservePerUnitNQT";
        protected const string NameKey = "name";
        protected const string NonceKey = "nonce";
        protected const string OptionsKey = "options";
        protected const string OrderIdKey = "order";
        protected const string PeriodKey = "period";
        protected const string PriceNqtKey = "priceNQT";
        protected const string PurchaseKey = "purchase";
        protected const string QuantityKey = "quantity";
        protected const string QuantityQntKey = "quantityQNT";
        protected const string RateNqtKey = "rateNQT";
        protected const string RecipientPublicKeyKey = "recipientPublicKey";
        protected const string RefundNqtKey = "refundNQT";
        protected const string ReserveSupplyKey = "reserveSupply";
        protected const string RulesetKey = "ruleset";
        protected const string SellRateNqtKey = "sellRateNQT";
        protected const string TagsKey = "tags";
        protected const string TotalBuyLimitKey = "totalBuyLimit";
        protected const string TotalSellLimitKey = "totalSellLimit";
        protected const string TypeKey = "type";
        protected const string UnitsKey = "units";
        protected const string UriKey = "uri";
        protected const string VotingModelKey = "votingModel";

        protected static T GetAttachmentValue<T>(JToken attachments, string key)
        {
            var obj = ((JValue)attachments.SelectToken(key)).Value;
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }

    public class Message : Appendix
    {
        public UnencryptedMessage UnencryptedMessage { get; set; }

        private Message(UnencryptedMessage unencryptedMessage)
        {
            UnencryptedMessage = unencryptedMessage;
        }

        internal static Message ParseJson(JObject jObject)
        {
            JValue messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(MessageKey) as JValue) == null)
            {
                return null;
            }

            var messageIsText = Convert.ToBoolean(jObject.SelectToken(MessageIsTextKey));
            return new Message(new UnencryptedMessage(messageToken.Value.ToString(), messageIsText));
        }
    }

    public abstract class EncryptedMessageBase : Appendix
    {
        public bool IsText { get; set; }
        public BinaryHexString Nonce { get; set; }
        public BinaryHexString Data { get; set; }

        protected EncryptedMessageBase(JToken messageToken)
        {
            IsText = Convert.ToBoolean(((JValue)messageToken.SelectToken(IsTextKey)).Value.ToString());
            Nonce = new BinaryHexString(((JValue)messageToken.SelectToken(NonceKey)).Value.ToString());
            Data = new BinaryHexString(((JValue)messageToken.SelectToken(DataKey)).Value.ToString());
        }
    }

    public class EncryptedMessage : EncryptedMessageBase
    {
        private EncryptedMessage(JToken messageToken)
            : base(messageToken)
        {
        }

        internal static EncryptedMessage ParseJson(JObject jObject)
        {
            JToken messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(EncryptedMessageKey)) == null)
            {
                return null;
            }
            return new EncryptedMessage(messageToken);
        }
    }

    public class EncryptToSelfMessage : EncryptedMessageBase
    {
        private EncryptToSelfMessage(JToken messageToken)
            : base(messageToken)
        {
        }

        internal static EncryptToSelfMessage ParseJson(JObject jObject)
        {
            JToken messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(EncryptToSelfMessageKey)) == null)
            {
                return null;
            }
            return new EncryptToSelfMessage(messageToken);
        }
    }

    public class PublicKeyAnnouncement : Appendix
    {
        public string RecipientPublicKey { get; set; }

        private PublicKeyAnnouncement(string recipientPublicKey)
        {
            RecipientPublicKey = recipientPublicKey;
        }

        internal static PublicKeyAnnouncement ParseJson(JObject jObject)
        {
            JValue announcement;
            if (jObject == null || (announcement = jObject.SelectToken(RecipientPublicKeyKey) as JValue) == null)
            {
                return null;
            }
            return new PublicKeyAnnouncement(announcement.Value.ToString());
        }
    }
}
