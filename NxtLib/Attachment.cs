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

    public class AccountInfoAttachment : Attachment
    {
        public string Name { get; set; }
        public string Description { get; set; }

        internal const string AttachmentName = "version.AccountInfo";

        public AccountInfoAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Name = values["name"].ToString();
            Description = values["description"].ToString();
        }
    }

    public class AliasAssignmentAttachment : Attachment
    {
        public string Alias { get; set; }
        public string Uri { get; set; }

        internal const string AttachmentName = "version.AliasAssignment";

        public AliasAssignmentAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Alias = values["alias"].ToString();
            Uri = values["uri"].ToString();
        }
    }

    public class AliasBuyAttachment : Attachment
    {
        public string Alias { get; set; }

        internal const string AttachmentName = "version.AliasBuy";

        public AliasBuyAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Alias = values["alias"].ToString();
        }
    }

    public class AliasDeleteAttachment : Attachment
    {
        public string Alias { get; set; }

        internal const string AttachmentName = "version.AliasDelete";

        public AliasDeleteAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Alias = values["alias"].ToString();
        }
    }

    public class AliasSellAttachment : Attachment
    {
        public string Alias { get; set; }
        public Amount Price { get; set; }

        internal const string AttachmentName = "version.AliasSell";

        public AliasSellAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Alias = values["alias"].ToString();
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values["priceNQT"]));
        }
    }

    public abstract class OrderPlacementAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public Amount Price { get; set; }

        protected OrderPlacementAttachment(IReadOnlyDictionary<string, object> values, string attachmentName)
            : base(values, attachmentName)
        {
            AssetId = Convert.ToUInt64(values["asset"]);
            QuantityQnt = Convert.ToInt64(values["quantityQNT"]);
            Price = Amount.CreateAmountFromNqt(Convert.ToInt64(values["priceNQT"]));
        }
    }

    public class AskOrderPlacementAttachment : OrderPlacementAttachment
    {
        internal const string AttachmentName = "version.AskOrderPlacement";

        public AskOrderPlacementAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
        }
    }

    public abstract class OrderCancellationAttachment : Attachment
    {
        public ulong OrderId { get; private set; }

        protected OrderCancellationAttachment(IReadOnlyDictionary<string, object> values, string attachmentName)
            : base(values, attachmentName)
        {
            OrderId = Convert.ToUInt64(values["order"]);
        }
    }

    public class AskOrderCancellationAttachment : OrderCancellationAttachment
    {
        internal const string AttachmentName = "version.AskOrderCancellation";

        public AskOrderCancellationAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
        }
    }

    public class AssetIssuanceAttachment : Attachment
    {
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public long QuantityQnt { get; set; }

        internal const string AttachmentName = "version.AssetIssuance";

        public AssetIssuanceAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Decimals = Convert.ToByte(values["decimals"]);
            Description = values["description"].ToString();
            Name = values["name"].ToString();
            QuantityQnt = Convert.ToInt64(values["quantityQNT"]);
        }
    }

    public class AssetTransferAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public string Comment { get; set; }

        internal const string AttachmentName = "version.AssetTransfer";

        public AssetTransferAttachment(IReadOnlyDictionary<string, object> values)
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

    public class BidOrderCancellationAttachment : OrderCancellationAttachment
    {
        internal const string AttachmentName = "version.BidOrderCancellation";

        public BidOrderCancellationAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
        }
    }

    public class BidOrderPlacementAttachment : OrderPlacementAttachment
    {
        internal const string AttachmentName = "version.BidOrderPlacement";

        public BidOrderPlacementAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
        }
    }

    public class CurrencyIssuanceAttachment : Attachment
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
        public CurrencyIssuanceAttachment(IReadOnlyDictionary<string, object> values)
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
                if ((type & (int) currencyType) != 0)
                {
                    Types.Add(currencyType);
                }
            }
        }
    }

    public class CurrencyMintingAttachment : Attachment
    {
        public long Counter { get; set; }
        public ulong CurrencyId { get; set; }
        public long Nonce { get; set; }
        public long Units { get; set; }

        internal const string AttachmentName = "version.CurrencyMinting";

        public CurrencyMintingAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Counter = Convert.ToInt64(values["counter"]);
            CurrencyId = Convert.ToUInt64(values["currency"]);
            Nonce = Convert.ToInt64(values["nonce"]);
            Units = Convert.ToInt64(values["units"]);
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

    public class EffectiveBalanceLeasingAttachment : Attachment
    {
        public short Period { get; set; }

        internal const string AttachmentName = "version.EffectiveBalanceLeasing";

        public EffectiveBalanceLeasingAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
        {
            Period = Convert.ToInt16(values["period"]);
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

    public class ExchangeBuyAttachment : MonetarySystemExchange
    {
        internal const string AttachmentName = "version.ExchangeBuy";

        public ExchangeBuyAttachment(IReadOnlyDictionary<string, object> values) 
            : base(values, AttachmentName)
        {
        }
    }

    public class ExchangeSellAttachment : MonetarySystemExchange
    {
        internal const string AttachmentName = "version.ExchangeSell";

        public ExchangeSellAttachment(IReadOnlyDictionary<string, object> values)
            : base(values, AttachmentName)
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

    public class PublishExchangeOfferAttachment : Attachment
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

        public PublishExchangeOfferAttachment(IReadOnlyDictionary<string, object> values) 
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
}
