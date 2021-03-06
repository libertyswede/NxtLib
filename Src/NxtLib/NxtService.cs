﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NxtLib.Local;

namespace NxtLib
{
    public interface INxtService
    {
        Task<JObject> Get(string requestType, Dictionary<string, string> queryParameters);
        Task<JObject> Post(string requestType, Dictionary<string, string> queryParameters);
    }

    public class NxtService : BaseService, INxtService
    {
        public NxtService(string baseUrl = Constants.DefaultNxtUrl)
            : base(baseUrl)
        {
        }

        public NxtService()
        {
        }

        public new async Task<JObject> Get(string requestType, Dictionary<string, string> queryParameters)
        {
            return await base.Get(requestType, queryParameters);
        }

        public async Task<JObject> Post(string requestType, Dictionary<string, string> queryParameters)
        {
            return await base.Post(requestType, queryParameters);
        }
    }
}
