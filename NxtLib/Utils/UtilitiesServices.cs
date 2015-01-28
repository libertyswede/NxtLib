using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.Utils
{
    public interface IUtilitiesServices
    {
        Task<LongConvertReply> LongConvert(ulong id);
        Task<RsConvertReply> RsConvert(string accountId);
    }

    public class UtilitiesServices : BaseService, IUtilitiesServices
    {
        public UtilitiesServices(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public UtilitiesServices(IDateTimeConverter dateTimeConverter) 
            : base(dateTimeConverter)
        {
        }

        // This can be done in .NET by just "var signedId = (long)id", but let's support the full NXT API
        public async Task<LongConvertReply> LongConvert(ulong id)
        {
            var queryParameters = new Dictionary<string, string> {{"id", id.ToString()}};
            return await Get<LongConvertReply>("longConvert", queryParameters);
        }

        public async Task<RsConvertReply> RsConvert(string accountId)
        {
            var queryParameters = new Dictionary<string, string> {{"account", accountId}};
            return await Get<RsConvertReply>("rsConvert", queryParameters);
        }
    }
}
