using System.Collections.Generic;
using System.Linq;
using NxtLib.Internal;

namespace NxtLib
{
    public abstract class CreateTransactionParameters
    {
        public bool Broadcast { get; set; }
        public short Deadline { get; set; }
        public Amount Fee { get; set; }
        public string RecipientPublicKey { get; set; }
        public string ReferencedTransactionFullHash { get; set; }
        public MessagesToSend MessagesesToSend { get; set; }

        protected CreateTransactionParameters(bool broadcast, short deadline, Amount fee,
            string recipientPublicKey = null, string referencedTransactionFullHash = null, MessagesToSend messagesesToSend = null)
        {
            Broadcast = broadcast;
            Deadline = deadline;
            Fee = fee;
            RecipientPublicKey = recipientPublicKey;
            ReferencedTransactionFullHash = referencedTransactionFullHash;
            MessagesesToSend = messagesesToSend;
        }

        public virtual void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            queryParameters.Add("broadcast", Broadcast.ToString());
            queryParameters.Add("deadline", Deadline.ToString());
            queryParameters.Add("feeNQT", Fee.Nqt.ToString());
            queryParameters.AddIfHasValue("recipientPublicKey", RecipientPublicKey);
            queryParameters.AddIfHasValue("referencedTransactionFullHash", ReferencedTransactionFullHash);
            if (MessagesesToSend != null)
            {
                MessagesesToSend.QueryParameters.ToList().ForEach(m => queryParameters.Add(m.Key, m.Value));
            }
        }
    }
}