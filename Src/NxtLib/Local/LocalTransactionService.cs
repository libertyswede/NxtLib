using System.Linq;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.Internal.LocalSign;
using System.IO;
using static NxtLib.CreateTransactionParameters;
using System.Text;
using System.Security.Cryptography;

namespace NxtLib.Local
{
    public interface ILocalTransactionService
    {
        JObject SignTransaction(TransactionCreatedReply transactionCreatedReply, string secretPhrase);
        void VerifySendMoneyTransactionBytes(TransactionCreatedReply transactionCreatedReply, CreateTransactionByPublicKey parameters,
            Account recipient, Amount amount);
        void VerifySendMessageTransactionBytes(TransactionCreatedReply transactionCreatedReply, CreateTransactionByPublicKey parameters,
            Account recipient);
    }

    public class LocalTransactionService : ILocalTransactionService
    {
        private readonly Crypto _crypto = new Crypto();

        public JObject SignTransaction(TransactionCreatedReply transactionCreatedReply, string secretPhrase)
        {
            var transaction = transactionCreatedReply.Transaction;
            var unsignedBytes = transactionCreatedReply.UnsignedTransactionBytes.ToBytes().ToArray();
            var referencedTransactionFullHash = transaction.ReferencedTransactionFullHash != null
                ? transaction.ReferencedTransactionFullHash.ToHexString()
                : "";
            var attachment = JObject.Parse(transactionCreatedReply.RawJsonReply).SelectToken($"{Parameters.TransactionJson}['{Parameters.Attachment}']");
            var signature = new BinaryHexString(_crypto.Sign(unsignedBytes, secretPhrase));
            return BuildSignedTransaction(transaction, referencedTransactionFullHash, signature, attachment);
        }

        public void VerifySendMoneyTransactionBytes(TransactionCreatedReply transactionCreatedReply, CreateTransactionByPublicKey parameters, 
            Account recipient, Amount amount)
        {
            var byteArray = transactionCreatedReply.UnsignedTransactionBytes.ToBytes().ToArray();
            using (var stream = new MemoryStream(byteArray))
            using (var reader = new BinaryReader(stream))
            {
                var transaction = VerifyCommonProperties(reader, parameters, recipient, amount, TransactionSubType.PaymentOrdinaryPayment);

                if (transaction.SubType != TransactionSubType.PaymentOrdinaryPayment)
                {
                    throw new ValidationException(nameof(transaction.SubType), TransactionSubType.PaymentOrdinaryPayment, transaction.SubType);
                }
            }
        }

        public void VerifySendMessageTransactionBytes(TransactionCreatedReply transactionCreatedReply, CreateTransactionByPublicKey parameters,
            Account recipient)
        {
            var byteArray = transactionCreatedReply.UnsignedTransactionBytes.ToBytes().ToArray();
            using (var stream = new MemoryStream(byteArray))
            using (var reader = new BinaryReader(stream))
            {
                var transaction = VerifyCommonProperties(reader, parameters, recipient, Amount.Zero, TransactionSubType.MessagingArbitraryMessage);

                if (transaction.SubType != TransactionSubType.MessagingArbitraryMessage)
                {
                    throw new ValidationException(nameof(transaction.SubType), TransactionSubType.MessagingArbitraryMessage, transaction.SubType);
                }
            }
        }

        private static Transaction VerifyCommonProperties(BinaryReader reader, CreateTransactionByPublicKey parameters,
            Account recipient, Amount amount, TransactionSubType transactionType)
        {
            var transaction = new Transaction();
            var type = reader.ReadByte(); // 1
            var subtype = reader.ReadByte(); // 2
            transaction.SubType = TransactionTypeMapper.GetSubType(type, (byte)(subtype & 0x0F));
            transaction.Version = (subtype & 0xF0) >> 4;
            transaction.Timestamp = new DateTimeConverter().GetFromNxtTime(reader.ReadInt32()); // 6
            transaction.Deadline = reader.ReadInt16(); // 8
            transaction.SenderPublicKey = new BinaryHexString(reader.ReadBytes(32)); // 40
            transaction.Recipient = reader.ReadUInt64(); // 48
            transaction.Amount = Amount.CreateAmountFromNqt(reader.ReadInt64()); // 56
            transaction.Fee = Amount.CreateAmountFromNqt(reader.ReadInt64()); // 64
            transaction.ReferencedTransactionFullHash = new BinaryHexString(reader.ReadBytes(32)); // 96

            if (transaction.ReferencedTransactionFullHash.ToBytes().All(b => b == 0))
            {
                transaction.ReferencedTransactionFullHash = "";
            }

            reader.ReadBytes(64); // signature, 160

            var flags = 0;

            if (transaction.Version > 0)
            {
                flags = reader.ReadInt32(); // 164
                transaction.EcBlockHeight = reader.ReadInt32(); // 168
                transaction.EcBlockId = reader.ReadUInt64(); // 176
            }

            if (!transaction.SenderPublicKey.Equals(parameters.PublicKey))
            {
                throw new ValidationException(nameof(transaction.SenderPublicKey), parameters.PublicKey, transaction.SenderPublicKey);
            }

            if (parameters.Deadline != transaction.Deadline)
            {
                throw new ValidationException(nameof(transaction.Deadline), parameters.Deadline, transaction.Deadline);
            }

            if ((recipient?.AccountId ?? 0) != (transaction?.Recipient ?? 0))
            {
                throw new ValidationException(nameof(transaction.Recipient), recipient?.AccountId, transaction.Recipient);
            }

            if (!amount.Equals(transaction.Amount))
            {
                throw new ValidationException(nameof(transaction.Amount), amount, transaction.Amount);
            }

            if (parameters.ReferencedTransactionFullHash != null)
            {
                if (!parameters.ReferencedTransactionFullHash.Equals(transaction.ReferencedTransactionFullHash))
                {
                    throw new ValidationException(nameof(transaction.ReferencedTransactionFullHash), parameters.ReferencedTransactionFullHash, transaction.ReferencedTransactionFullHash);
                }
            }
            else if (transaction.ReferencedTransactionFullHash.ToHexString() != "")
            {
                throw new ValidationException(nameof(transaction.ReferencedTransactionFullHash), parameters.ReferencedTransactionFullHash, transaction.ReferencedTransactionFullHash);
            }

            var attachmentConverter = new AttachmentConverter(reader, transaction.Version);
            transaction.Attachment = attachmentConverter.GetAttachment(transactionType);

            var position = 1;
            if ((flags & position) != 0 || (transaction.Version == 0 && transactionType == TransactionSubType.MessagingArbitraryMessage))
            {
                transaction.Message = new Message(reader, (byte)transaction.Version);

                if (parameters.Message.MessageIsText != transaction.Message.IsText)
                {
                    throw new ValidationException(nameof(transaction.Message.IsText), parameters.Message.MessageIsText, transaction.Message.IsText);
                }
                if (parameters.Message.MessageIsText && parameters.Message.Message != transaction.Message.MessageText)
                {
                    throw new ValidationException(nameof(transaction.Message.MessageText), parameters.Message.Message, transaction.Message.MessageText);
                }
                if (!parameters.Message.MessageIsText && !transaction.Message.Data.Equals(parameters.Message.Message))
                {
                    throw new ValidationException(nameof(transaction.Message.Data), parameters.Message.Message, transaction.Message.Data);
                }
            }
            else if (parameters.Message != null && !parameters.Message.IsPrunable)
            {
                throw new ValidationException("Expected a message, but got null");
            }

            position <<= 1;
            if ((flags & position) != 0)
            {
                transaction.EncryptedMessage = new EncryptedMessage(reader, (byte)transaction.Version);
                var encryptedMessage = parameters.EncryptedMessage;

                if (encryptedMessage.MessageIsText != transaction.EncryptedMessage.IsText)
                {
                    throw new ValidationException(nameof(transaction.EncryptedMessage.IsText), encryptedMessage.MessageIsText, transaction.EncryptedMessage.IsText);
                }
                if (encryptedMessage.CompressMessage != transaction.EncryptedMessage.IsCompressed)
                {
                    throw new ValidationException(nameof(transaction.EncryptedMessage.IsCompressed), encryptedMessage.CompressMessage, transaction.EncryptedMessage.IsCompressed);
                }
                if (!encryptedMessage.Message.Equals(transaction.EncryptedMessage.Data))
                {
                    throw new ValidationException(nameof(transaction.EncryptedMessage.MessageToEncrypt), encryptedMessage.Message, transaction.EncryptedMessage.MessageToEncrypt);
                }
                if (encryptedMessage is AlreadyEncryptedMessage)
                {
                    var alreadyEncryptedMessage = (AlreadyEncryptedMessage)parameters.EncryptedMessage;
                    if (!alreadyEncryptedMessage.Nonce.Equals(transaction.EncryptedMessage.Nonce))
                    {
                        throw new ValidationException(nameof(transaction.EncryptedMessage.Nonce), alreadyEncryptedMessage.Nonce, transaction.EncryptedMessage.Nonce);
                    }
                }
            }
            else if (parameters.EncryptedMessage != null && !parameters.EncryptedMessage.IsPrunable)
            {
                throw new ValidationException("Expected an encrypted message, but got null");
            }

            position <<= 1;
            if ((flags & position) != 0)
            {
                transaction.PublicKeyAnnouncement = new PublicKeyAnnouncement(reader, (byte)transaction.Version);
                if (parameters.RecipientPublicKey != null && !parameters.RecipientPublicKey.Equals(transaction.PublicKeyAnnouncement.RecipientPublicKey))
                {
                    throw new ValidationException(nameof(transaction.PublicKeyAnnouncement.RecipientPublicKey), parameters.RecipientPublicKey, transaction.PublicKeyAnnouncement.RecipientPublicKey);
                }
            }

            position <<= 1;
            if ((flags & position) != 0)
            {
                transaction.EncryptToSelfMessage = new EncryptToSelfMessage(reader, (byte)transaction.Version);
                var encryptedMessage = parameters.EncryptedMessageToSelf;

                if (encryptedMessage.MessageIsText != transaction.EncryptToSelfMessage.IsText)
                {
                    throw new ValidationException(nameof(transaction.EncryptToSelfMessage.IsText), encryptedMessage.MessageIsText, transaction.EncryptToSelfMessage.IsText);
                }
                if (encryptedMessage.CompressMessage != transaction.EncryptToSelfMessage.IsCompressed)
                {
                    throw new ValidationException(nameof(transaction.EncryptToSelfMessage.IsCompressed), encryptedMessage.CompressMessage, transaction.EncryptToSelfMessage.IsCompressed);
                }
                if (!encryptedMessage.Message.Equals(transaction.EncryptToSelfMessage.Data))
                {
                    throw new ValidationException(nameof(transaction.EncryptToSelfMessage.MessageToEncrypt), encryptedMessage.Message, transaction.EncryptToSelfMessage.MessageToEncrypt);
                }
                if (encryptedMessage is AlreadyEncryptedMessageToSelf)
                {
                    var alreadyEncryptedMessage = (AlreadyEncryptedMessageToSelf)parameters.EncryptedMessageToSelf;
                    if (!alreadyEncryptedMessage.Nonce.Equals(transaction.EncryptToSelfMessage.Nonce))
                    {
                        throw new ValidationException(nameof(transaction.EncryptToSelfMessage.Nonce), alreadyEncryptedMessage.Nonce, transaction.EncryptToSelfMessage.Nonce);
                    }
                }
            }
            else if (parameters.EncryptedMessageToSelf != null)
            {
                throw new ValidationException("Expected an encrypted to self message, but got null");
            }

            position <<= 1;
            if ((flags & position) != 0)
            {
                transaction.Phasing = new TransactionPhasing(reader, (byte)transaction.Version);

                if (parameters.Phasing.FinishHeight != transaction.Phasing.FinishHeight)
                {
                    throw new ValidationException(nameof(transaction.Phasing.FinishHeight), parameters.Phasing.FinishHeight, transaction.Phasing.FinishHeight);
                }
                if (parameters.Phasing.HashedSecret != null && !parameters.Phasing.HashedSecret.Equals(transaction.Phasing.HashedSecret))
                {
                    throw new ValidationException(nameof(transaction.Phasing.HashedSecret), parameters.Phasing.HashedSecret, transaction.Phasing.HashedSecret);
                }
                if (parameters.Phasing.HashedSecretAlgorithm != transaction.Phasing.HashedSecretAlgorithm)
                {
                    throw new ValidationException(nameof(transaction.Phasing.HashedSecretAlgorithm), parameters.Phasing.HashedSecretAlgorithm, transaction.Phasing.HashedSecretAlgorithm);
                }
                if (parameters.Phasing.HoldingId != transaction.Phasing.HoldingId)
                {
                    throw new ValidationException(nameof(transaction.Phasing.HoldingId), parameters.Phasing.HoldingId, transaction.Phasing.HoldingId);
                }
                if (!Enumerable.SequenceEqual(parameters.Phasing.LinkedFullHash, transaction.Phasing.LinkedFullHashes))
                {
                    throw new ValidationException(nameof(transaction.Phasing.LinkedFullHashes), parameters.Phasing.LinkedFullHash, transaction.Phasing.LinkedFullHashes);
                }
                if (parameters.Phasing.MinBalance != transaction.Phasing.MinBalance)
                {
                    throw new ValidationException(nameof(transaction.Phasing.MinBalance), parameters.Phasing.MinBalance, transaction.Phasing.MinBalance);
                }
                if (parameters.Phasing.MinBalanceModel != transaction.Phasing.MinBalanceModel)
                {
                    throw new ValidationException(nameof(transaction.Phasing.MinBalanceModel), parameters.Phasing.MinBalanceModel, transaction.Phasing.MinBalanceModel);
                }
                //if (parameters.Phasing.Phased != transaction.Phased)
                //{
                //    throw new ValidationException(nameof(transaction.Phased), parameters.Phasing.Phased, transaction.Phased);
                //}
                if (parameters.Phasing.Quorum != transaction.Phasing.Quorum)
                {
                    throw new ValidationException(nameof(transaction.Phasing.Quorum), parameters.Phasing.Quorum, transaction.Phasing.Quorum);
                }
                if (parameters.Phasing.VotingModel != transaction.Phasing.VotingModel)
                {
                    throw new ValidationException(nameof(transaction.Phasing.VotingModel), parameters.Phasing.VotingModel, transaction.Phasing.VotingModel);
                }
                if (!Enumerable.SequenceEqual(parameters.Phasing.WhiteListed, transaction.Phasing.WhiteList))
                {
                    throw new ValidationException(nameof(transaction.Phasing.WhiteList), parameters.Phasing.WhiteListed, transaction.Phasing.WhiteList);
                }
            }
            else if (parameters.Phasing != null)
            {
                throw new ValidationException("Expected phasing, but got null");
            }

            position <<= 1;
            if ((flags & position) != 0)
            {
                transaction.Message = new Message(reader, (byte)transaction.Version, true);
                var expectedHash = HashPrunableMessage(parameters.Message.MessageIsText, parameters.Message.Message);

                if (!expectedHash.Equals(transaction.Message.MessageHash))
                {
                    throw new ValidationException(nameof(transaction.Message.MessageHash), expectedHash, transaction.Message.MessageHash);
                }
            }
            else if (parameters.Message != null && parameters.Message.IsPrunable)
            {
                throw new ValidationException("Expected prunable message, but got null");
            }

            position <<= 1;
            if ((flags & position) != 0)
            {
                var alreadyEncrypted = (AlreadyEncryptedMessage)parameters.EncryptedMessage;
                transaction.EncryptedMessage = new EncryptedMessage(reader, (byte)transaction.Version, true);

                var isText = new byte[] { (byte)(alreadyEncrypted.MessageIsText ? 1 : 0) };
                var isCompressed = new byte[] { (byte)(alreadyEncrypted.CompressMessage ? 1 : 0) };
                var messageBytes = alreadyEncrypted.Message.ToBytes().ToArray();
                var nonceBytes = alreadyEncrypted.Nonce.ToBytes().ToArray();

                var expectedHash = HashPrunableMessage(isText, isCompressed, messageBytes, nonceBytes);

                if (!expectedHash.Equals(transaction.EncryptedMessage.EncryptedMessageHash))
                {
                    throw new ValidationException(nameof(transaction.EncryptedMessage.EncryptedMessageHash), expectedHash, transaction.EncryptedMessage.EncryptedMessageHash);
                }
            }
            else if (parameters.EncryptedMessage != null && parameters.EncryptedMessage.IsPrunable)
            {
                throw new ValidationException("Expected encrypted prunable message, but got null");
            }

            return transaction;
        }

        private static BinaryHexString HashPrunableMessage(params byte[][] messages)
        {
            byte[] hash;

#if (NET40 || NET45)

            using (var sha256 = SHA256.Create())
            {
                for (int i = 0; i < messages.Length - 1; i++)
                {
                    sha256.TransformBlock(messages[i], 0, messages[i].Length, messages[i], 0);
                }
                sha256.TransformFinalBlock(messages[messages.Length - 1], 0, messages[messages.Length - 1].Length);
                hash = sha256.Hash;
            }

#else

            using (var incrementalHash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256))
            {
                foreach (var byteArray in messages)
                {
                    incrementalHash.AppendData(byteArray);
                }
                hash = incrementalHash.GetHashAndReset();
            }

#endif
            var computed = new BinaryHexString(hash);
            return computed;
        }

        private static BinaryHexString HashPrunableMessage(bool isText, string message)
        {
            byte[] isTextByte;
            byte[] messageBytes;

            if (isText)
            {
                isTextByte = new byte[] { 1 };
                messageBytes = Encoding.UTF8.GetBytes(message);
            }
            else
            {
                isTextByte = new byte[] { 0 };
                messageBytes = new BinaryHexString(message).ToBytes().ToArray();
            }

            return HashPrunableMessage(isTextByte, messageBytes);
        }

        private static JObject BuildSignedTransaction(Transaction transaction, string referencedTransactionFullHash,
            BinaryHexString signature, JToken attachment)
        {
            var resultJson = new JObject();
            resultJson.Add(Parameters.Type, (int)TransactionTypeMapper.GetMainTypeByte(transaction.Type));
            resultJson.Add(Parameters.SubType, (int)TransactionTypeMapper.GetSubTypeByte(transaction.SubType));
            resultJson.Add(Parameters.Timestamp, new DateTimeConverter().GetNxtTimestamp(transaction.Timestamp));
            resultJson.Add(Parameters.Deadline, transaction.Deadline);
            resultJson.Add(Parameters.SenderPublicKey, transaction.SenderPublicKey.ToHexString());
            resultJson.Add(Parameters.AmountNqt, transaction.Amount.Nqt.ToString());
            resultJson.Add(Parameters.FeeNqt, transaction.Fee.Nqt.ToString());
            if (!string.IsNullOrEmpty(referencedTransactionFullHash))
            {
                resultJson.Add(Parameters.ReferencedTransactionFullHash, referencedTransactionFullHash);
            }
            resultJson.Add(Parameters.Signature, signature.ToHexString());
            resultJson.Add(Parameters.Version, transaction.Version);
            if (attachment != null && attachment.Children().Any())
            {
                resultJson.Add(Parameters.Attachment, attachment);
            }
            resultJson.Add(Parameters.EcBlockHeight, transaction.EcBlockHeight);
            resultJson.Add(Parameters.EcBlockId, transaction.EcBlockId.ToString());
            if (transaction.Recipient.HasValue)
            {
                resultJson.Add(Parameters.Recipient, transaction.Recipient.ToString());
            }
            return resultJson;
        }
    }
}