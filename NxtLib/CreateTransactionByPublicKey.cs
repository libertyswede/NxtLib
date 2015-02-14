using System.Collections.Generic;
using NxtLib.Internal;

namespace NxtLib
{
    public class CreateTransactionByPublicKey : CreateTransactionParameters
    {
        public BinaryHexString PublicKey { get; set; }

        public CreateTransactionByPublicKey(short deadline, Amount fee, BinaryHexString publicKey)
            : base(false, deadline, fee)
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