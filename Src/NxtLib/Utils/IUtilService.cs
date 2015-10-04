using System.Threading.Tasks;

namespace NxtLib.Utils
{
    public interface IUtilService
    {
        Task<DecodeQrCodeReply> DecodeQrCode(string qrCodeBase64);
        Task<EncodeQrCodeReply> EncodeQrCode(string qrCodeData, int? width = null, int? height = null);
        Task<FullHashToIdReply> FullHashToId(string fullHash);
        Task<HashReply> Hash(HashAlgorithm hashAlgorithm, BinaryHexString secret, bool? secretIsText = null);
        Task<HexConvertReply> HexConvert(string @string);
        Task<LongConvertReply> LongConvert(ulong id);
        Task<RsConvertReply> RsConvert(string accountId);
    }
}