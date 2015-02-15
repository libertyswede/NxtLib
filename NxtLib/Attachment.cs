using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;

namespace NxtLib
{
    public abstract class Attachment : Appendix
    {
        protected Attachment(JToken jToken) : base(jToken)
        {
        }
    }

    public class AccountControlEffectiveBalanceLeasingAttachment : Attachment
    {
        protected override string AppendixName { get { return "EffectiveBalanceLeasing"; } }
        public short Period { get; set; }

        internal AccountControlEffectiveBalanceLeasingAttachment(JToken attachments)
            : base(attachments)
        {
            Period = GetAttachmentValue<short>(attachments, PeriodKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class ColoredCoinsOrderCancellationAttachment : Attachment
    {
        public ulong OrderId { get; private set; }

        protected ColoredCoinsOrderCancellationAttachment(JToken attachments)
            : base(attachments)
        {
            OrderId = GetAttachmentValue<ulong>(attachments, OrderIdKey);
        }
    }

    public class ColoredCoinsAskOrderCancellationAttachment : ColoredCoinsOrderCancellationAttachment
    {
        protected override string AppendixName { get { return "AskOrderCancellation"; } }

        internal ColoredCoinsAskOrderCancellationAttachment(JToken attachments)
            : base(attachments)
        {
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class ColoredCoinsAskOrderPlacementAttachment : ColoredCoinsOrderPlacementAttachment
    {
        protected override string AppendixName { get { return "AskOrderPlacement"; } }

        internal ColoredCoinsAskOrderPlacementAttachment(JToken attachments)
            : base(attachments)
        {
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class ColoredCoinsAssetIssuanceAttachment : Attachment
    {
        protected override string AppendixName { get { return "AssetIssuance"; } }

        public byte Decimals { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public long QuantityQnt { get; set; }

        internal ColoredCoinsAssetIssuanceAttachment(JToken attachments)
            : base(attachments)
        {
            Decimals = GetAttachmentValue<byte>(attachments, DecimalsKey);
            Description = GetAttachmentValue<string>(attachments, DescriptionKey);
            Name = GetAttachmentValue<string>(attachments, NameKey);
            QuantityQnt = GetAttachmentValue<long>(attachments, QuantityQntKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class ColoredCoinsAssetTransferAttachment : Attachment
    {
        protected override string AppendixName { get { return "AssetTransfer"; } }

        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public string Comment { get; set; }

        internal ColoredCoinsAssetTransferAttachment(JToken attachments)
            : base(attachments)
        {
            AssetId = GetAttachmentValue<ulong>(attachments, AssetIdKey);
            QuantityQnt = GetAttachmentValue<long>(attachments, QuantityQntKey);
            if (attachments.SelectToken(CommentKey) != null)
            {
                Comment = GetAttachmentValue<string>(attachments, CommentKey);
            }
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class ColoredCoinsBidOrderCancellationAttachment : ColoredCoinsOrderCancellationAttachment
    {
        protected override string AppendixName { get { return "BidOrderCancellation"; } }

        internal ColoredCoinsBidOrderCancellationAttachment(JToken attachments)
            : base(attachments)
        {
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class ColoredCoinsOrderPlacementAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public Amount Price { get; set; }


        protected ColoredCoinsOrderPlacementAttachment(JToken attachments)
            : base(attachments)
        {
            AssetId = GetAttachmentValue<ulong>(attachments, AssetIdKey);
            QuantityQnt = GetAttachmentValue<long>(attachments, QuantityQntKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
        }
    }

    public class ColoredCoinsBidOrderPlacementAttachment : ColoredCoinsOrderPlacementAttachment
    {
        protected override string AppendixName { get { return "BidOrderPlacement"; } }

        internal ColoredCoinsBidOrderPlacementAttachment(JToken attachments)
            : base(attachments)
        {
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class DigitalGoodsDelistingAttachment : Attachment
    {
        protected override string AppendixName { get { return "DigitalGoodsDelisting"; } }
        public ulong GoodsId { get; set; }

        internal DigitalGoodsDelistingAttachment(JToken attachments)
            : base(attachments)
        {
            GoodsId = GetAttachmentValue<ulong>(attachments, GoodsIdKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class DigitalGoodsDeliveryAttachment : Attachment
    {
        protected override string AppendixName { get { return "DigitalGoodsDelivery"; } }
        public Amount Discount { get; set; }
        public BinaryHexString GoodsData { get; set; }
        public bool GoodsIsText { get; set; }
        public BinaryHexString GoodsNonce { get; set; }
        public ulong Purchase { get; set; }

        internal DigitalGoodsDeliveryAttachment(JToken attachments)
            : base(attachments)
        {
            Discount = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, DiscountKey));
            GoodsData = new BinaryHexString(GetAttachmentValue<string>(attachments, GoodsDataKey));
            GoodsIsText = GetAttachmentValue<bool>(attachments, GoodsIsTextKey);
            GoodsNonce = new BinaryHexString(GetAttachmentValue<string>(attachments, GoodsNonceKey));
            Purchase = GetAttachmentValue<ulong>(attachments, PurchaseKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class ColoredCoinsDividendPaymentAttachment : Attachment
    {
        protected override string AppendixName { get { return "DividendPayment"; } }
        public Amount AmountPerQnt { get; set; }
        public ulong AssetId { get; set; }
        public int Height { get; set; }

        internal ColoredCoinsDividendPaymentAttachment(JToken attachments)
            : base(attachments)
        {
            AmountPerQnt = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, AmountNqtPerQntKey));
            AssetId = GetAttachmentValue<ulong>(attachments, AssetIdKey);
            Height = GetAttachmentValue<int>(attachments, HeightKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class DigitalGoodsFeedbackAttachment : Attachment
    {
        protected override string AppendixName { get { return "DigitalGoodsFeedback"; } }
        public ulong PurchaseId { get; set; }

        internal DigitalGoodsFeedbackAttachment(JToken attachments)
            : base(attachments)
        {
            PurchaseId = GetAttachmentValue<ulong>(attachments, PurchaseKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class DigitalGoodsListingAttachment : Attachment
    {
        protected override string AppendixName { get { return "DigitalGoodsListing"; } }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int Quantity { get; set; }
        public Amount Price { get; set; }

        internal DigitalGoodsListingAttachment(JToken attachments)
            : base(attachments)
        {
            Name = GetAttachmentValue<string>(attachments, NameKey);
            Description = GetAttachmentValue<string>(attachments, DescriptionKey);
            Tags = GetAttachmentValue<string>(attachments, TagsKey);
            Quantity = GetAttachmentValue<int>(attachments, QuantityKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class DigitalGoodsPriceChangeAttachment : Attachment
    {
        protected override string AppendixName { get { return "DigitalGoodsPriceChange"; } }
        public ulong GoodsId { get; set; }
        public Amount Price { get; set; }

        internal DigitalGoodsPriceChangeAttachment(JToken attachments)
            : base(attachments)
        {
            GoodsId = GetAttachmentValue<ulong>(attachments, GoodsIdKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class DigitalGoodsPurchaseAttachment : Attachment
    {
        protected override string AppendixName { get { return "DigitalGoodsPurchase"; } }
        public DateTime DeliveryDeadlineTimestamp { get; set; }
        public ulong GoodsId { get; set; }
        public Amount Price { get; set; }
        public int Quantity { get; set; }

        internal DigitalGoodsPurchaseAttachment(JToken attachments)
            : base(attachments)
        {
            DeliveryDeadlineTimestamp =
                DateTimeConverter.GetDateTime(GetAttachmentValue<int>(attachments, DeliveryDeadlineTimestampKey));
            GoodsId = GetAttachmentValue<ulong>(attachments, GoodsIdKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
            Quantity = GetAttachmentValue<int>(attachments, QuantityKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class DigitalGoodsQuantityChangeAttachment : Attachment
    {
        protected override string AppendixName { get { return "DigitalGoodsQuantityChange"; } }
        public int DeltaQuantity { get; set; }
        public ulong GoodsId { get; set; }

        internal DigitalGoodsQuantityChangeAttachment(JToken attachments)
            : base(attachments)
        {
            DeltaQuantity = GetAttachmentValue<int>(attachments, DeltaQuantityKey);
            GoodsId = GetAttachmentValue<ulong>(attachments, GoodsIdKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class DigitalGoodsRefundAttachment : Attachment
    {
        protected override string AppendixName { get { return "DigitalGoodsRefund"; } }
        public ulong PurchaseId { get; set; }
        public Amount Refund { get; set; }

        internal DigitalGoodsRefundAttachment(JToken attachments)
            : base(attachments)
        {
            PurchaseId = GetAttachmentValue<ulong>(attachments, PurchaseKey);
            Refund = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, RefundNqtKey));
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MessagingAccountInfoAttachment : Attachment
    {
        protected override string AppendixName { get { return "AccountInfo"; } }
        public string Name { get; set; }
        public string Description { get; set; }

        internal MessagingAccountInfoAttachment(JToken attachments)
            : base(attachments)
        {
            Name = GetAttachmentValue<string>(attachments, NameKey);
            Description = GetAttachmentValue<string>(attachments, DescriptionKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MessagingAliasAssignmentAttachment : Attachment
    {
        protected override string AppendixName { get { return "AliasAssignment"; } }
        public string Alias { get; set; }
        public string Uri { get; set; }

        internal MessagingAliasAssignmentAttachment(JToken attachments)
            : base(attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, AliasKey);
            Uri = GetAttachmentValue<string>(attachments, UriKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MessagingAliasBuyAttachment : Attachment
    {
        protected override string AppendixName { get { return "AliasBuy"; } }
        public string Alias { get; set; }

        internal MessagingAliasBuyAttachment(JToken attachments)
            : base(attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, AliasKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MessagingAliasDeleteAttachment : Attachment
    {
        protected override string AppendixName { get { return "AliasDelete"; } }
        public string Alias { get; set; }

        internal MessagingAliasDeleteAttachment(JToken attachments)
            : base(attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, AliasKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MessagingAliasSellAttachment : Attachment
    {
        protected override string AppendixName { get { return "AliasSell"; } }
        public string Alias { get; set; }
        public Amount Price { get; set; }

        internal MessagingAliasSellAttachment(JToken attachments)
            : base(attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, AliasKey);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, PriceNqtKey));
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class MonetarySystemExchange : Attachment
    {
        public ulong CurrencyId { get; set; }
        public Amount Rate { get; set; }
        public long Units { get; set; }

        protected MonetarySystemExchange(JToken attachments)
            : base(attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
            Rate = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, RateNqtKey));
            Units = GetAttachmentValue<long>(attachments, UnitsKey);
        }
    }

    public class MonetarySystemExchangeBuyAttachment : MonetarySystemExchange
    {
        protected override string AppendixName { get { return "ExchangeBuy"; } }

        internal MonetarySystemExchangeBuyAttachment(JToken attachments)
            : base(attachments)
        {
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MonetarySystemExchangeSellAttachment : MonetarySystemExchange
    {
        protected override string AppendixName { get { return "ExchangeSell"; } }

        internal MonetarySystemExchangeSellAttachment(JToken attachments)
            : base(attachments)
        {
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MonetarySystemCurrencyDeletion : Attachment
    {
        protected override string AppendixName { get { return "CurrencyDeletion"; } }
        public ulong CurrencyId { get; set; }

        internal MonetarySystemCurrencyDeletion(JToken attachments)
            : base(attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MonetarySystemCurrencyIssuanceAttachment : Attachment
    {
        protected override string AppendixName { get { return "CurrencyIssuance"; } }
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
            : base(attachments)
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

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MonetarySystemCurrencyMintingAttachment : Attachment
    {
        protected override string AppendixName { get { return "CurrencyMinting"; } }
        public long Counter { get; set; }
        public ulong CurrencyId { get; set; }
        public long Nonce { get; set; }
        public long Units { get; set; }

        internal MonetarySystemCurrencyMintingAttachment(JToken attachments)
            : base(attachments)
        {
            Counter = GetAttachmentValue<long>(attachments, CounterKey);
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
            Nonce = GetAttachmentValue<long>(attachments, NonceKey);
            Units = GetAttachmentValue<long>(attachments, UnitsKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MonetarySystemCurrencyTransferAttachment : Attachment
    {
        protected override string AppendixName { get { return "CurrencyTransfer"; } }
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        internal MonetarySystemCurrencyTransferAttachment(JToken attachments)
            : base(attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
            Units = GetAttachmentValue<long>(attachments, UnitsKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MonetarySystemPublishExchangeOfferAttachment : Attachment
    {
        protected override string AppendixName { get { return "PublishExchangeOffer"; } }
        public Amount BuyRate { get; set; }
        public ulong CurrencyId { get; set; }
        public int ExpirationHeight { get; set; }
        public long InitialBuySupply { get; set; }
        public long InitialSellSupply { get; set; }
        public Amount SellRate { get; set; }
        public long TotalBuyLimit { get; set; }
        public long TotalSellLimit { get; set; }

        internal MonetarySystemPublishExchangeOfferAttachment(JToken attachments)
            : base(attachments)
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

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MonetarySystemReserveClaimAttachment : Attachment
    {
        protected override string AppendixName { get { return "ReserveClaim"; } }
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        internal MonetarySystemReserveClaimAttachment(JToken attachments)
            : base(attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
            Units = GetAttachmentValue<long>(attachments, UnitsKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class MonetarySystemReserveIncrease : Attachment
    {
        protected override string AppendixName { get { return "ReserveIncrease"; } }
        public Amount AmountPerUnit { get; set; }
        public ulong CurrencyId { get; set; }

        internal MonetarySystemReserveIncrease(JToken attachments)
            : base(attachments)
        {
            AmountPerUnit = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, AmountPerUnitNqtKey));
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
        }

        protected override void PutMyBytes(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
