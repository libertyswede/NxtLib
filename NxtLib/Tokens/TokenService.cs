using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.TokenOperations
{
    public interface ITokenService
    {
        Task<Token> DecodeToken(string website, string token);
        Task<TokenString> GenerateToken(string secretPhrase, string website);
    }

    public class TokenService : BaseService, ITokenService
    {
        public TokenService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public TokenService(IDateTimeConverter dateTimeConverter) 
            : base(dateTimeConverter)
        {
        }

        public async Task<Token> DecodeToken(string website, string token)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"website", website},
                {"token", token}
            };
            return await Get<Token>("decodeToken", queryParameters);
        }

        public async Task<TokenString> GenerateToken(string secretPhrase, string website)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"secretPhrase", secretPhrase},
                {"website", website}
            };
            return await Post<TokenString>("generateToken", queryParameters);
        }
    }
}
