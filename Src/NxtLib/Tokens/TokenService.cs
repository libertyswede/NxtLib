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

        public async Task<TokenReply> DecodeFileToken(string file, string token)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(token), token}, {nameof(file), file}};
            return await PostAsContent<TokenReply>("decodeFileToken", queryParameters);
        }

        public async Task<DecodeHallmarkReply> DecodeHallmark(string hallmark)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(hallmark), hallmark}};
            return await Get<DecodeHallmarkReply>("decodeHallmark", queryParameters);
        }

        public async Task<TokenReply> DecodeToken(string website, string token)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(website), website},
                {nameof(token), token}
            };
            return await Get<TokenReply>("decodeToken", queryParameters);
        }

        public async Task<GenerateTokenReply> GenerateFileToken(string secretPhrase, string file)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(secretPhrase), secretPhrase},
                {nameof(file), file}
            };
            return await PostAsContent<GenerateTokenReply>("generateFileToken", queryParameters);
        }

        public async Task<GenerateTokenReply> GenerateToken(string secretPhrase, string website)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(secretPhrase), secretPhrase},
                {nameof(website), website}
            };
            return await Post<GenerateTokenReply>("generateToken", queryParameters);
        }

        public async Task<MarkHostReply> MarkHost(string secretPhrase, string host, int weight, DateTime date)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(secretPhrase), secretPhrase},
                {nameof(host), host},
                {nameof(weight), weight.ToString()},
                {nameof(date), date.ToString("yyyy-MM-dd")}
            };
            return await Post<MarkHostReply>("markHost", queryParameters);
        }
    }
}
