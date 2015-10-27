using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Messages
{
    public class PrunableMessage
    {
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime BlockTimestamp { get; set; }
        public EncryptedData EncryptedMessage { get; set; }
        public bool IsCompressed { get; set; }
        public bool IsText { get; set; }
        public string Message { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "recipient")]
        public ulong RecipientId { get; set; }
        public string RecipientRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "sender")]
        public ulong SenderId { get; set; }
        public string SenderRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Transaction)]
        public ulong TransactionId { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTimestamp { get; set; }

    }
}