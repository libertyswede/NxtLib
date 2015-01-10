using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    public class Transaction
    {
        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "amountNqt")]
        public Amount Amount { get; set; }

        [JsonConverter(typeof(AttachmentConverter))]
        [JsonProperty(PropertyName = "attachment")]
        public List<Attachment> Attachments { get; set; }
        public ulong? Block { get; set; }
        public int? BlockTimestamp { get; set; }
        public int? Confirmations { get; set; }
        public short Deadline { get; set; }
        public ulong EcBlockId { get; set; }
        public int EcBlockHeight { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "feeNqt")]
        public Amount FeeNqt { get; set; }
        public string FullHash { get; set; }
        public int Height { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong? Recipient { get; set; }
        public string RecipientRs { get; set; }
        public string ReferencedTransactionFullHash { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Sender { get; set; }
        public string SenderRs { get; set; }
        public string SenderPublicKey { get; set; }
        public string Signature { get; set; }
        public string SignatureHash { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public byte SubType { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "transaction")]
        public ulong? TransactionId { get; set; }
        public int TransactionIndex { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public byte Type { get; set; }
        public int Version { get; set; }
    }
}