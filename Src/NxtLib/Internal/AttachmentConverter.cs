using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using System.IO;

namespace NxtLib.Internal
{
    internal class AttachmentConverter
    {
        private readonly JObject _attachments;
        private readonly BinaryReader _reader;
        private readonly int _transactionVersion;

        private static readonly IReadOnlyDictionary<TransactionSubType, Func<JObject, Attachment>> AttachmentFuncs;
        private static readonly IReadOnlyDictionary<TransactionSubType, Func<BinaryReader, int, Attachment>> BinaryAttachmentFuncs;

        static AttachmentConverter()
        {
            var attachmentFuncs = new Dictionary<TransactionSubType, Func<JObject, Attachment>>();
            var binaryAttachmentFuncs = new Dictionary<TransactionSubType, Func<BinaryReader, int, Attachment>>();

            attachmentFuncs.Add(TransactionSubType.AccountControlEffectiveBalanceLeasing, value => new AccountControlEffectiveBalanceLeasingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.AccountControlSetPhasingOnly, value => new AccountControlSetPhasingOnlyAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsAskOrderCancellation, value => new ColoredCoinsAskOrderCancellationAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsAskOrderPlacement, value => new ColoredCoinsAskOrderPlacementAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsAssetIssuance, value => new ColoredCoinsAssetIssuanceAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsAssetTransfer, value => new ColoredCoinsAssetTransferAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsBidOrderCancellation, value => new ColoredCoinsBidOrderCancellationAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsBidOrderPlacement, value => new ColoredCoinsBidOrderPlacementAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsDividendPayment, value => new ColoredCoinsDividendPaymentAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsAssetDelete, value => new ColoredCoinsDeleteAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsDelisting, value => new DigitalGoodsDelistingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsDelivery, value => new DigitalGoodsDeliveryAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsFeedback, value => new DigitalGoodsFeedbackAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsListing, value => new DigitalGoodsListingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsPriceChange, value => new DigitalGoodsPriceChangeAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsPurchase, value => new DigitalGoodsPurchaseAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsQuantityChange, value => new DigitalGoodsQuantityChangeAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsRefund, value => new DigitalGoodsRefundAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAccountInfo, value => new MessagingAccountInfoAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAccountProperty, value => new MessagingAccountPropertyAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAccountPropertyDelete, value => new MessagingAccountPropertyDeleteAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAliasAssignment, value => new MessagingAliasAssignmentAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAliasBuy, value => new MessagingAliasBuyAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAliasDelete, value => new MessagingAliasDeleteAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAliasSell, value => new MessagingAliasSellAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingArbitraryMessage, value => new MessagingArbitraryMessageAttachment());
            binaryAttachmentFuncs.Add(TransactionSubType.MessagingArbitraryMessage, (reader, version) => new MessagingArbitraryMessageAttachment());
            //attachmentFuncs.Add(TransactionSubType.MessagingHubTerminalAnnouncement, TODO: .... );
            attachmentFuncs.Add(TransactionSubType.MessagingPollCreation, value => new MessagingPollCreationAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingPhasingVoteCasting, value => new MessagingPhasingVoteCasting(value));
            attachmentFuncs.Add(TransactionSubType.MessagingVoteCasting, value => new MessagingVoteCastingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemCurrencyDeletion, value => new MonetarySystemCurrencyDeletion(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemCurrencyIssuance, value => new MonetarySystemCurrencyIssuanceAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemCurrencyMinting, value => new MonetarySystemCurrencyMintingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemCurrencyTransfer, value => new MonetarySystemCurrencyTransferAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemExchangeBuy, value => new MonetarySystemExchangeBuyAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemExchangeSell, value => new MonetarySystemExchangeSellAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemPublishExchangeOffer, value => new MonetarySystemPublishExchangeOfferAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemReserveClaim, value => new MonetarySystemReserveClaimAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemReserveIncrease, value => new MonetarySystemReserveIncreaseAttachment(value));
            attachmentFuncs.Add(TransactionSubType.PaymentOrdinaryPayment, value => new OrdinaryPaymentAttachment());
            binaryAttachmentFuncs.Add(TransactionSubType.PaymentOrdinaryPayment, (reader, version) => new OrdinaryPaymentAttachment());
            attachmentFuncs.Add(TransactionSubType.ShufflingCancellation, value => new ShufflingCreationAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ShufflingCreation, value => new ShufflingCreationAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ShufflingRegistration, value => new ShufflingRegistrationAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ShufflingProcessing, value => new ShufflingProcessingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ShufflingRecipients, value => new ShufflingRecipientsAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ShufflingVerification, value => new ShufflingVerificationAttachment(value));
            attachmentFuncs.Add(TransactionSubType.TaggedDataExtend, value => new TaggedDataExtendAttachment(value));
            attachmentFuncs.Add(TransactionSubType.TaggedDataUpload, value => new TaggedDataUploadAttachment(value));

            AttachmentFuncs = new ReadOnlyDictionary<TransactionSubType, Func<JObject, Attachment>>(attachmentFuncs);
            BinaryAttachmentFuncs = new ReadOnlyDictionary<TransactionSubType, Func<BinaryReader, int, Attachment>>(binaryAttachmentFuncs);
        }

        internal AttachmentConverter(JObject attachments)
        {
            _attachments = attachments;
        }

        internal AttachmentConverter(BinaryReader reader, int transactionVersion)
        {
            _reader = reader;
            _transactionVersion = transactionVersion;
        }

        internal Attachment GetAttachment(TransactionSubType transactionSubType)
        {
            if (_attachments != null)
            {
                var attachment = AttachmentFuncs[transactionSubType].Invoke(_attachments);
                return attachment;
            }
            if (_reader != null)
            {
                var attachment = BinaryAttachmentFuncs[transactionSubType].Invoke(_reader, _transactionVersion);
                return attachment;
            }
            return null;
        }
    }
}