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
        public Amount FeeNqt { get; set; }
        public string FullHash { get; set; }
        public int Height { get; set; }
        public UnencryptedMessage Message { get; set; }
        public ulong? Recipient { get; set; }
        public string RecipientRs { get; set; }
        public string ReferencedTransactionFullHash { get; set; }
        public PublicKeyAnnouncement PublicKeyAnnouncement { get; set; }
        public ulong Sender { get; set; }
        public string SenderRs { get; set; }
        public string SenderPublicKey { get; set; }
        public string Signature { get; set; }
        public string SignatureHash { get; set; }
        public byte SubType { get; set; }
        public DateTime Timestamp { get; set; }
        public ulong? TransactionId { get; set; }
        public int TransactionIndex { get; set; }
        public byte Type { get; set; }
        public int Version { get; set; }
    }
}