using NxtLib.Accounts;
using NxtLib.Internal.LocalSign;

namespace NxtLib.Local
{
    public interface ILocalAccountService
    {
        AccountWithPublicKey GetAccount(AccountIdLocator locator);
    }

    public class LocalAccountService : ILocalAccountService
    {
        private readonly Crypto _crypto = new Crypto();

        public AccountWithPublicKey GetAccount(AccountIdLocator locator)
        {
            var publicKey = !string.IsNullOrEmpty(locator.SecretPhrase)
                ? _crypto.GetPublicKey(locator.SecretPhrase)
                : locator.PublicKey;

            var accountId = _crypto.GetAccountIdFromPublicKey(publicKey);
            return new AccountWithPublicKey(accountId, publicKey);
        }

        internal string GetReedSolomonFromAccountId(ulong accountId)
        {
            return ReedSolomon.Encode(accountId);
        }

        internal ulong GetAccountIdFromReedSolomon(string reedSolomonAddress)
        {
            return ReedSolomon.Decode(reedSolomonAddress);
        }
    }
}