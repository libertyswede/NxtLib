using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.VotingSystem;

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
        protected const string ChannelKey = "channel";
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
        protected const string FilenameKey = "filename";
        protected const string FinishHeightKey = "finishHeight";
        protected const string HashKey = "hash";
        protected const string HoldingKey = "holding";
        protected const string GoodsIdKey = "goods";
        protected const string GoodsDataKey = "goodsData";
        protected const string GoodsIsTextKey = "goodsIsText";
        protected const string GoodsNonceKey = "goodsNonce";
        protected const string HeightKey = "height";
        protected const string InitialBuySupplyKey = "initialBuySupply";
        protected const string InitialSellSupplyKey = "initialSellSupply";
        protected const string InitialSupplyKey = "initialSupply";
        protected const string IsCompressedKey = "isCompressed";
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
        protected const string PhasingFinishHeightKey = "phasingFinishHeight";
        protected const string PhasingHashedSecretKey = "phasingHashedSecret";
        protected const string PhasingHashedSecretAlgorithmKey = "phasingHashedSecretAlgorithm";
        protected const string PhasingHoldingKey = "phasingHolding";
        protected const string PhasingLinkedFullHashesKey = "phasingLinkedFullHashes";
        protected const string PhasingMinBalanceKey = "phasingMinBalance";
        protected const string PhasingMinBalanceModelKey = "phasingMinBalanceModel";
        protected const string PhasingQuorumKey = "phasingQuorum";
        protected const string PhasingVotingModelKey = "phasingVotingModel";
        protected const string PhasingWhitelistKey = "phasingWhitelist";
        protected const string PollKey = "poll";
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
        protected const string TaggedDataKey = "taggedData";
        protected const string TagsKey = "tags";
        protected const string TotalBuyLimitKey = "totalBuyLimit";
        protected const string TotalSellLimitKey = "totalSellLimit";
        protected const string TransactionFullHashesKey = "transactionFullHashes";
        protected const string TypeKey = "type";
        protected const string UnitsKey = "units";
        protected const string UriKey = "uri";
        protected const string VoteKey = "vote";
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

            var messageIsText = Convert.ToBoolean(jObject.SelectToken(MessageIsTextKey).ToString());
            return new Message(new UnencryptedMessage(messageToken.Value.ToString(), messageIsText));
        }
    }

    public abstract class EncryptedMessageBase : Appendix
    {
        public bool IsCompressed { get; set; }
        public bool IsText { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Nonce { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Data { get; set; }

        protected EncryptedMessageBase(JToken messageToken)
        {
            IsCompressed = Convert.ToBoolean(((JValue) messageToken.SelectToken(IsCompressedKey)).Value.ToString());
            IsText = Convert.ToBoolean(((JValue) messageToken.SelectToken(IsTextKey)).Value.ToString());
            Nonce = new BinaryHexString(((JValue) messageToken.SelectToken(NonceKey)).Value.ToString());
            Data = new BinaryHexString(((JValue) messageToken.SelectToken(DataKey)).Value.ToString());
        }

        protected EncryptedMessageBase()
        {
        }
    }

    public class EncryptedMessage : EncryptedMessageBase
    {
        private EncryptedMessage(JToken messageToken)
            : base(messageToken)
        {
        }

        public EncryptedMessage()
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
        public BinaryHexString RecipientPublicKey { get; set; }

        private PublicKeyAnnouncement(BinaryHexString recipientPublicKey)
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

    public class TransactionPhasing : Appendix
    {
        public int FinishHeight { get; set; }
        public BinaryHexString HashedSecret { get; set; }
        public HashAlgorithm? HashedSecretAlgorithm { get; set; }
        public ulong HoldingId { get; set; }
        public List<BinaryHexString> LinkedFullHashes { get; set; }
        public long MinBalance { get; set; }
        public MinBalanceModel MinBalanceModel { get; set; }
        public long Quorum { get; set; }
        public VotingModel VotingModel { get; set; }
        public List<ulong> WhiteList { get; set; }

        private TransactionPhasing()
        {
            WhiteList = new List<ulong>();
            LinkedFullHashes = new List<BinaryHexString>();
        }

        internal static TransactionPhasing ParseJson(JObject jObject)
        {
            if (jObject?.SelectToken(PhasingFinishHeightKey) == null)
            {
                return null;
            }

            var phasing = new TransactionPhasing
            {
                FinishHeight = GetAttachmentValue<int>(jObject, PhasingFinishHeightKey),
                HoldingId = ulong.Parse(GetAttachmentValue<string>(jObject, PhasingHoldingKey)),
                MinBalance = long.Parse(GetAttachmentValue<string>(jObject, PhasingMinBalanceKey)),
                MinBalanceModel = (MinBalanceModel)GetAttachmentValue<int>(jObject, PhasingMinBalanceModelKey),
                Quorum = GetAttachmentValue<long>(jObject, PhasingQuorumKey),
                VotingModel = (VotingModel)GetAttachmentValue<int>(jObject, PhasingVotingModelKey)
            };

            if (jObject.SelectToken(PhasingWhitelistKey) != null)
            {
                phasing.WhiteList = ParseWhitelist(jObject.SelectToken(PhasingWhitelistKey)).Select(UInt64.Parse).ToList();
            }
            if (jObject.SelectToken(PhasingHashedSecretKey) != null)
            {
                phasing.HashedSecret = new BinaryHexString(GetAttachmentValue<string>(jObject, PhasingHashedSecretKey));
                phasing.HashedSecretAlgorithm = (HashAlgorithm)GetAttachmentValue<int>(jObject, PhasingHashedSecretAlgorithmKey);
            }
            if (jObject.SelectToken(PhasingLinkedFullHashesKey) != null)
            {
                phasing.LinkedFullHashes =
                    ParseWhitelist(jObject.SelectToken(PhasingLinkedFullHashesKey))
                        .Select(s => new BinaryHexString(s))
                        .ToList();
            }

            return phasing;
        }


        private static IEnumerable<string> ParseWhitelist(JToken whitelistToken)
        {
            return whitelistToken.Children<JValue>().Select(optionToken => optionToken.Value.ToString());
        }
    }
}
