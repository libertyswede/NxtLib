namespace NxtLib.Accounts
{
    public class AccountIdLocator : LocatorBase
    {
        public readonly string PublicKey;
        public readonly string SecretPhrase;

        private AccountIdLocator(string key, string value) : base(key, value)
        {
            if (key.Equals("publicKey"))
            {
                PublicKey = value;
            }
            else if (key.Equals("secretPhrase"))
            {
                SecretPhrase = value;
            }
        }

        public static AccountIdLocator ByPublicKey(string publicKey)
        {
            return new AccountIdLocator("publicKey", publicKey);
        }

        public static AccountIdLocator BySecretPhrase(string secretPhrase)
        {
            return new AccountIdLocator("secretPhrase", secretPhrase);
        }
    }
}