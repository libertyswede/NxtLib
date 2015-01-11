using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NxtLib.Internal
{
    internal class AttachmentConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var values = new Dictionary<string, object>();
                while (reader.Read() && reader.Value != null)
                {
                    var key = reader.Value.ToString();
                    if (reader.Read() && reader.TokenType != JsonToken.StartObject)
                    {
                        values.Add(key, reader.Value);
                    }
                    else if (reader.TokenType == JsonToken.StartObject)
                    {
                        values.Add(key, ReadJson(reader, typeof(object), existingValue, serializer));
                    }
                }
                if (objectType == typeof (List<Attachment>))
                {
                    var attachment = ParseAttachments(values);
                    return attachment;
                }
                return values;
            }
            throw new NotSupportedException("Cannot convert non attachment objects");
        }

        // TODO: Add following:
        // MessagingPollCreation
        // MessagingVoteCasting
        // MessagingHubAnnouncement
        // ColoredCoinsDividendPayment
        private static List<Attachment> ParseAttachments(Dictionary<string, object> values)
        {
            var attachments = new List<Attachment>();
            var unhandledKeys = new List<string>();

            foreach (var key in values.Keys)
            {
                switch (key)
                {
                    case AccountControlEffectiveBalanceLeasingAttachment.AttachmentName:
                        attachments.Add(new AccountControlEffectiveBalanceLeasingAttachment(values));
                        break;
                    case ColoredCoinsAskOrderCancellationAttachment.AttachmentName:
                        attachments.Add(new ColoredCoinsAskOrderCancellationAttachment(values));
                        break;
                    case ColoredCoinsAskOrderPlacementAttachment.AttachmentName:
                        attachments.Add(new ColoredCoinsAskOrderPlacementAttachment(values));
                        break;
                    case ColoredCoinsAssetIssuanceAttachment.AttachmentName:
                        attachments.Add(new ColoredCoinsAssetIssuanceAttachment(values));
                        break;
                    case ColoredCoinsAssetTransferAttachment.AttachmentName:
                        attachments.Add(new ColoredCoinsAssetTransferAttachment(values));
                        break;
                    case ColoredCoinsBidOrderCancellationAttachment.AttachmentName:
                        attachments.Add(new ColoredCoinsBidOrderCancellationAttachment(values));
                        break;
                    case ColoredCoinsBidOrderPlacementAttachment.AttachmentName:
                        attachments.Add(new ColoredCoinsBidOrderPlacementAttachment(values));
                        break;
                    case DigitalGoodsDelistingAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsDelistingAttachment(values));
                        break;
                    case DigitalGoodsDeliveryAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsDeliveryAttachment(values));
                        break;
                    case ColoredCoinsDividendPaymentAttachment.AttachmentName:
                        attachments.Add(new ColoredCoinsDividendPaymentAttachment(values));
                        break;
                    case DigitalGoodsFeedbackAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsFeedbackAttachment(values));
                        break;
                    case DigitalGoodsListingAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsListingAttachment(values));
                        break;
                    case DigitalGoodsPriceChangeAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsPriceChangeAttachment(values));
                        break;
                    case DigitalGoodsPurchaseAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsPurchaseAttachment(values));
                        break;
                    case DigitalGoodsQuantityChangeAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsQuantityChangeAttachment(values));
                        break;
                    case DigitalGoodsRefundAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsRefundAttachment(values));
                        break;
                    case EncryptedMessageAttachment.AttachmentName:
                        attachments.Add(new EncryptedMessageAttachment(values));
                        break;
                    case EncryptToSelfMessageAttachment.AttachmentName:
                        attachments.Add(new EncryptToSelfMessageAttachment(values));
                        break;
                    case MessageAttachment.AttachmentName:
                        attachments.Add(new MessageAttachment(values));
                        break;
                    case MessagingAccountInfoAttachment.AttachmentName:
                        attachments.Add(new MessagingAccountInfoAttachment(values));
                        break;
                    case MessagingAliasAssignmentAttachment.AttachmentName:
                        attachments.Add(new MessagingAliasAssignmentAttachment(values));
                        break;
                    case MessagingAliasBuyAttachment.AttachmentName:
                        attachments.Add(new MessagingAliasBuyAttachment(values));
                        break;
                    case MessagingAliasDeleteAttachment.AttachmentName:
                        attachments.Add(new MessagingAliasDeleteAttachment(values));
                        break;
                    case MessagingAliasSellAttachment.AttachmentName:
                        attachments.Add(new MessagingAliasSellAttachment(values));
                        break;
                    case MonetarySystemExchangeBuyAttachment.AttachmentName:
                        attachments.Add(new MonetarySystemExchangeBuyAttachment(values));
                        break;
                    case MonetarySystemExchangeSellAttachment.AttachmentName:
                        attachments.Add(new MonetarySystemExchangeSellAttachment(values));
                        break;
                    case MonetarySystemCurrencyIssuanceAttachment.AttachmentName:
                        attachments.Add(new MonetarySystemCurrencyIssuanceAttachment(values));
                        break;
                    case MonetarySystemCurrencyMintingAttachment.AttachmentName:
                        attachments.Add(new MonetarySystemCurrencyMintingAttachment(values));
                        break;
                    case MonetarySystemCurrencyTransferAttachment.AttachmentName:
                        attachments.Add(new MonetarySystemCurrencyTransferAttachment(values));
                        break;
                    case MonetarySystemPublishExchangeOfferAttachment.AttachmentName:
                        attachments.Add(new MonetarySystemPublishExchangeOfferAttachment(values));
                        break;
                    case MonetarySystemReserveClaimAttachment.AttachmentName:
                        attachments.Add(new MonetarySystemReserveClaimAttachment(values));
                        break;
                    case MonetarySystemReserveIncrease.AttachmentName:
                        attachments.Add(new MonetarySystemReserveIncrease(values));
                        break;
                    case PublicKeyAnnouncementAttachment.AttachmentName:
                        attachments.Add(new PublicKeyAnnouncementAttachment(values));
                        break;
                    default: 
                        unhandledKeys.Add(key);
                        break;
                }
            }

            if (unhandledKeys.Any(k => k.StartsWith("version.")))
            {

                throw new NotSupportedException("Could not find correct attachment to convert to: " + unhandledKeys.First(k => k.StartsWith("version.")));
            }

            return attachments;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }

    
}