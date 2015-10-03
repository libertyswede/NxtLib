using System.Threading.Tasks;

namespace NxtLib.Utils
{
    public interface IUtilService
    {
        Task<DecodeQrCodeReply> DecodeQrCode(string qrCodeBase64);
        Task<HexConvertReply> HexConvert(string @string);
        Task<LongConvertReply> LongConvert(ulong id);
        Task<RsConvertReply> RsConvert(string accountId);
    }
}