namespace NxtLib.Forging
{
    public class SecretPhraseOrAdminPassword : LocatorBase
    {
        private SecretPhraseOrAdminPassword(string key, string value) : base(key, value)
        {
        }

        public static SecretPhraseOrAdminPassword BySecretPhrase(string secretPhrase)
        {
            return new SecretPhraseOrAdminPassword("secretPhrase", secretPhrase);
        }

        public static SecretPhraseOrAdminPassword ByAdminPassword(string adminPassword)
        {
            return new SecretPhraseOrAdminPassword("adminPassword", adminPassword);
        }
    }
}