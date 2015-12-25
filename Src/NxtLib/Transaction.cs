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
        TaggedData,
        Shuffling
    }

    public enum TransactionSubType
    {
        // Payment
        [NxtApi("OrdinaryPayment")]
        PaymentOrdinaryPayment,

        // Messaging
        [NxtApi("ArbitraryMessage")]
        MessagingArbitraryMessage,
        [NxtApi("AliasAssignment")]
        MessagingAliasAssignment,
        [NxtApi("PollCreation")]
        MessagingPollCreation,
        [NxtApi("VoteCasting")]
        MessagingVoteCasting,
        [NxtApi("HubAnnouncement")]
        MessagingHubTerminalAnnouncement,
        [NxtApi("AccountInfo")]
        MessagingAccountInfo,
        [NxtApi("AliasSell")]
        MessagingAliasSell,
        [NxtApi("AliasBuy")]
        MessagingAliasBuy,
        [NxtApi("AliasDelete")]
        MessagingAliasDelete,
        [NxtApi("PhasingVoteCasting")]
        MessagingPhasingVoteCasting,

        // ColoredCoins
        [NxtApi("AssetIssuance")]
        ColoredCoinsAssetIssuance,
        [NxtApi("AssetTransfer")]
        ColoredCoinsAssetTransfer,
        [NxtApi("AskOrderPlacement")]
        ColoredCoinsAskOrderPlacement,
        [NxtApi("BidOrderPlacement")]
        ColoredCoinsBidOrderPlacement,
        [NxtApi("AskOrderCancellation")]
        ColoredCoinsAskOrderCancellation,
        [NxtApi("BidOrderCancellation")]
        ColoredCoinsBidOrderCancellation,
        [NxtApi("DividendPayment")]
        ColoredCoinsDividendPayment,
        [NxtApi("AssetDelete")]
        ColoredCoinsAssetDelete,

        // DigitalGoods
        [NxtApi("DigitalGoodsListing")]
        DigitalGoodsListing,
        [NxtApi("DigitalGoodsDelisting")]
        DigitalGoodsDelisting,
        [NxtApi("DigitalGoodsPriceChange")]
        DigitalGoodsPriceChange,
        [NxtApi("DigitalGoodsQuantityChange")]
        DigitalGoodsQuantityChange,
        [NxtApi("DigitalGoodsPurchase")]
        DigitalGoodsPurchase,
        [NxtApi("DigitalGoodsDelivery")]
        DigitalGoodsDelivery,
        [NxtApi("DigitalGoodsFeedback")]
        DigitalGoodsFeedback,
        [NxtApi("DigitalGoodsRefund")]
        DigitalGoodsRefund,

        // AccountControl
        [NxtApi("EffectiveBalanceLeasing")]
        AccountControlEffectiveBalanceLeasing,
        [NxtApi("SetPhasingOnly")]
        AccountControlSetPhasingOnly,

        // MonetarySystem
        [NxtApi("CurrencyIssuance")]
        MonetarySystemCurrencyIssuance,
        [NxtApi("ReserveIncrease")]
        MonetarySystemReserveIncrease,
        [NxtApi("ReserveClaim")]
        MonetarySystemReserveClaim,
        [NxtApi("CurrencyTransfer")]
        MonetarySystemCurrencyTransfer,
        [NxtApi("PublishExchangeOffer")]
        MonetarySystemPublishExchangeOffer,
        [NxtApi("ExchangeBuy")]
        MonetarySystemExchangeBuy,
        [NxtApi("ExchangeSell")]
        MonetarySystemExchangeSell,
        [NxtApi("CurrencyMinting")]
        MonetarySystemCurrencyMinting,
        [NxtApi("CurrencyDeletion")]
        MonetarySystemCurrencyDeletion,

        // TaggedData
        [NxtApi("TaggedDataUpload")]
        TaggedDataUpload,
        [NxtApi("TaggedDataExtend")]
        TaggedDataExtend,

        // Shuffling
        [NxtApi("ShufflingCreation")]
        ShufflingCreation,
        [NxtApi("ShufflingRegistration")]
        ShufflingRegistration,
        [NxtApi("ShufflingProcessing")]
        ShufflingProcessing,
        [NxtApi("ShufflingRecipients")]
        ShufflingRecipients
    }
}