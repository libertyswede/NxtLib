using System;
using System.Threading.Tasks;

namespace NxtLib.Tokens
{
    public interface ITokenService
    {
        Task<DecodeHallmarkReply> DecodeHallmark(string hallmark);
        Task<TokenReply> DecodeToken(string website, string token);
        Task<GenerateTokenReply> GenerateToken(string secretPhrase, string website);
        Task<MarkHostReply> MarkHost(string secretPhrase, string host, int weight, DateTime date);
    }
}