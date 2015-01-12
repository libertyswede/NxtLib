using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace NxtLib.Internal
{
    internal class AttachmentConverter
    {
        private readonly JObject _attachments;
        private static readonly IReadOnlyDictionary<TransactionSubType, Func<IReadOnlyDictionary<string, object>, Attachment>> AttachmentFuncs;
        private readonly Dictionary<string, object> _attachmentsDictionary;

        static AttachmentConverter()
        {
            var attachmentFuncs = new Dictionary<TransactionSubType, Func<IReadOnlyDictionary<string, object>, Attachment>>();

            attachmentFuncs.Add(TransactionSubType.AccountControlEffectiveBalanceLeasing, value => new AccountControlEffectiveBalanceLeasingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsAskOrderCancellation, value => new ColoredCoinsAskOrderCancellationAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsAskOrderPlacement, value => new ColoredCoinsAskOrderPlacementAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsAssetIssuance, value => new ColoredCoinsAssetIssuanceAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsAssetTransfer, value => new ColoredCoinsAssetTransferAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsBidOrderCancellation, value => new ColoredCoinsBidOrderCancellationAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsBidOrderPlacement, value => new ColoredCoinsBidOrderPlacementAttachment(value));
            attachmentFuncs.Add(TransactionSubType.ColoredCoinsDividendPayment, value => new ColoredCoinsDividendPaymentAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsDelisting, value => new DigitalGoodsDelistingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsDelivery, value => new DigitalGoodsDeliveryAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsFeedback, value => new DigitalGoodsFeedbackAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsListing, value => new DigitalGoodsListingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsPriceChange, value => new DigitalGoodsPriceChangeAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsPurchase, value => new DigitalGoodsPurchaseAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsQuantityChange, value => new DigitalGoodsQuantityChangeAttachment(value));
            attachmentFuncs.Add(TransactionSubType.DigitalGoodsRefund, value => new DigitalGoodsRefundAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAccountInfo, value => new MessagingAccountInfoAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAliasAssignment, value => new MessagingAliasAssignmentAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAliasBuy, value => new MessagingAliasBuyAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAliasDelete, value => new MessagingAliasDeleteAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingAliasSell, value => new MessagingAliasSellAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MessagingArbitraryMessage, value => null);
            //attachmentFuncs.Add(TransactionSubType.MessagingHubTerminalAnnouncement, TODO: .... );
            //attachmentFuncs.Add(TransactionSubType.MessagingPollCreation, TODO: .... );
            //attachmentFuncs.Add(TransactionSubType.MessagingVoteCasting, TODO: .... );
            attachmentFuncs.Add(TransactionSubType.MonetarySystemCurrencyDeletion, value => new MonetarySystemCurrencyDeletion(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemCurrencyIssuance, value => new MonetarySystemCurrencyIssuanceAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemCurrencyMinting, value => new MonetarySystemCurrencyMintingAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemCurrencyTransfer, value => new MonetarySystemCurrencyTransferAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemExchangeBuy, value => new MonetarySystemExchangeBuyAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemExchangeSell, value => new MonetarySystemExchangeSellAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemPublishExchangeOffer, value => new MonetarySystemPublishExchangeOfferAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemReserveClaim, value => new MonetarySystemReserveClaimAttachment(value));
            attachmentFuncs.Add(TransactionSubType.MonetarySystemReserveIncrease, value => new MonetarySystemReserveIncrease(value));
            attachmentFuncs.Add(TransactionSubType.PaymentOrdinaryPayment, value => null);

            AttachmentFuncs = new ReadOnlyDictionary<TransactionSubType, Func<IReadOnlyDictionary<string, object>, Attachment>>(attachmentFuncs);
        }

        internal AttachmentConverter(JObject attachments)
        {
            _attachments = attachments;
            _attachmentsDictionary = GetDictionaryFromAttachments(_attachments);
        }

        internal PublicKeyAnnouncement GetPublicKeyAnnouncement()
        {
            if (_attachments == null || !_attachmentsDictionary.ContainsKey(PublicKeyAnnouncement.AppendixName))
            {
                return null;
            }
            var message = new PublicKeyAnnouncement(_attachmentsDictionary);
            return message;
        }

        internal EncryptToSelfMessage GetEncryptToSelfMessage()
        {
            if (_attachments == null || !_attachmentsDictionary.ContainsKey(EncryptToSelfMessage.AppendixName))
            {
                return null;
            }
            var message = new EncryptToSelfMessage(_attachmentsDictionary);
            return message;
        }

        internal EncryptedMessage GetEncryptedMessage()
        {
            if (_attachments == null || !_attachmentsDictionary.ContainsKey(EncryptedMessage.AppendixName))
            {
                return null;
            } 
            var message = new EncryptedMessage(_attachmentsDictionary);
            return message;
        }

        internal UnencryptedMessage GetMessage()
        {
            if (_attachments == null || !_attachmentsDictionary.ContainsKey(UnencryptedMessage.AppendixName))
            {
                return null;
            }
            var message = new UnencryptedMessage(_attachmentsDictionary);
            return message;
        }

        internal Attachment GetAttachment(TransactionSubType transactionSubType)
        {
            var attachment = AttachmentFuncs[transactionSubType].Invoke(_attachmentsDictionary);
            return attachment;
        }

        private static Dictionary<string, object> GetDictionaryFromAttachments(JToken attachments)
        {
            var dictionary = new Dictionary<string, object>();
            if (attachments == null)
            {
                return dictionary;
            }
            foreach (var child in attachments.Children<JProperty>())
            {
                var jValue = child.Value as JValue;
                dictionary.Add(child.Name, jValue != null ? jValue.Value : GetDictionaryFromAttachments(child.Value));
            }

            return dictionary;
        }

        internal bool AreAttachmentsLeft()
        {
            return _attachmentsDictionary.Any(a => !a.Key.StartsWith("version."));
        }
    }
}