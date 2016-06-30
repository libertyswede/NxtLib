using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.VotingSystem;
using System.IO;
using NxtLib.Local;

namespace NxtLib
{
    public abstract class Appendix
    {
        internal readonly byte version;

        protected Appendix()
        {
        }

        protected Appendix(BinaryReader reader, byte transactionVersion)
        {
            if (transactionVersion == 0)
            {
                version = 0;
            }
            else
            {
                version = reader.ReadByte();
            }
        }

        protected static T GetAttachmentValue<T>(JToken attachments, string key)
        {
            var obj = ((JValue)attachments.SelectToken(key)).Value;
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }

    public class Message : Appendix
    {
        public BinaryHexString Data { get; set; }
        public string MessageText { get; }
        public bool IsPrunable { get; }
        public bool IsText { get; }

        private Message(string message, bool isText, bool isPrunable)
        {
            IsText = isText;
            MessageText = message;
            IsPrunable = isPrunable;

            Data = IsText
                ? Encoding.UTF8.GetBytes(message)
                : ByteToHexStringConverter.ToBytesFromHexString(message).ToArray();
        }

        internal Message(BinaryReader reader, byte transactionVersion) : base(reader, transactionVersion)
        {
            var messageLength = reader.ReadInt32();
            IsText = messageLength < 0;
            if (messageLength < 0)
            {
                messageLength &= int.MaxValue;
            }
            if (messageLength > 1000)
            {
                throw new ValidationException("Invalid arbitrary message length: " + messageLength);
            }
            Data = reader.ReadBytes(messageLength);
            MessageText = Encoding.UTF8.GetString(Data.ToBytes().ToArray(), 0, messageLength);
            IsPrunable = false;
        }

        internal static Message ParseJson(JObject jObject)
        {
            JValue messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(Parameters.Message) as JValue) == null)
            {
                return null;
            }

            var messageIsText = Convert.ToBoolean(jObject.SelectToken(Parameters.MessageIsText).ToString());
            var isPrunable = jObject.Property(Parameters.VersionPrunablePlainMessage) != null;
            return new Message(messageToken.Value.ToString(), messageIsText, isPrunable);
        }
    }

    public abstract class EncryptedMessageBase : Appendix
    {
        public bool IsCompressed { get; set; }
        public bool IsText { get; set; }
        public string MessageToEncrypt { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Nonce { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public BinaryHexString Data { get; set; }

        protected EncryptedMessageBase(JToken messageToken)
        {
            IsCompressed = Convert.ToBoolean(((JValue) messageToken.SelectToken(Parameters.IsCompressed)).Value.ToString());
            IsText = Convert.ToBoolean(((JValue) messageToken.SelectToken(Parameters.IsText)).Value.ToString());
            if (messageToken.SelectToken(Parameters.MessageToEncrypt) != null)
            {
                MessageToEncrypt = ((JValue) messageToken.SelectToken(Parameters.MessageToEncrypt)).Value.ToString();
            }
            if (messageToken.SelectToken(Parameters.Nonce) != null)
            {
                Nonce = ((JValue) messageToken.SelectToken(Parameters.Nonce)).Value.ToString();
                Data = ((JValue) messageToken.SelectToken(Parameters.Data)).Value.ToString();
            }
        }

        protected EncryptedMessageBase()
        {
        }

        public EncryptedMessageBase(BinaryReader reader, byte transactionVersion) : base(reader, transactionVersion)
        {
            var length = reader.ReadInt32();
            IsText = length < 0;
            if (length < 0)
            {
                length &= int.MaxValue;
            }
            Data = reader.ReadBytes(length);
            Nonce = reader.ReadBytes(32);
            IsCompressed = version != 2;
        }
    }

    public class EncryptedMessage : EncryptedMessageBase
    {
        public bool IsPrunable { get; }
        public BinaryHexString EncryptedMessageHash { get; set; }

        private EncryptedMessage(JToken messageToken, bool isPrunable, string encryptedMessageHash)
            : base(messageToken)
        {
            IsPrunable = isPrunable;
            EncryptedMessageHash = encryptedMessageHash;
        }

        public EncryptedMessage()
        {
        }

        public EncryptedMessage(BinaryReader reader, byte transactionVersion) : base(reader, transactionVersion)
        {
        }

        internal static EncryptedMessage ParseJson(JObject jObject)
        {
            JToken messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(Parameters.EncryptedMessage)) == null)
            {
                return null;
            }
            var isPrunable = jObject.Property(Parameters.VersionPrunableEncryptedMessage) != null;
            var hash = jObject.SelectToken(Parameters.VersionPrunableEncryptedMessage)?.ToString();
            return new EncryptedMessage(messageToken, isPrunable, hash);
        }
    }

    public class EncryptToSelfMessage : EncryptedMessageBase
    {
        public EncryptToSelfMessage(BinaryReader reader, byte transactionVersion) : base(reader, transactionVersion)
        {
        }

        private EncryptToSelfMessage(JToken messageToken)
            : base(messageToken)
        {
        }

        internal static EncryptToSelfMessage ParseJson(JObject jObject)
        {
            JToken messageToken;
            if (jObject == null || (messageToken = jObject.SelectToken(Parameters.EncryptToSelfMessage)) == null)
            {
                return null;
            }
            return new EncryptToSelfMessage(messageToken);
        }
    }

    public class PublicKeyAnnouncement : Appendix
    {
        public BinaryHexString RecipientPublicKey { get; set; }

        private PublicKeyAnnouncement(BinaryHexString recipientPublicKey)
        {
            RecipientPublicKey = recipientPublicKey;
        }

        public PublicKeyAnnouncement(BinaryReader reader, byte transactionVersion) : base (reader, transactionVersion)
        {
            RecipientPublicKey = reader.ReadBytes(32);
        }

        internal static PublicKeyAnnouncement ParseJson(JObject jObject)
        {
            JValue announcement;
            if (jObject == null || (announcement = jObject.SelectToken(Parameters.RecipientPublicKey) as JValue) == null)
            {
                return null;
            }
            return new PublicKeyAnnouncement(announcement.Value.ToString());
        }
    }

    public class TransactionPhasing : Appendix
    {
        public int FinishHeight { get; set; }
        public BinaryHexString HashedSecret { get; set; }
        public HashAlgorithm? HashedSecretAlgorithm { get; set; }
        public ulong HoldingId { get; set; }
        public List<BinaryHexString> LinkedFullHashes { get; set; }
        public long MinBalance { get; set; }
        public MinBalanceModel MinBalanceModel { get; set; }
        public long Quorum { get; set; }
        public VotingModel VotingModel { get; set; }
        public List<Account> WhiteList { get; set; }

        private TransactionPhasing()
        {
            WhiteList = new List<Account>();
            LinkedFullHashes = new List<BinaryHexString>();
        }

        public TransactionPhasing(BinaryReader reader, byte transactionVersion) : base(reader, transactionVersion)
        {
            WhiteList = new List<Account>();
            LinkedFullHashes = new List<BinaryHexString>();

            FinishHeight = reader.ReadInt32();
            var votingModelValue = (int)reader.ReadByte();
            VotingModel = (VotingModel)votingModelValue;
            Quorum = reader.ReadInt64();
            MinBalance = reader.ReadInt64();
            var whitelistSize = reader.ReadByte();
            for (int i = 0; i < whitelistSize; i++)
            {
                WhiteList.Add(reader.ReadUInt64());
            }

            HoldingId = (ulong)reader.ReadInt64();
            MinBalanceModel = (MinBalanceModel)reader.ReadByte();
            var linkedFullHashesSize = reader.ReadByte();
            for (int i = 0; i < linkedFullHashesSize; i++)
            {
                LinkedFullHashes.Add(reader.ReadBytes(32));
            }

            var hashedSecredLength = reader.ReadByte();
            if (hashedSecredLength > 0)
            {
                HashedSecret = reader.ReadBytes(hashedSecredLength);
            }
            var algorithmValue = (int)reader.ReadByte();
            if (Enum.IsDefined(typeof(HashAlgorithm), algorithmValue))
            {
                HashedSecretAlgorithm = (HashAlgorithm)algorithmValue;
            }
        }

        internal static TransactionPhasing ParseJson(JObject jObject)
        {
            if (jObject?.SelectToken(Parameters.PhasingFinishHeight) == null)
            {
                return null;
            }

            var phasing = new TransactionPhasing
            {
                FinishHeight = GetAttachmentValue<int>(jObject, Parameters.PhasingFinishHeight),
                HoldingId = ulong.Parse(GetAttachmentValue<string>(jObject, Parameters.PhasingHolding)),
                MinBalance = long.Parse(GetAttachmentValue<string>(jObject, Parameters.PhasingMinBalance)),
                MinBalanceModel = (MinBalanceModel)GetAttachmentValue<int>(jObject, Parameters.PhasingMinBalanceModel),
                Quorum = GetAttachmentValue<long>(jObject, Parameters.PhasingQuorum),
                VotingModel = (VotingModel)GetAttachmentValue<int>(jObject, Parameters.PhasingVotingModel)
            };

            if (jObject.SelectToken(Parameters.PhasingWhitelist) != null)
            {
                phasing.WhiteList = ParseWhitelist(jObject.SelectToken(Parameters.PhasingWhitelist))
                    .Select(w => new Account(ulong.Parse(w)))
                    .ToList();
            }
            if (jObject.SelectToken(Parameters.PhasingHashedSecret) != null)
            {
                phasing.HashedSecret = new BinaryHexString(GetAttachmentValue<string>(jObject, Parameters.PhasingHashedSecret));
                phasing.HashedSecretAlgorithm = (HashAlgorithm)GetAttachmentValue<int>(jObject, Parameters.PhasingHashedSecretAlgorithm);
            }
            if (jObject.SelectToken(Parameters.PhasingLinkedFullHashes) != null)
            {
                phasing.LinkedFullHashes =
                    ParseWhitelist(jObject.SelectToken(Parameters.PhasingLinkedFullHashes))
                        .Select(s => new BinaryHexString(s))
                        .ToList();
            }

            return phasing;
        }


        private static IEnumerable<string> ParseWhitelist(JToken whitelistToken)
        {
            return whitelistToken.Children<JValue>().Select(optionToken => optionToken.Value.ToString());
        }
    }
}
