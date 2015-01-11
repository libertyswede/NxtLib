using System.Collections.Generic;

namespace NxtLib
{
    public class CreateTransactionBySecretPhrase : CreateTransactionParameters
    {
        public string SecretPhrase { get; set; }

        public CreateTransactionBySecretPhrase(bool broadcast, short deadline, Amount fee, string secretPhrase,
            string recipientPublicKey = null, string referencedTransactionFullHash = null,
            MessagesToSend messagesesToSend = null)
            : base(broadcast, deadline, fee, recipientPublicKey, referencedTransactionFullHash, messagesesToSend)
        {
            SecretPhrase = secretPhrase;
        }

        public override void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            base.AppendToQueryParameters(queryParameters);
            queryParameters.Add("secretPhrase", SecretPhrase);
        }
    }
}