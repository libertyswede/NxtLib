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
        MonetarySystem
    }

    public enum TransactionSubType
    {
        // Payment
        PaymentOrdinaryPayment,

        // Messaging
        MessagingArbitraryMessage,
        MessagingAliasAssignment,
        MessagingAliasSell,
        MessagingAliasBuy,
        MessagingAliasDelete,
        MessagingPhasingVoteCasting,
        MessagingPollCreation,
        MessagingVoteCasting,
        MessagingHubTerminalAnnouncement,
        MessagingAccountInfo,

        // ColoredCoins
        ColoredCoinsAssetIssuance,
        ColoredCoinsAssetTransfer,
        ColoredCoinsAskOrderPlacement,
        ColoredCoinsBidOrderPlacement,
        ColoredCoinsAskOrderCancellation,
        ColoredCoinsBidOrderCancellation,
        ColoredCoinsDividendPayment,

        // DigitalGoods
        DigitalGoodsListing,
        DigitalGoodsDelisting,
        DigitalGoodsPriceChange,
        DigitalGoodsQuantityChange,
        DigitalGoodsPurchase,
        DigitalGoodsDelivery,
        DigitalGoodsFeedback,
        DigitalGoodsRefund,

        // AccountControl
        AccountControlEffectiveBalanceLeasing,

        // MonetarySystem
        MonetarySystemCurrencyIssuance,
        MonetarySystemReserveIncrease,
        MonetarySystemReserveClaim,
        MonetarySystemCurrencyTransfer,
        MonetarySystemPublishExchangeOffer,
        MonetarySystemExchangeBuy,
        MonetarySystemExchangeSell,
        MonetarySystemCurrencyMinting,
        MonetarySystemCurrencyDeletion
    }
}