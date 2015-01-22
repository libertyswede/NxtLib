using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.MonetarySystemOperations;

namespace NxtLib
{
    public abstract class Attachment
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
        protected const string MaxSupplyKey = "maxSupply";
        protected const string MessageKey = "message";
        protected const string MessageIsTextKey = "messageIsText";
        protected const string MinDifficultyKey = "minDifficulty";
        protected const string MinReservePerUnitNqtKey = "minReservePerUnitNQT";
        protected const string NameKey = "name";
        protected const string NonceKey = "nonce";
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

        protected static T GetAttachmentValue<T>(JToken attachments, string key) where T : IConvertible
        {
            var obj = ((JValue) attachments.SelectToken(key)).Value;
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }

    public class AccountControlEffectiveBalanceLeasingAttachment : Attachment
    {
        public short Period { get; set; }

        internal AccountControlEffectiveBalanceLeasingAttachment(JToken attachments)
        {
            Period = GetAttachmentValue<short>(attachments, PeriodKey);
        }
    }

    public abstract class ColoredCoinsOrderCancellationAttachment : Attachment
    {
        public ulong OrderId { get; private set; }

        protected ColoredCoinsOrderCancellationAttachment(JToken attachments)
        {
            OrderId = GetAttachmentValue<ulong>(attachments, OrderIdKey);
        }
    }

    public class ColoredCoinsAskOrderCancellationAttachment : ColoredCoinsOrderCancellationAttachment
    {
        internal ColoredCoinsAskOrderCancellationAttachment(JToken attachments)
            : base(attachments)
        {
        }
    }

    public class ColoredCoinsAskOrderPlacementAttachment : ColoredCoinsOrderPlacementAttachment
    {
        internal ColoredCoinsAskOrderPlacementAttachment(JToken attachments)
            : base(attachments)
        {
        }
    }

    public class ColoredCoinsAssetIssuanceAttachment : Attachment
    {
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public long QuantityQnt { get; set; }

        internal ColoredCoinsAssetIssuanceAttachment(JToken attachments)
        {
            Decimals = GetAttachmentValue<byte>(attachments, DecimalsKey);
            Description = GetAttachmentValue<string>(attachments, DescriptionKey);
            Name = GetAttachmentValue<string>(attachments, NameKey);
            QuantityQnt = GetAttachmentValue<long>(attachments, QuantityQntKey);
        }
    }

    public class ColoredCoinsAssetTransferAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public string Comment { get; set; }

        internal ColoredCoinsAssetTransferAttachment(JToken attachments)
        {
            AssetId = GetAttachmentValue<ulong>(attachments, AssetIdKey);
            QuantityQnt = GetAttachmentValue<long>(attachments, QuantityQntKey);
            if (attachments.SelectToken(CommentKey) != null)
            {
                Comment = GetAttachmentValue<string>(attachments, CommentKey);
            }
        }
    }

    public class ColoredCoinsBidOrderCancellationAttachment : ColoredCoinsOrderCancellationAttachment
    {
        internal ColoredCoinsBidOrderCancellationAttachment(JToken attachments)
            : base(attachments)
        {
        }
    }

    public abstract class ColoredCoinsOrderPlacementAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public Amount Price { get; set; }


        protected ColoredCoinsOrderPlacementAttachment(JToken attachments)
        {
            AssetId = GetAttachmentValue<ulong>(attachments, AssetIdKey);
            QuantityQnt = GetAttachmentValue<long>(attachments, QuantityQntKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
        }
    }

    public class ColoredCoinsBidOrderPlacementAttachment : ColoredCoinsOrderPlacementAttachment
    {
        internal ColoredCoinsBidOrderPlacementAttachment(JToken attachments)
            : base(attachments)
        {
        }
    }

    public class DigitalGoodsDelistingAttachment : Attachment
    {
        public ulong GoodsId { get; set; }

        internal DigitalGoodsDelistingAttachment(JToken attachments)
        {
            GoodsId = GetAttachmentValue<ulong>(attachments, GoodsIdKey);
        }
    }

    public class DigitalGoodsDeliveryAttachment : Attachment
    {
        public Amount Discount { get; set; }
        public string GoodsData { get; set; }
        public bool GoodsIsText { get; set; }
        public IEnumerable<byte> GoodsNonce { get; set; }
        public ulong Purchase { get; set; }

        internal DigitalGoodsDeliveryAttachment(JToken attachments)
        {
            Discount = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, DiscountKey));
            GoodsData = GetAttachmentValue<string>(attachments, GoodsDataKey);
            GoodsIsText = GetAttachmentValue<bool>(attachments, GoodsIsTextKey);
            var goodsNonceString = GetAttachmentValue<string>(attachments, GoodsNonceKey);
            GoodsNonce = ByteToHexStringConverter.ToBytes(goodsNonceString);
            Purchase = GetAttachmentValue<ulong>(attachments, PurchaseKey);
        }
    }

    public class ColoredCoinsDividendPaymentAttachment : Attachment
    {
        public Amount AmountPerQnt { get; set; }
        public ulong AssetId { get; set; }
        public int Height { get; set; }

        internal ColoredCoinsDividendPaymentAttachment(JToken attachments)
        {
            AmountPerQnt = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, AmountNqtPerQntKey));
            AssetId = GetAttachmentValue<ulong>(attachments, AssetIdKey);
            Height = GetAttachmentValue<int>(attachments, HeightKey);
        }
    }

    public class DigitalGoodsFeedbackAttachment : Attachment
    {
        public ulong PurchaseId { get; set; }

        internal DigitalGoodsFeedbackAttachment(JToken attachments)
        {
            PurchaseId = GetAttachmentValue<ulong>(attachments, PurchaseKey);
        }
    }

    public class DigitalGoodsListingAttachment : Attachment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int Quantity { get; set; }
        public Amount Price { get; set; }

        internal DigitalGoodsListingAttachment(JToken attachments)
        {
            Name = GetAttachmentValue<string>(attachments, NameKey);
            Description = GetAttachmentValue<string>(attachments, DescriptionKey);
            Tags = GetAttachmentValue<string>(attachments, TagsKey);
            Quantity = GetAttachmentValue<int>(attachments, QuantityKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
        }
    }

    public class DigitalGoodsPriceChangeAttachment : Attachment
    {
        public ulong GoodsId { get; set; }
        public Amount Price { get; set; }

        internal DigitalGoodsPriceChangeAttachment(JToken attachments)
        {
            GoodsId = GetAttachmentValue<ulong>(attachments, GoodsIdKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
        }
    }

    public class DigitalGoodsPurchaseAttachment : Attachment
    {
        public DateTime DeliveryDeadlineTimestamp { get; set; }
        public ulong GoodsId { get; set; }
        public Amount Price { get; set; }
        public int Quantity { get; set; }

        internal DigitalGoodsPurchaseAttachment(JToken attachments)
        {
            DeliveryDeadlineTimestamp =
                DateTimeConverter.GetDateTime(GetAttachmentValue<int>(attachments, DeliveryDeadlineTimestampKey));
            GoodsId = GetAttachmentValue<ulong>(attachments, GoodsIdKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
            Quantity = GetAttachmentValue<int>(attachments, QuantityKey);
        }
    }

    public class DigitalGoodsQuantityChangeAttachment : Attachment
    {
        public int DeltaQuantity { get; set; }
        public ulong GoodsId { get; set; }

        internal DigitalGoodsQuantityChangeAttachment(JToken attachments)
        {
            DeltaQuantity = GetAttachmentValue<int>(attachments, DeltaQuantityKey);
            GoodsId = GetAttachmentValue<ulong>(attachments, GoodsIdKey);
        }
    }

    public class DigitalGoodsRefundAttachment : Attachment
    {
        public ulong PurchaseId { get; set; }
        public Amount Refund { get; set; }

        internal DigitalGoodsRefundAttachment(JToken attachments)
        {
            PurchaseId = GetAttachmentValue<ulong>(attachments, PurchaseKey);
            Refund = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, RefundNqtKey));
        }
    }

    public class MessagingAccountInfoAttachment : Attachment
    {
        public string Name { get; set; }
        public string Description { get; set; }

        internal MessagingAccountInfoAttachment(JToken attachments)
        {
            Name = GetAttachmentValue<string>(attachments, NameKey);
            Description = GetAttachmentValue<string>(attachments, DescriptionKey);
        }
    }

    public class MessagingAliasAssignmentAttachment : Attachment
    {
        public string Alias { get; set; }
        public string Uri { get; set; }

        internal MessagingAliasAssignmentAttachment(JToken attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, AliasKey);
            Uri = GetAttachmentValue<string>(attachments, UriKey);
        }
    }

    public class MessagingAliasBuyAttachment : Attachment
    {
        public string Alias { get; set; }

        internal MessagingAliasBuyAttachment(JToken attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, AliasKey);
        }
    }

    public class MessagingAliasDeleteAttachment : Attachment
    {
        public string Alias { get; set; }

        internal MessagingAliasDeleteAttachment(JToken attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, AliasKey);
        }
    }

    public class MessagingAliasSellAttachment : Attachment
    {
        public string Alias { get; set; }
        public Amount Price { get; set; }

        internal MessagingAliasSellAttachment(JToken attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, AliasKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
        }
    }

    public abstract class MonetarySystemExchange : Attachment
    {
        public ulong CurrencyId { get; set; }
        public Amount Rate { get; set; }
        public long Units { get; set; }

        protected MonetarySystemExchange(JToken attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
            Rate = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, RateNqtKey));
            Units = GetAttachmentValue<long>(attachments, UnitsKey);
        }
    }

    public class MonetarySystemExchangeBuyAttachment : MonetarySystemExchange
    {
        internal MonetarySystemExchangeBuyAttachment(JToken attachments)
            : base(attachments)
        {
        }
    }

    public class MonetarySystemExchangeSellAttachment : MonetarySystemExchange
    {
        internal MonetarySystemExchangeSellAttachment(JToken attachments)
            : base(attachments)
        {
        }
    }

    public class MonetarySystemCurrencyDeletion : Attachment
    {
        public ulong CurrencyId { get; set; }

        internal MonetarySystemCurrencyDeletion(JToken attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
        }
    }

    public class MonetarySystemCurrencyIssuanceAttachment : Attachment
    {
        public byte Algorithm { get; set; }
        public string Code { get; set; }
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public long InitialSupply { get; set; }
        public int IssuanceHeight { get; set; }
        public int MaxDifficulty { get; set; }
        public long MaxSupply { get; set; }
        public int MinDifficulty { get; set; }
        public Amount MinReservePerUnit { get; set; }
        public string Name { get; set; }
        public long ReserveSupply { get; set; }
        public byte Ruleset { get; set; }
        public HashSet<CurrencyType> Types { get; set; }

        internal MonetarySystemCurrencyIssuanceAttachment(JToken attachments)
        {
            Algorithm = GetAttachmentValue<byte>(attachments, AlgorithmKey);
            Code = GetAttachmentValue<string>(attachments, CodeKey);
            Decimals = GetAttachmentValue<byte>(attachments, DecimalsKey);
            Description = GetAttachmentValue<string>(attachments, DescriptionKey);
            InitialSupply = GetAttachmentValue<long>(attachments, InitialSupplyKey);
            IssuanceHeight = GetAttachmentValue<int>(attachments, IssuanceHeightKey);
            MaxDifficulty = GetAttachmentValue<int>(attachments, MaxDifficultyKey);
            MaxSupply = GetAttachmentValue<long>(attachments, MaxSupplyKey);
            MinDifficulty = GetAttachmentValue<int>(attachments, MinDifficultyKey);
            MinReservePerUnit = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, MinReservePerUnitNqtKey));
            Name = GetAttachmentValue<string>(attachments, NameKey);
            ReserveSupply = GetAttachmentValue<long>(attachments, ReserveSupplyKey);
            Ruleset = GetAttachmentValue<byte>(attachments, RulesetKey);
            SetTypes(GetAttachmentValue<int>(attachments, TypeKey));
        }

        private void SetTypes(int type)
        {
            Types = new HashSet<CurrencyType>();

            foreach (var currencyType in Enum.GetValues(typeof(CurrencyType)).Cast<CurrencyType>())
            {
                if ((type & (int)currencyType) != 0)
                {
                    Types.Add(currencyType);
                }
            }
        }
    }

    public class MonetarySystemCurrencyMintingAttachment : Attachment
    {
        public long Counter { get; set; }
        public ulong CurrencyId { get; set; }
        public long Nonce { get; set; }
        public long Units { get; set; }

        internal MonetarySystemCurrencyMintingAttachment(JToken attachments)
        {
            Counter = GetAttachmentValue<long>(attachments, CounterKey);
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
            Nonce = GetAttachmentValue<long>(attachments, NonceKey);
            Units = GetAttachmentValue<long>(attachments, UnitsKey);
        }
    }

    public class MonetarySystemCurrencyTransferAttachment : Attachment
    {
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        internal MonetarySystemCurrencyTransferAttachment(JToken attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
            Units = GetAttachmentValue<long>(attachments, UnitsKey);
        }
    }

    public class MonetarySystemPublishExchangeOfferAttachment : Attachment
    {
        public Amount BuyRate { get; set; }
        public ulong CurrencyId { get; set; }
        public int ExpirationHeight { get; set; }
        public long InitialBuySupply { get; set; }
        public long InitialSellSupply { get; set; }
        public Amount SellRate { get; set; }
        public long TotalBuyLimit { get; set; }
        public long TotalSellLimit { get; set; }

        internal MonetarySystemPublishExchangeOfferAttachment(JToken attachments)
        {
            BuyRate = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, BuyRateNqtKey));
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
            ExpirationHeight = GetAttachmentValue<int>(attachments, ExpirationHeightKey);
            InitialBuySupply = GetAttachmentValue<long>(attachments, InitialBuySupplyKey);
            InitialSellSupply = GetAttachmentValue<long>(attachments, InitialSellSupplyKey);
            SellRate = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, SellRateNqtKey));
            TotalBuyLimit = GetAttachmentValue<long>(attachments, TotalBuyLimitKey);
            TotalSellLimit = GetAttachmentValue<long>(attachments, TotalSellLimitKey);
        }
    }

    public class MonetarySystemReserveClaimAttachment : Attachment
    {
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        internal MonetarySystemReserveClaimAttachment(JToken attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
            Units = GetAttachmentValue<long>(attachments, UnitsKey);
        }
    }

    public class MonetarySystemReserveIncrease : Attachment
    {
        public Amount AmountPerUnit { get; set; }
        public ulong CurrencyId { get; set; }

        internal MonetarySystemReserveIncrease(JToken attachments) 
        {
            AmountPerUnit = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, AmountPerUnitNqtKey));
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
        }
    }
}
