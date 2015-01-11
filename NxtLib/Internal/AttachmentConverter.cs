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

        private static List<Attachment> ParseAttachments(Dictionary<string, object> values)
        {
            var attachments = new List<Attachment>();
            var unhandledKeys = new List<string>();

            foreach (var key in values.Keys)
            {
                switch (key)
                {
                    case AccountInfoAttachment.AttachmentName:
                        attachments.Add(new AccountInfoAttachment(values));
                        break;
                    case AliasAssignmentAttachment.AttachmentName:
                        attachments.Add(new AliasAssignmentAttachment(values));
                        break;
                    case AliasBuyAttachment.AttachmentName:
                        attachments.Add(new AliasBuyAttachment(values));
                        break;
                    case AliasDeleteAttachment.AttachmentName:
                        attachments.Add(new AliasDeleteAttachment(values));
                        break;
                    case AliasSellAttachment.AttachmentName:
                        attachments.Add(new AliasSellAttachment(values));
                        break;
                    case AskOrderPlacementAttachment.AttachmentName:
                        attachments.Add(new AskOrderPlacementAttachment(values));
                        break;
                    case AskOrderCancellationAttachment.AttachmentName:
                        attachments.Add(new AskOrderCancellationAttachment(values));
                        break;
                    case AssetIssuanceAttachment.AttachmentName:
                        attachments.Add(new AssetIssuanceAttachment(values));
                        break;
                    case AssetTransferAttachment.AttachmentName:
                        attachments.Add(new AssetTransferAttachment(values));
                        break;
                    case BidOrderCancellationAttachment.AttachmentName:
                        attachments.Add(new BidOrderCancellationAttachment(values));
                        break;
                    case BidOrderPlacementAttachment.AttachmentName:
                        attachments.Add(new BidOrderPlacementAttachment(values));
                        break;
                    case CurrencyIssuanceAttachment.AttachmentName:
                        attachments.Add(new CurrencyIssuanceAttachment(values));
                        break;
                    case CurrencyMintingAttachment.AttachmentName:
                        attachments.Add(new CurrencyMintingAttachment(values));
                        break;
                    case DigitalGoodsDelistingAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsDelistingAttachment(values));
                        break;
                    case DigitalGoodsDeliveryAttachment.AttachmentName:
                        attachments.Add(new DigitalGoodsDeliveryAttachment(values));
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
                    case EffectiveBalanceLeasingAttachment.AttachmentName:
                        attachments.Add(new EffectiveBalanceLeasingAttachment(values));
                        break;
                    case EncryptedMessageAttachment.AttachmentName:
                        attachments.Add(new EncryptedMessageAttachment(values));
                        break;
                    case EncryptToSelfMessageAttachment.AttachmentName:
                        attachments.Add(new EncryptToSelfMessageAttachment(values));
                        break;
                    case ExchangeBuyAttachment.AttachmentName:
                        attachments.Add(new ExchangeBuyAttachment(values));
                        break;
                    case MessageAttachment.AttachmentName:
                        attachments.Add(new MessageAttachment(values));
                        break;
                    case PublicKeyAnnouncementAttachment.AttachmentName:
                        attachments.Add(new PublicKeyAnnouncementAttachment(values));
                        break;
                    case PublishExchangeOfferAttachment.AttachmentName:
                        attachments.Add(new PublishExchangeOfferAttachment(values));
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