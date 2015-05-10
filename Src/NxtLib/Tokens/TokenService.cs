using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Tokens
{
    public class TokenService : BaseService, ITokenService
    {
        public TokenService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public TokenService(IDateTimeConverter dateTimeConverter) 
            : base(dateTimeConverter)
        {
        }

        public async Task<DecodeHallmarkReply> DecodeHallmark(string hallmark)
        {
            var queryParameters = new Dictionary<string, string> { { "hallmark", hallmark } };
            return await Get<DecodeHallmarkReply>("decodeHallmark", queryParameters);
        }

        public async Task<TokenReply> DecodeToken(string website, string token)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"website", website},
                {"token", token}
            };
            return await Get<TokenReply>("decodeToken", queryParameters);
        }

        public async Task<GenerateTokenReply> GenerateToken(string secretPhrase, string website)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"secretPhrase", secretPhrase},
                {"website", website}
            };
            return await Post<GenerateTokenReply>("generateToken", queryParameters);
        }

        public async Task<MarkHostReply> MarkHost(string secretPhrase, string host, int weight, DateTime date)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"secretPhrase", secretPhrase},
                {"host", host},
                {"weight", weight.ToString()},
                {"date", date.ToString("yyyy-MM-dd")}
            };
            return await Post<MarkHostReply>("markHost", queryParameters);
        }
    }
}
