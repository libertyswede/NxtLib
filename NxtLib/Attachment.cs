using System;
using System.Collections.Generic;
using System.Linq;
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
    }

    public class AccountControlEffectiveBalanceLeasingAttachment : Attachment
    {
        public short Period { get; set; }

        public AccountControlEffectiveBalanceLeasingAttachment(IReadOnlyDictionary<string, object> values)
        {
            Period = Convert.ToInt16(values[PeriodKey]);
        }
    }

    public abstract class ColoredCoinsOrderCancellationAttachment : Attachment
    {
        public ulong OrderId { get; private set; }

        protected ColoredCoinsOrderCancellationAttachment(IReadOnlyDictionary<string, object> values)
        {
            OrderId = Convert.ToUInt64(values[OrderIdKey]);
        }
    }

    public class ColoredCoinsAskOrderCancellationAttachment : ColoredCoinsOrderCancellationAttachment
    {
        public ColoredCoinsAskOrderCancellationAttachment(IReadOnlyDictionary<string, object> values)
            : base(values)
        {
        }
    }

    public class ColoredCoinsAskOrderPlacementAttachment : ColoredCoinsOrderPlacementAttachment
    {
        public ColoredCoinsAskOrderPlacementAttachment(IReadOnlyDictionary<string, object> values)
            : base(values)
        {
        }
    }

    public class ColoredCoinsAssetIssuanceAttachment : Attachment
    {
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public long QuantityQnt { get; set; }

        public ColoredCoinsAssetIssuanceAttachment(IReadOnlyDictionary<string, object> values)
        {
            Decimals = Convert.ToByte(values[DecimalsKey]);
            Description = values[DescriptionKey].ToString();
            Name = values[NameKey].ToString();
            QuantityQnt = Convert.ToInt64(values[QuantityQntKey]);
        }
    }

    public class ColoredCoinsAssetTransferAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public string Comment { get; set; }

        public ColoredCoinsAssetTransferAttachment(IReadOnlyDictionary<string, object> values)
        {
            AssetId = Convert.ToUInt64(values[AssetIdKey]);
            QuantityQnt = Convert.ToInt64(values[QuantityQntKey]);
            if (values.ContainsKey(CommentKey))
            {
                Comment = values[CommentKey].ToString();
            }
        }
    }

    public class ColoredCoinsBidOrderCancellationAttachment : ColoredCoinsOrderCancellationAttachment
    {
        public ColoredCoinsBidOrderCancellationAttachment(IReadOnlyDictionary<string, object> values)
            : base(values)
        {
        }
    }

    public abstract class ColoredCoinsOrderPlacementAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public Amount Price { get; set; }


        protected ColoredCoinsOrderPlacementAttachment(IReadOnlyDictionary<string, object> values)
        {
            AssetId = Convert.ToUInt64(values[AssetIdKey]);
            QuantityQnt = Convert.ToInt64(values[QuantityQntKey]);
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values[PriceNqtKey]));
        }
    }

    public class ColoredCoinsBidOrderPlacementAttachment : ColoredCoinsOrderPlacementAttachment
    {
        public ColoredCoinsBidOrderPlacementAttachment(IReadOnlyDictionary<string, object> values)
            : base(values)
        {
        }
    }

    public class DigitalGoodsDelistingAttachment : Attachment
    {
        public ulong GoodsId { get; set; }

        public DigitalGoodsDelistingAttachment(IReadOnlyDictionary<string, object> values)
        {
            GoodsId = Convert.ToUInt64(values[GoodsIdKey]);
        }
    }

    public class DigitalGoodsDeliveryAttachment : Attachment
    {
        public Amount Discount { get; set; }
        public string GoodsData { get; set; }
        public bool GoodsIsText { get; set; }
        public string GoodsNonce { get; set; }
        public ulong Purchase { get; set; }

        public DigitalGoodsDeliveryAttachment(IReadOnlyDictionary<string, object> values)
        {
            Discount = Amount.CreateAmountFromNqt(Convert.ToInt64(values[DiscountKey]));
            GoodsData = values[GoodsDataKey].ToString();
            GoodsIsText = Convert.ToBoolean(values[GoodsIsTextKey]);
            GoodsNonce = values[GoodsNonceKey].ToString();
            Purchase = Convert.ToUInt64(values[PurchaseKey]);
        }
    }

    public class ColoredCoinsDividendPaymentAttachment : Attachment
    {
        public Amount AmountPerQnt { get; set; }
        public ulong AssetId { get; set; }
        public int Height { get; set; }

        public ColoredCoinsDividendPaymentAttachment(IReadOnlyDictionary<string, object> values)
        {
            AmountPerQnt = Amount.CreateAmountFromNqt(Convert.ToInt64(values[AmountNqtPerQntKey]));
            AssetId = Convert.ToUInt64(values[AssetIdKey]);
            Height = Convert.ToInt32(values[HeightKey]);
        }
    }

    public class DigitalGoodsFeedbackAttachment : Attachment
    {
        public ulong PurchaseId { get; set; }

        public DigitalGoodsFeedbackAttachment(IReadOnlyDictionary<string, object> values)
        {
            PurchaseId = Convert.ToUInt64(values[PurchaseKey]);
        }
    }

    public class DigitalGoodsListingAttachment : Attachment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int Quantity { get; set; }
        public Amount Price { get; set; }
        
        public DigitalGoodsListingAttachment(IReadOnlyDictionary<string, object> values)
        {
            Name = values[NameKey].ToString();
            Description = values[DescriptionKey].ToString();
            Tags = values[TagsKey].ToString();
            Quantity = Convert.ToInt32(values[QuantityKey]);
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values[PriceNqtKey]));
        }
    }

    public class DigitalGoodsPriceChangeAttachment : Attachment
    {
        public ulong GoodsId { get; set; }
        public Amount Price { get; set; }

        public DigitalGoodsPriceChangeAttachment(IReadOnlyDictionary<string, object> values)
        {
            GoodsId = Convert.ToUInt64(values[GoodsIdKey]);
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values[PriceNqtKey]));
        }
    }

    public class DigitalGoodsPurchaseAttachment : Attachment
    {
        public int DeliveryDeadlineTimestamp { get; set; }
        public ulong GoodsId { get; set; }
        public Amount Price { get; set; }
        public int Quantity { get; set; }

        public DigitalGoodsPurchaseAttachment(IReadOnlyDictionary<string, object> values)
        {
            DeliveryDeadlineTimestamp = Convert.ToInt32(values[DeliveryDeadlineTimestampKey]);
            GoodsId = Convert.ToUInt64(values[GoodsIdKey]);
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values[PriceNqtKey]));
            Quantity = Convert.ToInt32(values[QuantityKey]);
        }
    }

    public class DigitalGoodsQuantityChangeAttachment : Attachment
    {
        public int DeltaQuantity { get; set; }
        public ulong GoodsId { get; set; }

        public DigitalGoodsQuantityChangeAttachment(IReadOnlyDictionary<string, object> values)
        {
            DeltaQuantity = Convert.ToInt32(values[DeltaQuantityKey]);
            GoodsId = Convert.ToUInt64(values[GoodsIdKey]);
        }
    }

    public class DigitalGoodsRefundAttachment : Attachment
    {
        public ulong PurchaseId { get; set; }
        public Amount Refund { get; set; }

        public DigitalGoodsRefundAttachment(IReadOnlyDictionary<string, object> values)
        {
            PurchaseId = Convert.ToUInt64(values[PurchaseKey]);
            Refund = Amount.CreateAmountFromNqt(Convert.ToInt64(values[RefundNqtKey]));
        }
    }

    public class MessagingAccountInfoAttachment : Attachment
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public MessagingAccountInfoAttachment(IReadOnlyDictionary<string, object> values)
        {
            Name = values[NameKey].ToString();
            Description = values[DescriptionKey].ToString();
        }
    }

    public class MessagingAliasAssignmentAttachment : Attachment
    {
        public string Alias { get; set; }
        public string Uri { get; set; }

        public MessagingAliasAssignmentAttachment(IReadOnlyDictionary<string, object> values)
        {
            Alias = values[AliasKey].ToString();
            Uri = values[UriKey].ToString();
        }
    }

    public class MessagingAliasBuyAttachment : Attachment
    {
        public string Alias { get; set; }

        public MessagingAliasBuyAttachment(IReadOnlyDictionary<string, object> values)
        {
            Alias = values[AliasKey].ToString();
        }
    }

    public class MessagingAliasDeleteAttachment : Attachment
    {
        public string Alias { get; set; }

        public MessagingAliasDeleteAttachment(IReadOnlyDictionary<string, object> values)
        {
            Alias = values[AliasKey].ToString();
        }
    }

    public class MessagingAliasSellAttachment : Attachment
    {
        public string Alias { get; set; }
        public Amount Price { get; set; }

        public MessagingAliasSellAttachment(IReadOnlyDictionary<string, object> values)
        {
            Alias = values[AliasKey].ToString();
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values[PriceNqtKey]));
        }
    }

    public abstract class MonetarySystemExchange : Attachment
    {
        public ulong CurrencyId { get; set; }
        public Amount Rate { get; set; }
        public long Units { get; set; }

        protected MonetarySystemExchange(IReadOnlyDictionary<string, object> values)
        {
            CurrencyId = Convert.ToUInt64(values[CurrencyKey]);
            Rate = Amount.CreateAmountFromNqt(Convert.ToInt64(values[RateNqtKey]));
            Units = Convert.ToInt64(values[UnitsKey]);
        }
    }

    public class MonetarySystemExchangeBuyAttachment : MonetarySystemExchange
    {
        public MonetarySystemExchangeBuyAttachment(IReadOnlyDictionary<string, object> values)
            : base(values)
        {
        }
    }

    public class MonetarySystemExchangeSellAttachment : MonetarySystemExchange
    {
        public MonetarySystemExchangeSellAttachment(IReadOnlyDictionary<string, object> values)
            : base(values)
        {
        }
    }

    public class MonetarySystemCurrencyDeletion : Attachment
    {
        public ulong CurrencyId { get; set; }

        public MonetarySystemCurrencyDeletion(IReadOnlyDictionary<string, object> values)
        {
            CurrencyId = Convert.ToUInt64(values[CurrencyKey]);
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

        public MonetarySystemCurrencyIssuanceAttachment(IReadOnlyDictionary<string, object> values)
        {
            Algorithm = Convert.ToByte(values[AlgorithmKey]);
            Code = values[CodeKey].ToString();
            Decimals = Convert.ToByte(values[DecimalsKey]);
            Description = values[DescriptionKey].ToString();
            InitialSupply = Convert.ToInt64(values[InitialSupplyKey]);
            IssuanceHeight = Convert.ToInt32(values[IssuanceHeightKey]);
            MaxDifficulty = Convert.ToInt32(values[MaxDifficultyKey]);
            MaxSupply = Convert.ToInt64(values[MaxSupplyKey]);
            MinDifficulty = Convert.ToInt32(values[MinDifficultyKey]);
            MinReservePerUnit = Amount.CreateAmountFromNqt(Convert.ToInt64(values[MinReservePerUnitNqtKey]));
            Name = values[NameKey].ToString();
            ReserveSupply = Convert.ToInt64(values[ReserveSupplyKey]);
            Ruleset = Convert.ToByte(values[RulesetKey]);
            SetTypes(Convert.ToInt32(values[TypeKey]));
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

        public MonetarySystemCurrencyMintingAttachment(IReadOnlyDictionary<string, object> values)
        {
            Counter = Convert.ToInt64(values[CounterKey]);
            CurrencyId = Convert.ToUInt64(values[CurrencyKey]);
            Nonce = Convert.ToInt64(values[NonceKey]);
            Units = Convert.ToInt64(values[UnitsKey]);
        }
    }

    public class MonetarySystemCurrencyTransferAttachment : Attachment
    {
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        public MonetarySystemCurrencyTransferAttachment(IReadOnlyDictionary<string, object> values)
        {
            CurrencyId = Convert.ToUInt64(values[CurrencyKey]);
            Units = Convert.ToInt64(values[UnitsKey]);
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

        public MonetarySystemPublishExchangeOfferAttachment(IReadOnlyDictionary<string, object> values)
        {
            BuyRate = Amount.CreateAmountFromNqt(Convert.ToInt64(values[BuyRateNqtKey]));
            CurrencyId = Convert.ToUInt64(values[CurrencyKey]);
            ExpirationHeight = Convert.ToInt32(values[ExpirationHeightKey]);
            InitialBuySupply = Convert.ToInt64(values[InitialBuySupplyKey]);
            InitialSellSupply = Convert.ToInt64(values[InitialSellSupplyKey]);
            SellRate = Amount.CreateAmountFromNqt(Convert.ToInt64(values[SellRateNqtKey]));
            TotalBuyLimit = Convert.ToInt64(values[TotalBuyLimitKey]);
            TotalSellLimit = Convert.ToInt64(values[TotalSellLimitKey]);
        }
    }

    public class MonetarySystemReserveClaimAttachment : Attachment
    {
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        public MonetarySystemReserveClaimAttachment(IReadOnlyDictionary<string, object> values)
        {
            CurrencyId = Convert.ToUInt64(values[CurrencyKey]);
            Units = Convert.ToInt64(values[UnitsKey]);
        }
    }

    public class MonetarySystemReserveIncrease : Attachment
    {
        public Amount AmountPerUnit { get; set; }
        public ulong CurrencyId { get; set; }

        public MonetarySystemReserveIncrease(IReadOnlyDictionary<string, object> values) 
        {
            AmountPerUnit = Amount.CreateAmountFromNqt(Convert.ToInt64(values[AmountPerUnitNqtKey]));
            CurrencyId = Convert.ToUInt64(values[CurrencyKey]);
        }
    }
}
