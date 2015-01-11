using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NxtLib.MonetarySystemOperations;

namespace NxtLib
{
    public abstract class Attachment
    {
        protected virtual long Version { get { return 1; } }

        protected Attachment(IReadOnlyDictionary<string, object> values, string name)
        {
            VerifyAttachmentVersion(values, name);
        }

        protected void VerifyAttachmentVersion(IReadOnlyDictionary<string, object> values, string name)
        {
            Debug.Assert((long)values[name] == Version);
        }
    }

    public class AccountControlEffectiveBalanceLeasingAttachment : Attachment
    {
        public short Period { get; set; }

        internal const string AttachmentName = "version.EffectiveBalanceLeasing";

        public AccountControlEffectiveBalanceLeasingAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Period = Convert.ToInt16(values["period"]);
        }
    }

    public abstract class ColoredCoinsOrderCancellationAttachment : Attachment
    {
        public ulong OrderId { get; private set; }

        protected ColoredCoinsOrderCancellationAttachment(IReadOnlyDictionary<string, object> values, string attachmentName)
            : base(values, attachmentName)
        {
            OrderId = Convert.ToUInt64(values["order"]);
        }
    }

    public class ColoredCoinsAskOrderCancellationAttachment : ColoredCoinsOrderCancellationAttachment
    {
        internal const string AttachmentName = "version.AskOrderCancellation";

        public ColoredCoinsAskOrderCancellationAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
        }
    }

    public class ColoredCoinsAskOrderPlacementAttachment : ColoredCoinsOrderPlacementAttachment
    {
        internal const string AttachmentName = "version.AskOrderPlacement";

        public ColoredCoinsAskOrderPlacementAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
        }
    }

    public class ColoredCoinsAssetIssuanceAttachment : Attachment
    {
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public long QuantityQnt { get; set; }

        internal const string AttachmentName = "version.AssetIssuance";

        public ColoredCoinsAssetIssuanceAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Decimals = Convert.ToByte(values["decimals"]);
            Description = values["description"].ToString();
            Name = values["name"].ToString();
            QuantityQnt = Convert.ToInt64(values["quantityQNT"]);
        }
    }

    public class ColoredCoinsAssetTransferAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public string Comment { get; set; }

        internal const string AttachmentName = "version.AssetTransfer";

        public ColoredCoinsAssetTransferAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            AssetId = Convert.ToUInt64(values["asset"]);
            QuantityQnt = Convert.ToInt64(values["quantityQNT"]);
            if (values.ContainsKey("comment"))
            {
                Comment = values["comment"].ToString();
            }
        }
    }

    public class ColoredCoinsBidOrderCancellationAttachment : ColoredCoinsOrderCancellationAttachment
    {
        internal const string AttachmentName = "version.BidOrderCancellation";

        public ColoredCoinsBidOrderCancellationAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
        }
    }

    public abstract class ColoredCoinsOrderPlacementAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public Amount Price { get; set; }

        protected ColoredCoinsOrderPlacementAttachment(IReadOnlyDictionary<string, object> values, string attachmentName)
            : base(values, attachmentName)
        {
            AssetId = Convert.ToUInt64(values["asset"]);
            QuantityQnt = Convert.ToInt64(values["quantityQNT"]);
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values["priceNQT"]));
        }
    }

    public class ColoredCoinsBidOrderPlacementAttachment : ColoredCoinsOrderPlacementAttachment
    {
        internal const string AttachmentName = "version.BidOrderPlacement";

        public ColoredCoinsBidOrderPlacementAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
        }
    }

    public class DigitalGoodsDelistingAttachment : Attachment
    {
        public ulong GoodsId { get; set; }

        internal const string AttachmentName = "version.DigitalGoodsDelisting";

        public DigitalGoodsDelistingAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            GoodsId = Convert.ToUInt64(values["goods"]);
        }
    }

    public class DigitalGoodsDeliveryAttachment : Attachment
    {
        public Amount Discount { get; set; }
        public string GoodsData { get; set; }
        public bool GoodsIsText { get; set; }
        public string GoodsNonce { get; set; }
        public ulong Purchase { get; set; }

        internal const string AttachmentName = "version.DigitalGoodsDelivery";

        public DigitalGoodsDeliveryAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Discount = Amount.CreateAmountFromNqt(Convert.ToInt64(values["discountNQT"]));
            GoodsData = values["goodsData"].ToString();
            GoodsIsText = Convert.ToBoolean(values["goodsIsText"]);
            GoodsNonce = values["goodsNonce"].ToString();
            Purchase = Convert.ToUInt64(values["purchase"]);
        }
    }

    public class ColoredCoinsDividendPaymentAttachment : Attachment
    {
        public Amount AmountPerQnt { get; set; }
        public ulong AssetId { get; set; }
        public int Height { get; set; }

        internal const string AttachmentName = "version.DividendPayment";

        public ColoredCoinsDividendPaymentAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            AmountPerQnt = Amount.CreateAmountFromNqt(Convert.ToInt64(values["amountNQTPerQNT"]));
            AssetId = Convert.ToUInt64(values["asset"]);
            Height = Convert.ToInt32(values["height"]);
        }
    }

    public class DigitalGoodsFeedbackAttachment : Attachment
    {
        public ulong PurchaseId { get; set; }

        internal const string AttachmentName = "version.DigitalGoodsFeedback";

        public DigitalGoodsFeedbackAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            PurchaseId = Convert.ToUInt64(values["purchase"]);
        }
    }

    public class DigitalGoodsListingAttachment : Attachment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int Quantity { get; set; }
        public Amount Price { get; set; }

        internal const string AttachmentName = "version.DigitalGoodsListing";

        public DigitalGoodsListingAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Name = values["name"].ToString();
            Description = values["description"].ToString();
            Tags = values["tags"].ToString();
            Quantity = Convert.ToInt32(values["quantity"]);
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values["priceNQT"]));
        }
    }

    public class DigitalGoodsPriceChangeAttachment : Attachment
    {
        public ulong GoodsId { get; set; }
        public Amount Price { get; set; }

        internal const string AttachmentName = "version.DigitalGoodsPriceChange";

        public DigitalGoodsPriceChangeAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            GoodsId = Convert.ToUInt64(values["goods"]);
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values["priceNQT"]));
        }
    }

    public class DigitalGoodsPurchaseAttachment : Attachment
    {
        public int DeliveryDeadlineTimestamp { get; set; }
        public ulong GoodsId { get; set; }
        public Amount Price { get; set; }
        public int Quantity { get; set; }

        internal const string AttachmentName = "version.DigitalGoodsPurchase";

        public DigitalGoodsPurchaseAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            DeliveryDeadlineTimestamp = Convert.ToInt32(values["deliveryDeadlineTimestamp"]);
            GoodsId = Convert.ToUInt64(values["goods"]);
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values["priceNQT"]));
            Quantity = Convert.ToInt32(values["quantity"]);
        }
    }

    public class DigitalGoodsQuantityChangeAttachment : Attachment
    {
        public int DeltaQuantity { get; set; }
        public ulong GoodsId { get; set; }

        internal const string AttachmentName = "version.DigitalGoodsQuantityChange";

        public DigitalGoodsQuantityChangeAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        
        {
            DeltaQuantity = Convert.ToInt32(values["deltaQuantity"]);
            GoodsId = Convert.ToUInt64(values["goods"]);
        }
    }

    public class DigitalGoodsRefundAttachment : Attachment
    {
        public ulong PurchaseId { get; set; }
        public Amount Refund { get; set; }

        internal const string AttachmentName = "version.DigitalGoodsRefund";

        public DigitalGoodsRefundAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            PurchaseId = Convert.ToUInt64(values["purchase"]);
            Refund = Amount.CreateAmountFromNqt(Convert.ToInt64(values["refundNQT"]));
        }
    }

    public abstract class EncryptedMessageBase : Attachment
    {
        public bool IsText { get; set; }
        public string Nonce { get; set; }
        public string Data { get; set; }

        protected EncryptedMessageBase(IReadOnlyDictionary<string, object> values, string encryptedMessageKey, string name)
            : base(values, name)
        {
            var encryptedMessageValues = (IReadOnlyDictionary<string, object>) values[encryptedMessageKey];

            IsText = (bool)encryptedMessageValues["isText"];
            Nonce = encryptedMessageValues["nonce"].ToString();
            Data = encryptedMessageValues["data"].ToString();
        }
    }

    public class EncryptedMessageAttachment : EncryptedMessageBase
    {
        internal const string AttachmentName = "version.EncryptedMessage";

        public EncryptedMessageAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, "encryptedMessage", AttachmentName)
        {
        }
    }

    public class EncryptToSelfMessageAttachment : EncryptedMessageBase
    {
        internal const string AttachmentName = "version.EncryptToSelfMessage";

        public EncryptToSelfMessageAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, "encryptToSelfMessage", AttachmentName)
        {
        }
    }

    public class MessageAttachment : Attachment
    {
        public bool MessageIsText { get; set; }
        public string Message { get; set; }

        internal const string AttachmentName = "version.Message";

        public MessageAttachment(IReadOnlyDictionary<string, object> values)
            :base(values, AttachmentName)
        {
            MessageIsText = Convert.ToBoolean(values["messageIsText"]);
            Message = values["message"].ToString();
        }
    }

    public class MessagingAccountInfoAttachment : Attachment
    {
        public string Name { get; set; }
        public string Description { get; set; }

        internal const string AttachmentName = "version.AccountInfo";

        public MessagingAccountInfoAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Name = values["name"].ToString();
            Description = values["description"].ToString();
        }
    }

    public class MessagingAliasAssignmentAttachment : Attachment
    {
        public string Alias { get; set; }
        public string Uri { get; set; }

        internal const string AttachmentName = "version.AliasAssignment";

        public MessagingAliasAssignmentAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Alias = values["alias"].ToString();
            Uri = values["uri"].ToString();
        }
    }

    public class MessagingAliasBuyAttachment : Attachment
    {
        public string Alias { get; set; }

        internal const string AttachmentName = "version.AliasBuy";

        public MessagingAliasBuyAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Alias = values["alias"].ToString();
        }
    }

    public class MessagingAliasDeleteAttachment : Attachment
    {
        public string Alias { get; set; }

        internal const string AttachmentName = "version.AliasDelete";

        public MessagingAliasDeleteAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Alias = values["alias"].ToString();
        }
    }

    public class MessagingAliasSellAttachment : Attachment
    {
        public string Alias { get; set; }
        public Amount Price { get; set; }

        internal const string AttachmentName = "version.AliasSell";

        public MessagingAliasSellAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Alias = values["alias"].ToString();
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values["priceNQT"]));
        }
    }

    public abstract class MonetarySystemExchange : Attachment
    {
        public ulong CurrencyId { get; set; }
        public Amount Rate { get; set; }
        public long Units { get; set; }

        protected MonetarySystemExchange(IReadOnlyDictionary<string, object> values, string name)
            : base(values, name)
        {
            CurrencyId = Convert.ToUInt64(values["currency"]);
            Rate = Amount.CreateAmountFromNqt(Convert.ToInt64(values["rateNQT"]));
            Units = Convert.ToInt64(values["units"]);
        }
    }

    public class MonetarySystemExchangeBuyAttachment : MonetarySystemExchange
    {
        internal const string AttachmentName = "version.ExchangeBuy";

        public MonetarySystemExchangeBuyAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
        }
    }

    public class MonetarySystemExchangeSellAttachment : MonetarySystemExchange
    {
        internal const string AttachmentName = "version.ExchangeSell";

        public MonetarySystemExchangeSellAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
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

        internal const string AttachmentName = "version.CurrencyIssuance";
        public MonetarySystemCurrencyIssuanceAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Algorithm = Convert.ToByte(values["algorithm"]);
            Code = values["code"].ToString();
            Decimals = Convert.ToByte(values["decimals"]);
            Description = values["description"].ToString();
            InitialSupply = Convert.ToInt64(values["initialSupply"]);
            IssuanceHeight = Convert.ToInt32(values["issuanceHeight"]);
            MaxDifficulty = Convert.ToInt32(values["maxDifficulty"]);
            MaxSupply = Convert.ToInt64(values["maxSupply"]);
            MinDifficulty = Convert.ToInt32(values["minDifficulty"]);
            MinReservePerUnit = Amount.CreateAmountFromNqt(Convert.ToInt64(values["minReservePerUnitNQT"]));
            Name = values["name"].ToString();
            ReserveSupply = Convert.ToInt64(values["reserveSupply"]);
            Ruleset = Convert.ToByte(values["ruleset"]);
            SetTypes(Convert.ToInt32(values["type"]));
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

        internal const string AttachmentName = "version.CurrencyMinting";

        public MonetarySystemCurrencyMintingAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Counter = Convert.ToInt64(values["counter"]);
            CurrencyId = Convert.ToUInt64(values["currency"]);
            Nonce = Convert.ToInt64(values["nonce"]);
            Units = Convert.ToInt64(values["units"]);
        }
    }

    public class MonetarySystemCurrencyTransferAttachment : Attachment
    {
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        internal const string AttachmentName = "version.CurrencyTransfer";

        public MonetarySystemCurrencyTransferAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            CurrencyId = Convert.ToUInt64(values["currency"]);
            Units = Convert.ToInt64(values["units"]);
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

        internal const string AttachmentName = "version.PublishExchangeOffer";

        public MonetarySystemPublishExchangeOfferAttachment(IReadOnlyDictionary<string, object> values) 
            :base(values, AttachmentName)
        {
            BuyRate = Amount.CreateAmountFromNqt(Convert.ToInt64(values["buyRateNQT"]));
            CurrencyId = Convert.ToUInt64(values["currency"]);
            ExpirationHeight = Convert.ToInt32(values["expirationHeight"]);
            InitialBuySupply = Convert.ToInt64(values["initialBuySupply"]);
            InitialSellSupply = Convert.ToInt64(values["initialSellSupply"]);
            SellRate = Amount.CreateAmountFromNqt(Convert.ToInt64(values["sellRateNQT"]));
            TotalBuyLimit = Convert.ToInt64(values["totalBuyLimit"]);
            TotalSellLimit = Convert.ToInt64(values["totalSellLimit"]);
        }
    }

    public class MonetarySystemReserveClaimAttachment : Attachment
    {
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        internal const string AttachmentName = "version.ReserveClaim";

        public MonetarySystemReserveClaimAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            CurrencyId = Convert.ToUInt64(values["currency"]);
            Units = Convert.ToInt64(values["units"]);
        }
    }

    public class MonetarySystemReserveIncrease : Attachment
    {
        public Amount AmountPerUnit { get; set; }
        public ulong CurrencyId { get; set; }

        internal const string AttachmentName = "version.ReserveIncrease";

        public MonetarySystemReserveIncrease(IReadOnlyDictionary<string, object> values) 
            : base(values, AttachmentName)
        {
            AmountPerUnit = Amount.CreateAmountFromNqt(Convert.ToInt64(values["amountPerUnitNQT"]));
            CurrencyId = Convert.ToUInt64(values["currency"]);
        }
    }

    public class PublicKeyAnnouncementAttachment : Attachment
    {
        public string RecipientPublicKey { get; set; }

        internal const string AttachmentName = "version.PublicKeyAnnouncement";

        public PublicKeyAnnouncementAttachment(IReadOnlyDictionary<string, object> values)
            :base(values, AttachmentName)
        {
            RecipientPublicKey = values["recipientPublicKey"].ToString();
        }
    }
}
