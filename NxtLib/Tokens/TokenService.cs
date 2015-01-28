using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.Tokens
{
    public interface ITokenService
    {
        Task<DecodeHallmarkReply> DecodeHallmark(string hallmark);
        Task<Token> DecodeToken(string website, string token);
        Task<TokenString> GenerateToken(string secretPhrase, string website);
        Task<MarkHostReply> MarkHost(string secretPhrase, string host, int weight, DateTime date);
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

        public async Task<DecodeHallmarkReply> DecodeHallmark(string hallmark)
        {
            var queryParameters = new Dictionary<string, string> { { "hallmark", hallmark } };
            return await Get<DecodeHallmarkReply>("decodeHallmark", queryParameters);
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
