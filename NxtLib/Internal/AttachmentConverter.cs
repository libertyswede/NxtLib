using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace NxtLib.Internal
{
    internal class AttachmentConverter
    {
        private readonly JObject _attachments;
        private static readonly IReadOnlyDictionary<string, Func<IReadOnlyDictionary<string, object>, Attachment>> AttachmentFuncs;
        private readonly IReadOnlyDictionary<string, object> _attachmentsDictionary;

        // TODO: Add following:
        // MessagingPollCreation
        // MessagingVoteCasting
        // MessagingHubAnnouncement
        // ColoredCoinsDividendPayment
        static AttachmentConverter()
        {
            var attachmentFuncs = new Dictionary<string, Func<IReadOnlyDictionary<string, object>, Attachment>>();

            attachmentFuncs.Add(AccountControlEffectiveBalanceLeasingAttachment.AttachmentName, value => new AccountControlEffectiveBalanceLeasingAttachment(value));
            attachmentFuncs.Add(ColoredCoinsAskOrderCancellationAttachment.AttachmentName, value => new ColoredCoinsAskOrderCancellationAttachment(value));
            attachmentFuncs.Add(ColoredCoinsAskOrderPlacementAttachment.AttachmentName, value => new ColoredCoinsAskOrderPlacementAttachment(value));
            attachmentFuncs.Add(ColoredCoinsAssetIssuanceAttachment.AttachmentName, value => new ColoredCoinsAssetIssuanceAttachment(value));
            attachmentFuncs.Add(ColoredCoinsAssetTransferAttachment.AttachmentName, value => new ColoredCoinsAssetTransferAttachment(value));
            attachmentFuncs.Add(ColoredCoinsBidOrderCancellationAttachment.AttachmentName, value => new ColoredCoinsBidOrderCancellationAttachment(value));
            attachmentFuncs.Add(ColoredCoinsBidOrderPlacementAttachment.AttachmentName, value => new ColoredCoinsBidOrderPlacementAttachment(value));
            attachmentFuncs.Add(DigitalGoodsDelistingAttachment.AttachmentName, value => new DigitalGoodsDelistingAttachment(value));
            attachmentFuncs.Add(DigitalGoodsDeliveryAttachment.AttachmentName, value => new DigitalGoodsDeliveryAttachment(value));
            attachmentFuncs.Add(ColoredCoinsDividendPaymentAttachment.AttachmentName, value => new ColoredCoinsDividendPaymentAttachment(value));
            attachmentFuncs.Add(DigitalGoodsFeedbackAttachment.AttachmentName, value => new DigitalGoodsFeedbackAttachment(value));
            attachmentFuncs.Add(DigitalGoodsListingAttachment.AttachmentName, value => new DigitalGoodsListingAttachment(value));
            attachmentFuncs.Add(DigitalGoodsPriceChangeAttachment.AttachmentName, value => new DigitalGoodsPriceChangeAttachment(value));
            attachmentFuncs.Add(DigitalGoodsPurchaseAttachment.AttachmentName, value => new DigitalGoodsPurchaseAttachment(value));
            attachmentFuncs.Add(DigitalGoodsQuantityChangeAttachment.AttachmentName, value => new DigitalGoodsQuantityChangeAttachment(value));
            attachmentFuncs.Add(DigitalGoodsRefundAttachment.AttachmentName, value => new DigitalGoodsRefundAttachment(value));
            attachmentFuncs.Add(MessagingAccountInfoAttachment.AttachmentName, value => new MessagingAccountInfoAttachment(value));
            attachmentFuncs.Add(MessagingAliasAssignmentAttachment.AttachmentName, value => new MessagingAliasAssignmentAttachment(value));
            attachmentFuncs.Add(MessagingAliasBuyAttachment.AttachmentName, value => new MessagingAliasBuyAttachment(value));
            attachmentFuncs.Add(MessagingAliasDeleteAttachment.AttachmentName, value => new MessagingAliasDeleteAttachment(value));
            attachmentFuncs.Add(MessagingAliasSellAttachment.AttachmentName, value => new MessagingAliasSellAttachment(value));
            attachmentFuncs.Add(MonetarySystemExchangeBuyAttachment.AttachmentName, value => new MonetarySystemExchangeBuyAttachment(value));
            attachmentFuncs.Add(MonetarySystemExchangeSellAttachment.AttachmentName, value => new MonetarySystemExchangeSellAttachment(value));
            attachmentFuncs.Add(MonetarySystemCurrencyIssuanceAttachment.AttachmentName, value => new MonetarySystemCurrencyIssuanceAttachment(value));
            attachmentFuncs.Add(MonetarySystemCurrencyMintingAttachment.AttachmentName, value => new MonetarySystemCurrencyMintingAttachment(value));
            attachmentFuncs.Add(MonetarySystemCurrencyTransferAttachment.AttachmentName, value => new MonetarySystemCurrencyTransferAttachment(value));
            attachmentFuncs.Add(MonetarySystemPublishExchangeOfferAttachment.AttachmentName, value => new MonetarySystemPublishExchangeOfferAttachment(value));
            attachmentFuncs.Add(MonetarySystemReserveClaimAttachment.AttachmentName, value => new MonetarySystemReserveClaimAttachment(value));
            attachmentFuncs.Add(MonetarySystemReserveIncrease.AttachmentName, value => new MonetarySystemReserveIncrease(value));

            AttachmentFuncs = new ReadOnlyDictionary<string, Func<IReadOnlyDictionary<string, object>, Attachment>>(attachmentFuncs);
        }

        internal AttachmentConverter(JObject attachments)
        {
            _attachments = attachments;
            _attachmentsDictionary = GetDictionaryFromAttachments(_attachments);
        }

        internal PublicKeyAnnouncement GetPublicKeyAnnouncement()
        {
            if (_attachments == null || !_attachmentsDictionary.ContainsKey(PublicKeyAnnouncement.AttachmentName))
            {
                return null;
            }
            return new PublicKeyAnnouncement(_attachmentsDictionary);
        }

        internal EncryptToSelfMessage GetEncryptToSelfMessage()
        {
            if (_attachments == null || !_attachmentsDictionary.ContainsKey(EncryptToSelfMessage.AttachmentName))
            {
                return null;
            }
            return new EncryptToSelfMessage(_attachmentsDictionary);
        }

        internal EncryptedMessage GetEncryptedMessage()
        {
            if (_attachments == null || !_attachmentsDictionary.ContainsKey(EncryptedMessage.AttachmentName))
            {
                return null;
            }
            return new EncryptedMessage(_attachmentsDictionary);
        }

        internal UnencryptedMessage GetMessage()
        {
            if (_attachments == null || !_attachmentsDictionary.ContainsKey(UnencryptedMessage.AttachmentName))
            {
                return null;
            }
            return new UnencryptedMessage(_attachmentsDictionary);
        }

        internal Attachment GetAttachment()
        {
            if (_attachments == null)
            {
                return null;
            }

            foreach (var key in _attachmentsDictionary.Keys)
            {
                Func<IReadOnlyDictionary<string, object>, Attachment> func;
                if (AttachmentFuncs.TryGetValue(key, out func))
                {
                    return func.Invoke(_attachmentsDictionary);
                }
            }

            return null;
        }

        private static Dictionary<string, object> GetDictionaryFromAttachments(JToken attachments)
        {
            if (attachments == null)
            {
                return null;
            }
            var dictionary = new Dictionary<string, object>();
            foreach (var child in attachments.Children<JProperty>())
            {
                var jValue = child.Value as JValue;
                dictionary.Add(child.Name, jValue != null ? jValue.Value : GetDictionaryFromAttachments(child.Value));
            }

            return dictionary;
        }
    }
}