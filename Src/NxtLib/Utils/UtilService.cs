using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Utils
{
    public class UtilService : BaseService, IUtilService
    {
        public UtilService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public UtilService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<DecodeQrCodeReply> DecodeQrCode(string qrCodeBase64)
        {
            var queryParameters = new Dictionary<string, string> {{"qrCodeBase64", qrCodeBase64}};
            return await Post<DecodeQrCodeReply>("decodeQRCode", queryParameters);
        }

        public async Task<EncodeQrCodeReply> EncodeQrCode(string qrCodeData, int? width = null, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> {{"qrCodeData", qrCodeData}};
            queryParameters.AddIfHasValue(nameof(width), width);
            queryParameters.AddIfHasValue(nameof(height), height);
            return await Post<EncodeQrCodeReply>("encodeQRCode", queryParameters);
        }

        public async Task<FullHashToIdReply> FullHashToId(string fullHash)
        {
            var queryParameters = new Dictionary<string, string> {{"fullHash", fullHash}};
            return await Get<FullHashToIdReply>("fullHashToId", queryParameters);
        }

        public async Task<HashReply> Hash(HashAlgorithm hashAlgorithm, BinaryHexString secret, bool? secretIsText = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"hashAlgorithm", ((int) hashAlgorithm).ToString()},
                {"secret", secret.ToString()}
            };
            queryParameters.AddIfHasValue(nameof(secretIsText), secretIsText);
            return await Get<HashReply>("hash", queryParameters);
        }

        public async Task<HexConvertReply> HexConvert(string @string)
        {
            var queryParameters = new Dictionary<string, string> {{"string", @string}};
            return await Get<HexConvertReply>("hexConvert", queryParameters);
        }

        // This can be done in .NET by just "var signedId = (long)unsignedId", but let's support the full NXT API
        public async Task<LongConvertReply> LongConvert(ulong id)
        {
            var queryParameters = new Dictionary<string, string> {{"id", id.ToString()}};
            return await Get<LongConvertReply>("longConvert", queryParameters);
        }

        public async Task<RsConvertReply> RsConvert(string account)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account}};
            return await Get<RsConvertReply>("rsConvert", queryParameters);
        }
    }
}