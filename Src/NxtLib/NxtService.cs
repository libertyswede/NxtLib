using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NxtLib
{
    public interface INxtService
    {
        Task<JObject> Get(string requestType, Dictionary<string, string> queryParameters);
        Task<JObject> Post(string requestType, Dictionary<string, string> queryParameters);
    }

    public class NxtService : BaseService, INxtService
    {
        public NxtService(IDateTimeConverter dateTimeConverter, string baseUrl = DefaultBaseUrl)
            : base(dateTimeConverter, baseUrl)
        {
        }

        public NxtService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public new async Task<JObject> Get(string requestType, Dictionary<string, string> queryParameters)
        {
            return await base.Get(requestType, queryParameters);
        }

        public new async Task<JObject> Post(string requestType, Dictionary<string, string> queryParameters)
        {
            return await base.Post(requestType, queryParameters);
        }
    }
}
