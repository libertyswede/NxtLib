namespace NxtLib.Forging
{
    public class SecretPhraseOrAdminPassword : LocatorBase
    {
        public readonly string SecretPhrase;
        public readonly string AdminPassword;

        private SecretPhraseOrAdminPassword(string key, string value) : base(key, value)
        {
            if (key.Equals("secretPhrase"))
            {
                SecretPhrase = value;
            }
            else if (key.Equals("adminPassword"))
            {
                AdminPassword = value;
            }
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