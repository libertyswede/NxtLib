using System.Collections.Generic;

namespace NxtLib
{
    public class CreateTransactionBySecretPhrase : CreateTransactionParameters
    {
        public string SecretPhrase { get; set; }

        public CreateTransactionBySecretPhrase(bool broadcast, short deadline, Amount fee, string secretPhrase)
            : base(broadcast, deadline, fee)
        {
            SecretPhrase = secretPhrase;
        }

        internal override void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            base.AppendToQueryParameters(queryParameters);
            queryParameters.Add("secretPhrase", SecretPhrase);
        }
    }
}