using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib
{
    public class CreateTransactionByPublicKey : CreateTransactionParameters
    {
        public string PublicKey { get; set; }

        public CreateTransactionByPublicKey(bool broadcast, short deadline, Amount fee, string publicKey, 
            string recipientPublicKey = null, string referencedTransactionFullHash = null)
            : base(broadcast, deadline, fee, recipientPublicKey, referencedTransactionFullHash)
        {
            PublicKey = publicKey;
        }

        public override void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            base.AppendToQueryParameters(queryParameters);
            queryParameters.AddIfHasValue("publicKey", PublicKey);
        }
    }
}