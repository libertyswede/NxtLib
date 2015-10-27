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
            : base(baseAddress)
        {
        }

        public async Task<TokenReply> DecodeFileToken(string file, string token)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Token, token}, {Parameters.File, file}};
            return await PostAsContent<TokenReply>("decodeFileToken", queryParameters);
        }

        public async Task<DecodeHallmarkReply> DecodeHallmark(string hallmark)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Hallmark, hallmark}};
            return await Get<DecodeHallmarkReply>("decodeHallmark", queryParameters);
        }

        public async Task<TokenReply> DecodeToken(string website, string token)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Website, website},
                {Parameters.Token, token}
            };
            return await Get<TokenReply>("decodeToken", queryParameters);
        }

        public async Task<GenerateTokenReply> GenerateFileToken(string secretPhrase, string file)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.SecretPhrase, secretPhrase},
                {Parameters.File, file}
            };
            return await PostAsContent<GenerateTokenReply>("generateFileToken", queryParameters);
        }

        public async Task<GenerateTokenReply> GenerateToken(string secretPhrase, string website)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.SecretPhrase, secretPhrase},
                {Parameters.Website, website}
            };
            return await Post<GenerateTokenReply>("generateToken", queryParameters);
        }

        public async Task<MarkHostReply> MarkHost(string secretPhrase, string host, int weight, DateTime date)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.SecretPhrase, secretPhrase},
                {Parameters.Host, host},
                {Parameters.Weight, weight.ToString()},
                {Parameters.Date, date.ToString("yyyy-MM-dd")}
            };
            return await Post<MarkHostReply>("markHost", queryParameters);
        }
    }
}
