using System;
using System.Threading.Tasks;

namespace NxtLib.Tokens
{
    public interface ITokenService
    {
        Task<DecodeHallmarkReply> DecodeHallmark(string hallmark);
        Task<Token> DecodeToken(string website, string token);
        Task<TokenString> GenerateToken(string secretPhrase, string website);
        Task<MarkHostReply> MarkHost(string secretPhrase, string host, int weight, DateTime date);
    }
}