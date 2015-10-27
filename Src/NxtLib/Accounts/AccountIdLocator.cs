using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountIdLocator : LocatorBase
    {
        public readonly string PublicKey;
        public readonly string SecretPhrase;

        private AccountIdLocator(string key, string value) : base(key, value)
        {
            if (key.Equals(Parameters.PublicKey))
            {
                PublicKey = value;
            }
            else if (key.Equals(Parameters.SecretPhrase))
            {
                SecretPhrase = value;
            }
        }

        public static AccountIdLocator ByPublicKey(string publicKey)
        {
            return new AccountIdLocator(Parameters.PublicKey, publicKey);
        }

        public static AccountIdLocator BySecretPhrase(string secretPhrase)
        {
            return new AccountIdLocator(Parameters.SecretPhrase, secretPhrase);
        }
    }
}