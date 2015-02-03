using System.Threading.Tasks;

namespace NxtLib.Utils
{
    public interface IUtilService
    {
        Task<LongConvertReply> LongConvert(ulong id);
        Task<RsConvertReply> RsConvert(string accountId);
    }
}