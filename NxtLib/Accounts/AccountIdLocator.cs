namespace NxtLib.Accounts
{
    public class AccountIdLocator : LocatorBase
    {
        private AccountIdLocator(string key, string value) : base(key, value)
        {
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