using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.Shuffling;
using NxtLib.VotingSystem;

namespace NxtLib
{
    public abstract class Attachment : Appendix
    {
    }

    public class AccountControlEffectiveBalanceLeasingAttachment : Attachment
    {
        public ushort Period { get; set; }

        internal AccountControlEffectiveBalanceLeasingAttachment(JToken attachments)
        {
            Period = GetAttachmentValue<ushort>(attachments, Parameters.Period);
        }
    }

    public class AccountControlSetPhasingOnlyAttachment : Attachment
    {
        public Amount ControlMaxFees { get; set; }
        public int ControlMinDuration { get; set; }
        public int ControlMaxDuration { get; set; }

        public ulong PhasingHoldingId { get; set; }
        public long PhasingQuorum { get; set; }
        public long PhasingMinBalance { get; set; }
        public MinBalanceModel PhasingMinBalanceModel { get; set; }
        public IEnumerable<ulong> PhasingWhitelist { get; set; } = Enumerable.Empty<ulong>();
        public VotingModel PhasingVotingModel { get; set; }

        internal AccountControlSetPhasingOnlyAttachment(JToken attachments)
        {
            ControlMaxFees = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.ControlMaxFees));
            ControlMinDuration = GetAttachmentValue<int>(attachments, Parameters.ControlMinDuration);
            ControlMaxDuration = GetAttachmentValue<int>(attachments, Parameters.ControlMaxDuration);

            var phasing = attachments.SelectToken(Parameters.PhasingControlParams);
            PhasingHoldingId = GetAttachmentValue<ulong>(phasing, Parameters.PhasingHolding);
            PhasingQuorum = GetAttachmentValue<long>(phasing, Parameters.PhasingQuorum);
            PhasingMinBalance = GetAttachmentValue<long>(phasing, Parameters.PhasingMinBalance);
            PhasingMinBalanceModel = (MinBalanceModel)GetAttachmentValue<int>(phasing, Parameters.PhasingMinBalanceModel);
            PhasingVotingModel = (VotingModel) GetAttachmentValue<int>(phasing, Parameters.PhasingVotingModel);
            
            if (phasing.SelectToken(Parameters.PhasingWhitelist) != null)
            {
                var array = (JArray)phasing.SelectToken(Parameters.PhasingWhitelist);
                PhasingWhitelist = array.ToObject<ulong[]>();
            }
        }
    }

    public abstract class ColoredCoinsOrderCancellationAttachment : Attachment
    {
        public ulong OrderId { get; private set; }

        protected ColoredCoinsOrderCancellationAttachment(JToken attachments)
        {
            OrderId = GetAttachmentValue<ulong>(attachments, Parameters.Order);
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
            Decimals = GetAttachmentValue<byte>(attachments, Parameters.Decimals);
            Description = GetAttachmentValue<string>(attachments, Parameters.Description);
            Name = GetAttachmentValue<string>(attachments, Parameters.Name);
            QuantityQnt = GetAttachmentValue<long>(attachments, Parameters.QuantityQnt);
        }
    }

    public class ColoredCoinsAssetTransferAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }
        public string Comment { get; set; }

        internal ColoredCoinsAssetTransferAttachment(JToken attachments)
        {
            AssetId = GetAttachmentValue<ulong>(attachments, Parameters.Asset);
            QuantityQnt = GetAttachmentValue<long>(attachments, Parameters.QuantityQnt);
            if (attachments.SelectToken(Parameters.Comment) != null)
            {
                Comment = GetAttachmentValue<string>(attachments, Parameters.Comment);
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
            AssetId = GetAttachmentValue<ulong>(attachments, Parameters.Asset);
            QuantityQnt = GetAttachmentValue<long>(attachments, Parameters.QuantityQnt);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.PriceNqt));
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
            GoodsId = GetAttachmentValue<ulong>(attachments, Parameters.Goods);
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
            Discount = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.DiscountNqt));
            GoodsData = new BinaryHexString(GetAttachmentValue<string>(attachments, Parameters.GoodsData));
            GoodsIsText = GetAttachmentValue<bool>(attachments, Parameters.GoodsIsText);
            GoodsNonce = new BinaryHexString(GetAttachmentValue<string>(attachments, Parameters.GoodsNonce));
            Purchase = GetAttachmentValue<ulong>(attachments, Parameters.Purchase);
        }
    }

    public class ColoredCoinsDividendPaymentAttachment : Attachment
    {
        public Amount AmountPerQnt { get; set; }
        public ulong AssetId { get; set; }
        public int Height { get; set; }

        internal ColoredCoinsDividendPaymentAttachment(JToken attachments)
        {
            AmountPerQnt = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.AmountNqtPerQnt));
            AssetId = GetAttachmentValue<ulong>(attachments, Parameters.Asset);
            Height = GetAttachmentValue<int>(attachments, Parameters.Height);
        }
    }

    public class ColoredCoinsDeleteAttachment : Attachment
    {
        public ulong AssetId { get; set; }
        public long QuantityQnt { get; set; }

        internal ColoredCoinsDeleteAttachment(JToken attachments)
        {
            AssetId = GetAttachmentValue<ulong>(attachments, Parameters.Asset);
            QuantityQnt = GetAttachmentValue<long>(attachments, Parameters.QuantityQnt);
        }
    }

    public class DigitalGoodsFeedbackAttachment : Attachment
    {
        public ulong PurchaseId { get; set; }

        internal DigitalGoodsFeedbackAttachment(JToken attachments)
        {
            PurchaseId = GetAttachmentValue<ulong>(attachments, Parameters.Purchase);
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
            Name = GetAttachmentValue<string>(attachments, Parameters.Name);
            Description = GetAttachmentValue<string>(attachments, Parameters.Description);
            Tags = GetAttachmentValue<string>(attachments, Parameters.Tags);
            Quantity = GetAttachmentValue<int>(attachments, Parameters.Quantity);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.PriceNqt));
        }
    }

    public class DigitalGoodsPriceChangeAttachment : Attachment
    {
        public ulong GoodsId { get; set; }
        public Amount Price { get; set; }

        internal DigitalGoodsPriceChangeAttachment(JToken attachments)
        {
            GoodsId = GetAttachmentValue<ulong>(attachments, Parameters.Goods);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.PriceNqt));
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
                new DateTimeConverter().GetFromNxtTime(GetAttachmentValue<int>(attachments, Parameters.DeliveryDeadlineTimestamp));
            GoodsId = GetAttachmentValue<ulong>(attachments, Parameters.Goods);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.PriceNqt));
            Quantity = GetAttachmentValue<int>(attachments, Parameters.Quantity);
        }
    }

    public class DigitalGoodsQuantityChangeAttachment : Attachment
    {
        public int DeltaQuantity { get; set; }
        public ulong GoodsId { get; set; }

        internal DigitalGoodsQuantityChangeAttachment(JToken attachments)
        {
            DeltaQuantity = GetAttachmentValue<int>(attachments, Parameters.DeltaQuantity);
            GoodsId = GetAttachmentValue<ulong>(attachments, Parameters.Goods);
        }
    }

    public class DigitalGoodsRefundAttachment : Attachment
    {
        public ulong PurchaseId { get; set; }
        public Amount Refund { get; set; }

        internal DigitalGoodsRefundAttachment(JToken attachments)
        {
            PurchaseId = GetAttachmentValue<ulong>(attachments, Parameters.Purchase);
            Refund = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.RefundNqt));
        }
    }

    public class MessagingAccountInfoAttachment : Attachment
    {
        public string Name { get; set; }
        public string Description { get; set; }

        internal MessagingAccountInfoAttachment(JToken attachments)
        {
            Name = GetAttachmentValue<string>(attachments, Parameters.Name);
            Description = GetAttachmentValue<string>(attachments, Parameters.Description);
        }
    }

    public class MessagingAccountPropertyAttachment : Attachment
    {
        public string Property { get; set; }
        public string Value { get; set; }

        internal MessagingAccountPropertyAttachment(JToken attachments)
        {
            Property = GetAttachmentValue<string>(attachments, Parameters.Property);
            Value = GetAttachmentValue<string>(attachments, Parameters.Value);
        }
    }

    public class MessagingAccountPropertyDeleteAttachment : Attachment
    {
        public ulong Property { get; set; }

        internal MessagingAccountPropertyDeleteAttachment(JToken attachments)
        {
            Property = GetAttachmentValue<ulong>(attachments, Parameters.Property);
        }
    }

    public class MessagingAliasAssignmentAttachment : Attachment
    {
        public string Alias { get; set; }
        public string Uri { get; set; }

        internal MessagingAliasAssignmentAttachment(JToken attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, Parameters.Alias);
            Uri = GetAttachmentValue<string>(attachments, Parameters.Uri);
        }
    }

    public class MessagingAliasBuyAttachment : Attachment
    {
        public string Alias { get; set; }

        internal MessagingAliasBuyAttachment(JToken attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, Parameters.Alias);
        }
    }

    public class MessagingAliasDeleteAttachment : Attachment
    {
        public string Alias { get; set; }

        internal MessagingAliasDeleteAttachment(JToken attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, Parameters.Alias);
        }
    }

    public class MessagingAliasSellAttachment : Attachment
    {
        public string Alias { get; set; }
        public Amount Price { get; set; }

        internal MessagingAliasSellAttachment(JToken attachments)
        {
            Alias = GetAttachmentValue<string>(attachments, Parameters.Alias);
            Price = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.PriceNqt));
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
            Description = GetAttachmentValue<string>(attachments, Parameters.Description);
            FinishHeight = GetAttachmentValue<int>(attachments, Parameters.FinishHeight);
            HoldingId = ulong.Parse(GetAttachmentValue<string>(attachments, Parameters.Holding));
            MaxNumberOfOptions = GetAttachmentValue<int>(attachments, Parameters.MaxNumberOfOptions);
            MaxRangeValue = GetAttachmentValue<int>(attachments, Parameters.MaxRangeValue);
            MinBalance = GetAttachmentValue<long>(attachments, Parameters.MinBalance);
            MinBalanceModel = (MinBalanceModel) GetAttachmentValue<int>(attachments, Parameters.MinBalanceModel);
            MinRangeValue = GetAttachmentValue<int>(attachments, Parameters.MinRangeValue);
            MinNumberOfOptions = GetAttachmentValue<int>(attachments, Parameters.MinNumberOfOptions);
            Name = GetAttachmentValue<string>(attachments, Parameters.Name);
            Options = ParseOptions(attachments.SelectToken(Parameters.Options)).ToList();
            VotingModel = (VotingModel)GetAttachmentValue<int>(attachments, Parameters.VotingModel);
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
            TransactionFullHashes = ParseHashes(attachments.SelectToken(Parameters.TransactionFullHashes)).ToList();
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
            PollId = ulong.Parse(GetAttachmentValue<string>(attachments, Parameters.Poll));
            Votes = ParseVotes(attachments.SelectToken(Parameters.Vote)).ToList();
        }

        private static IEnumerable<int> ParseVotes(JToken votesToken)
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
            CurrencyId = GetAttachmentValue<ulong>(attachments, Parameters.Currency);
            Rate = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.RateNqt));
            Units = GetAttachmentValue<long>(attachments, Parameters.Units);
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
            CurrencyId = GetAttachmentValue<ulong>(attachments, Parameters.Currency);
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
            Algorithm = GetAttachmentValue<byte>(attachments, Parameters.Algorithm);
            Code = GetAttachmentValue<string>(attachments, Parameters.Code);
            Decimals = GetAttachmentValue<byte>(attachments, Parameters.Decimals);
            Description = GetAttachmentValue<string>(attachments, Parameters.Description);
            InitialSupply = GetAttachmentValue<long>(attachments, Parameters.InitialSupply);
            IssuanceHeight = GetAttachmentValue<int>(attachments, Parameters.IssuanceHeight);
            MaxDifficulty = GetAttachmentValue<int>(attachments, Parameters.MaxDifficulty);
            MaxSupply = GetAttachmentValue<long>(attachments, Parameters.MaxSupply);
            MinDifficulty = GetAttachmentValue<int>(attachments, Parameters.MinDifficulty);
            MinReservePerUnit = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.MinReservePerUnitNqt));
            Name = GetAttachmentValue<string>(attachments, Parameters.Name);
            ReserveSupply = GetAttachmentValue<long>(attachments, Parameters.ReserveSupply);
            Ruleset = GetAttachmentValue<byte>(attachments, Parameters.Ruleset);
            SetTypes(GetAttachmentValue<int>(attachments, Parameters.Type));
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
            Counter = GetAttachmentValue<long>(attachments, Parameters.Counter);
            CurrencyId = GetAttachmentValue<ulong>(attachments, Parameters.Currency);
            Nonce = GetAttachmentValue<long>(attachments, Parameters.Nonce);
            Units = GetAttachmentValue<long>(attachments, Parameters.Units);
        }
    }

    public class MonetarySystemCurrencyTransferAttachment : Attachment
    {
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        internal MonetarySystemCurrencyTransferAttachment(JToken attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, Parameters.Currency);
            Units = GetAttachmentValue<long>(attachments, Parameters.Units);
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
            BuyRate = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.BuyRateNqt));
            CurrencyId = GetAttachmentValue<ulong>(attachments, Parameters.Currency);
            ExpirationHeight = GetAttachmentValue<int>(attachments, Parameters.ExpirationHeight);
            InitialBuySupply = GetAttachmentValue<long>(attachments, Parameters.InitialBuySupply);
            InitialSellSupply = GetAttachmentValue<long>(attachments, Parameters.InitialSellSupply);
            SellRate = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.SellRateNqt));
            TotalBuyLimit = GetAttachmentValue<long>(attachments, Parameters.TotalBuyLimit);
            TotalSellLimit = GetAttachmentValue<long>(attachments, Parameters.TotalSellLimit);
        }
    }

    public class MonetarySystemReserveClaimAttachment : Attachment
    {
        public ulong CurrencyId { get; set; }
        public long Units { get; set; }

        internal MonetarySystemReserveClaimAttachment(JToken attachments)
        {
            CurrencyId = GetAttachmentValue<ulong>(attachments, Parameters.Currency);
            Units = GetAttachmentValue<long>(attachments, Parameters.Units);
        }
    }

    public class MonetarySystemReserveIncreaseAttachment : Attachment
    {
        public Amount AmountPerUnit { get; set; }
        public ulong CurrencyId { get; set; }

        internal MonetarySystemReserveIncreaseAttachment(JToken attachments)
        {
            AmountPerUnit = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(attachments, Parameters.AmountPerUnitNqt));
            CurrencyId = GetAttachmentValue<ulong>(attachments, Parameters.Currency);
        }
    }

    public class OrdinaryPaymentAttachment : Attachment
    {
        internal OrdinaryPaymentAttachment()
        {
        }
    }

    public abstract class TaggedDataAttachment : Attachment, ITaggedData
    {
        public string Channel { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public bool IsText { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }

        protected void ParseTaggedData(JToken jToken)
        {
            if (jToken.SelectToken(Parameters.Data) != null)
            {
                Channel = GetAttachmentValue<string>(jToken, Parameters.Channel);
                Data = GetAttachmentValue<string>(jToken, Parameters.Data);
                Description = GetAttachmentValue<string>(jToken, Parameters.Description);
                Filename = GetAttachmentValue<string>(jToken, Parameters.Filename);
                IsText = GetAttachmentValue<bool>(jToken, Parameters.IsText);
                Name = GetAttachmentValue<string>(jToken, Parameters.Name);
                Tags = GetAttachmentValue<string>(jToken, Parameters.Tags);
                Type = GetAttachmentValue<string>(jToken, Parameters.Type);
            }
        }
    }

    public class TaggedDataExtendAttachment : TaggedDataAttachment
    {
        public ulong TaggedDataId { get; set; }

        internal TaggedDataExtendAttachment(JToken jToken)
        {
            TaggedDataId = GetAttachmentValue<ulong>(jToken, Parameters.TaggedData);
            ParseTaggedData(jToken);
        }
    }

    public class TaggedDataUploadAttachment : TaggedDataAttachment
    {
        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Hash { get; set; }

        internal TaggedDataUploadAttachment()
        {
        }

        internal TaggedDataUploadAttachment(JToken jToken)
        {
            Hash = new BinaryHexString(GetAttachmentValue<string>(jToken, Parameters.Hash));
            ParseTaggedData(jToken);
        }
    }

    public class ShuffligCancellationAttachment : Attachment
    {
        public BinaryHexString BlameData { get; set; }
        public ulong CancellingAccountId { get; set; }
        public BinaryHexString KeySeeds { get; set; }
        public ulong ShufflingId { get; set; }
        public BinaryHexString ShufflingStateHash { get; set; }
        
        internal ShuffligCancellationAttachment(JToken jToken)
        {
            BlameData = GetAttachmentValue<string>(jToken, Parameters.BlameData);
            CancellingAccountId = GetAttachmentValue<ulong>(jToken, Parameters.CancellingAccount);
            KeySeeds = GetAttachmentValue<string>(jToken, Parameters.KeySeeds);
            ShufflingId = GetAttachmentValue<ulong>(jToken, Parameters.Shuffling);
            ShufflingStateHash = GetAttachmentValue<string>(jToken, Parameters.ShufflingStateHash);
        }
    }

    public class ShufflingCreationAttachment : Attachment
    {
        public Amount Amount { get; }
        public ulong HoldingId { get; }
        public HoldingType HoldingType { get; }
        public int ParticipantCount { get; }
        public int RegistrationPeriod { get; }
        
        internal ShufflingCreationAttachment(JToken jToken)
        {
            Amount = Amount.CreateAmountFromNqt(GetAttachmentValue<long>(jToken, Parameters.Amount));
            HoldingId = GetAttachmentValue<ulong>(jToken, Parameters.Holding);
            HoldingType = (HoldingType) GetAttachmentValue<int>(jToken, Parameters.HoldingType);
            ParticipantCount = GetAttachmentValue<int>(jToken, Parameters.ParticipantCount);
            RegistrationPeriod = GetAttachmentValue<int>(jToken, Parameters.RegistrationPeriod);
        }
    }

    public class ShufflingRegistrationAttachment : Attachment
    {
        public BinaryHexString ShufflingFullHash { get; }

        internal ShufflingRegistrationAttachment(JToken jToken)
        {
            ShufflingFullHash = GetAttachmentValue<string>(jToken, Parameters.ShufflingFullHash);
        }
    }

    public class ShufflingProcessingAttachment : Attachment
    {
        public IEnumerable<BinaryHexString> Data { get; set; }
        public BinaryHexString Hash { get; set; }
        public ulong ShufflingId { get; set; }
        public BinaryHexString ShufflingStateHash { get; set; }

        internal ShufflingProcessingAttachment(JToken jToken)
        {
            Hash = GetAttachmentValue<string>(jToken, Parameters.Hash);
            ShufflingId = GetAttachmentValue<ulong>(jToken, Parameters.Shuffling);
            ShufflingStateHash = GetAttachmentValue<string>(jToken, Parameters.ShufflingStateHash);

            var array = (JArray)jToken.SelectToken(Parameters.Data);
            Data = array.ToObject<string[]>().Select(s => new BinaryHexString(s));
        }
    }

    public class ShufflingRecipientsAttachment : Attachment
    {
        public IEnumerable<BinaryHexString> RecipientPublicKeys { get; set; }
        public ulong ShufflingId { get; set; }
        public BinaryHexString ShufflingStateHash { get; set; }

        internal ShufflingRecipientsAttachment(JToken jToken)
        {
            ShufflingId = GetAttachmentValue<ulong>(jToken, Parameters.Shuffling);
            ShufflingStateHash = GetAttachmentValue<string>(jToken, Parameters.ShufflingStateHash);

            var array = (JArray)jToken.SelectToken(Parameters.RecipientPublicKeys);
            RecipientPublicKeys = array.ToObject<string[]>().Select(s => new BinaryHexString(s));
        }
    }

    public class ShufflingVerificationAttachment : Attachment
    {
        public ulong ShufflingId { get; set; }
        public BinaryHexString ShufflingStateHash { get; set; }

        internal ShufflingVerificationAttachment(JToken jToken)
        {
            ShufflingId = GetAttachmentValue<ulong>(jToken, Parameters.Shuffling);
            ShufflingStateHash = GetAttachmentValue<string>(jToken, Parameters.ShufflingStateHash);
        }
    }
}
