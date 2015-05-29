using System.Threading.Tasks;

namespace NxtLib.Utils
{
    public interface IUtilService
    {
        Task<HexConvertReply> HexConvert(string @string);
        Task<LongConvertReply> LongConvert(ulong id);
        Task<RsConvertReply> RsConvert(string accountId);
    }
}