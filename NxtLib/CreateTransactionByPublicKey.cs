using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib
{
    public class CreateTransactionByPublicKey : CreateTransactionParameters
    {
        public string PublicKey { get; set; }

        public CreateTransactionByPublicKey(bool broadcast, short deadline, Amount fee, string publicKey)
            : base(broadcast, deadline, fee)
        {
            PublicKey = publicKey;
        }

        internal override void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            base.AppendToQueryParameters(queryParameters);
            queryParameters.AddIfHasValue("publicKey", PublicKey);
        }
    }
}