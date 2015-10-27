using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Utils
{
    public class UtilService : BaseService, IUtilService
    {
        public UtilService(string baseAddress = Constants.DefaultNxtUrl)
            : base(baseAddress)
        {
        }

        public async Task<DecodeQrCodeReply> DecodeQrCode(string qrCodeBase64)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.QrCodeBase64, qrCodeBase64}};
            return await Post<DecodeQrCodeReply>("decodeQRCode", queryParameters);
        }

        public async Task<EncodeQrCodeReply> EncodeQrCode(string qrCodeData, int? width = null, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.QrCodeData, qrCodeData}};
            queryParameters.AddIfHasValue(Parameters.Width, width);
            queryParameters.AddIfHasValue(Parameters.Height, height);
            return await Post<EncodeQrCodeReply>("encodeQRCode", queryParameters);
        }

        public async Task<FullHashToIdReply> FullHashToId(string fullHash)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.FullHash, fullHash}};
            return await Get<FullHashToIdReply>("fullHashToId", queryParameters);
        }

        public async Task<HashReply> Hash(HashAlgorithm hashAlgorithm, BinaryHexString secret, bool? secretIsText = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.HashAlgorithm, ((int) hashAlgorithm).ToString()},
                {Parameters.Secret, secret.ToString()}
            };
            queryParameters.AddIfHasValue(Parameters.SecretIsText, secretIsText);
            return await Get<HashReply>("hash", queryParameters);
        }

        public async Task<HexConvertReply> HexConvert(string @string)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.String, @string}};
            return await Get<HexConvertReply>("hexConvert", queryParameters);
        }

        // This can be done in .NET by just "var signedId = (long)unsignedId", but let's support the full NXT API
        public async Task<LongConvertReply> LongConvert(ulong id)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Id, id.ToString()}};
            return await Get<LongConvertReply>("longConvert", queryParameters);
        }

        public async Task<RsConvertReply> RsConvert(string account)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account}};
            return await Get<RsConvertReply>("rsConvert", queryParameters);
        }
    }
}