using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.VotingSystem;

namespace NxtLib
{
    public abstract class Attachment : Appendix
    {
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
        public BinaryHexString GoodsData { get; set; }
        public bool GoodsIsText { get; set; }
        public BinaryHexString GoodsNonce { get; set; }
        public ulong Purchase { get; set; }

        internal DigitalGoodsDeliveryAttachment(JToken attachments)
        {
            Discount = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, DiscountKey));
            GoodsData = new BinaryHexString(GetAttachmentValue<string>(attachments, GoodsDataKey));
            GoodsIsText = GetAttachmentValue<bool>(attachments, GoodsIsTextKey);
            GoodsNonce = new BinaryHexString(GetAttachmentValue<string>(attachments, GoodsNonceKey));
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

    public class MessagingPollCreationAttachment : Attachment
    {
        public string Description { get; set; }
        public int FinishHeight { get; set; }
        public ulong HoldingId { get; set; }
        public int MaxNumberOfOptions { get; set; }
        public int MaxRangeValue { get; set; }
        public long MinBalance { get; set; }
        public MinBalanceModel MinBalanceModel { get; set; }
        public int MinNumberOfOptions { get; set; }
        public int MinRangeValue { get; set; }
        public string Name { get; set; }
        public List<string> Options { get; set; }
        public VotingModel VotingModel { get; set; }

        internal MessagingPollCreationAttachment(JToken attachments)
        {
            Description = GetAttachmentValue<string>(attachments, DescriptionKey);
            FinishHeight = GetAttachmentValue<int>(attachments, FinishHeightKey);
            HoldingId = UInt64.Parse(GetAttachmentValue<string>(attachments, HoldingKey));
            MaxNumberOfOptions = GetAttachmentValue<int>(attachments, MaxNumberOfOptionsKey);
            MaxRangeValue = GetAttachmentValue<int>(attachments, MaxRangeValueKey);
            MinBalance = GetAttachmentValue<long>(attachments, MinBalanceKey);
            MinBalanceModel = (MinBalanceModel) GetAttachmentValue<int>(attachments, MinBalanceModelKey);
            MinRangeValue = GetAttachmentValue<int>(attachments, MinRangeValueKey);
            MinNumberOfOptions = GetAttachmentValue<int>(attachments, MinNumberOfOptionsKey);
            Name = GetAttachmentValue<string>(attachments, NameKey);
            Options = ParseOptions(attachments.SelectToken(OptionsKey)).ToList();
            VotingModel = (VotingModel)GetAttachmentValue<int>(attachments, VotingModelKey);
        }

        private static IEnumerable<string> ParseOptions(JToken optionsToken)
        {
            return optionsToken.Children<JValue>().Select(optionToken => optionToken.Value.ToString());
        }
    }

    public class MessagingPhasingVoteCasting : Attachment
    {
        public BinaryHexString RevealedSecret { get; set; }
        public string RevealedSecretText { get; set; }
        public List<BinaryHexString> TransactionFullHashes { get; set; }

        internal MessagingPhasingVoteCasting(JToken attachments)
        {
            TransactionFullHashes = ParseHashes(attachments.SelectToken(TransactionFullHashesKey)).ToList();
        }

        private static IEnumerable<BinaryHexString> ParseHashes(JToken hashesToken)
        {
            return hashesToken.Children<JValue>().Select(optionToken => new BinaryHexString(optionToken.Value.ToString()));
        }
    }

    public class MessagingVoteCastingAttachment : Attachment
    {
        public ulong PollId { get; set; }
        public List<int> Votes { get; set; }

        internal MessagingVoteCastingAttachment(JToken attachments)
        {
            PollId = UInt64.Parse(GetAttachmentValue<string>(attachments, PollKey));
            Votes = ParseVotes(attachments.SelectToken(VoteKey)).ToList();
        }

        private IEnumerable<int> ParseVotes(JToken votesToken)
        {
            return votesToken.Children<JValue>().Select(optionToken => (int)(long)optionToken.Value);
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

    public class MonetarySystemReserveIncreaseAttachment : Attachment
    {
        public Amount AmountPerUnit { get; set; }
        public ulong CurrencyId { get; set; }

        internal MonetarySystemReserveIncreaseAttachment(JToken attachments)
        {
            AmountPerUnit = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, AmountPerUnitNqtKey));
            CurrencyId = GetAttachmentValue<ulong>(attachments, CurrencyKey);
        }
    }

    public class OrdinaryPaymentAttachment : Attachment
    {
        // ReSharper disable UnusedParameter.Local
        internal OrdinaryPaymentAttachment(JToken attachments)
        {
        }
        // ReSharper restore UnusedParameter.Local
    }
}
