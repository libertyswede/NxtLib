using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    [JsonConverter(typeof(TransactionConverter))]
    public class Transaction
    {
        public Amount Amount { get; set; }
        public Attachment Attachment { get; set; }
        public ulong? BlockId { get; set; }
        public DateTime? BlockTimestamp { get; set; }
        public int? Confirmations { get; set; }
        public short Deadline { get; set; }
        public ulong EcBlockId { get; set; }
        public int EcBlockHeight { get; set; }
        public EncryptedMessage EncryptedMessage { get; set; }
        public EncryptToSelfMessage EncryptToSelfMessage { get; set; }
        public Amount Fee { get; set; }
        public string FullHash { get; set; }
        public int Height { get; set; }
        public Message Message { get; set; }
        public ulong? Recipient { get; set; }
        public string RecipientRs { get; set; }
        public BinaryHexString ReferencedTransactionFullHash { get; set; }
        public TransactionPhasing Phasing { get; set; }
        public PublicKeyAnnouncement PublicKeyAnnouncement { get; set; }
        public ulong Sender { get; set; }
        public string SenderRs { get; set; }
        public BinaryHexString SenderPublicKey { get; set; }
        public BinaryHexString Signature { get; set; }
        public string SignatureHash { get; set; }
        public TransactionSubType SubType { get; set; }
        public DateTime Timestamp { get; set; }
        public ulong? TransactionId { get; set; }
        public int TransactionIndex { get; set; }
        public TransactionMainType Type { get; set; }
        public int Version { get; set; }
    }

    public enum TransactionMainType
    {
        Payment,
        Messaging,
        ColoredCoins,
        DigitalGoods,
        AccountControl,
        MonetarySystem,
        TaggedData
    }

    public enum TransactionSubType
    {
        // Payment
        [Description("OrdinaryPayment")]
        PaymentOrdinaryPayment,

        // Messaging
        [Description("ArbitraryMessage")]
        MessagingArbitraryMessage,
        [Description("AliasAssignment")]
        MessagingAliasAssignment,
        [Description("PollCreation")]
        MessagingPollCreation,
        [Description("VoteCasting")]
        MessagingVoteCasting,
        [Description("HubAnnouncement")]
        MessagingHubTerminalAnnouncement,
        [Description("AccountInfo")]
        MessagingAccountInfo,
        [Description("AliasSell")]
        MessagingAliasSell,
        [Description("AliasBuy")]
        MessagingAliasBuy,
        [Description("AliasDelete")]
        MessagingAliasDelete,
        [Description("PhasingVoteCasting")]
        MessagingPhasingVoteCasting,

        // ColoredCoins
        [Description("AssetIssuance")]
        ColoredCoinsAssetIssuance,
        [Description("AssetTransfer")]
        ColoredCoinsAssetTransfer,
        [Description("AskOrderPlacement")]
        ColoredCoinsAskOrderPlacement,
        [Description("BidOrderPlacement")]
        ColoredCoinsBidOrderPlacement,
        [Description("AskOrderCancellation")]
        ColoredCoinsAskOrderCancellation,
        [Description("BidOrderCancellation")]
        ColoredCoinsBidOrderCancellation,
        [Description("DividendPayment")]
        ColoredCoinsDividendPayment,

        // DigitalGoods
        [Description("DigitalGoodsListing")]
        DigitalGoodsListing,
        [Description("DigitalGoodsDelisting")]
        DigitalGoodsDelisting,
        [Description("DigitalGoodsPriceChange")]
        DigitalGoodsPriceChange,
        [Description("DigitalGoodsQuantityChange")]
        DigitalGoodsQuantityChange,
        [Description("DigitalGoodsPurchase")]
        DigitalGoodsPurchase,
        [Description("DigitalGoodsDelivery")]
        DigitalGoodsDelivery,
        [Description("DigitalGoodsFeedback")]
        DigitalGoodsFeedback,
        [Description("DigitalGoodsRefund")]
        DigitalGoodsRefund,

        // AccountControl
        [Description("EffectiveBalanceLeasing")]
        AccountControlEffectiveBalanceLeasing,

        // MonetarySystem
        [Description("CurrencyIssuance")]
        MonetarySystemCurrencyIssuance,
        [Description("ReserveIncrease")]
        MonetarySystemReserveIncrease,
        [Description("ReserveClaim")]
        MonetarySystemReserveClaim,
        [Description("CurrencyTransfer")]
        MonetarySystemCurrencyTransfer,
        [Description("PublishExchangeOffer")]
        MonetarySystemPublishExchangeOffer,
        [Description("ExchangeBuy")]
        MonetarySystemExchangeBuy,
        [Description("ExchangeSell")]
        MonetarySystemExchangeSell,
        [Description("CurrencyMinting")]
        MonetarySystemCurrencyMinting,
        [Description("CurrencyDeletion")]
        MonetarySystemCurrencyDeletion,

        // TaggedData
        [Description("TaggedDataUpload")]
        TaggedDataUpload,
        [Description("TaggedDataExtend")]
        TaggedDataExtend
    }
}