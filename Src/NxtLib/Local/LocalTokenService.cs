using System;
using System.Text;
using NxtLib.Accounts;
using NxtLib.Internal;
using NxtLib.Internal.LocalSign;

namespace NxtLib.Local
{
    public interface ILocalTokenService
    {
        LocalGeneratedToken GenerateToken(string secretPhrase, string message, DateTime? timestamp = null);
        LocalDecodedToken DecodeToken(string message, string token);
    }

    public class LocalTokenService : ILocalTokenService
    {
        private readonly Crypto _crypto = new Crypto();
        private readonly ILocalAccountService _localAccountService = new LocalAccountService();

        public LocalGeneratedToken GenerateToken(string secretPhrase, string message, DateTime? timestamp = null)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            if (!timestamp.HasValue)
            {
                timestamp = DateTime.UtcNow;
            }
            var datetimeConverter = new DateTimeConverter();
            var nxtTimestamp = datetimeConverter.GetNxtTimestamp(timestamp.Value);
            var tokenString = _crypto.GenerateToken(secretPhrase, messageBytes, nxtTimestamp);

            var generatedToken = new LocalGeneratedToken
            {
                Timestamp = datetimeConverter.GetFromNxtTime(nxtTimestamp),
                Token = tokenString,
                Valid = true,
                Account = _localAccountService.GetAccount(AccountIdLocator.BySecretPhrase(secretPhrase))
            };

            return generatedToken;
        }

        public LocalDecodedToken DecodeToken(string message, string token)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message.Trim());
            var result = _crypto.DecodeToken(messageBytes, token);
            return result;
        }

    }
}