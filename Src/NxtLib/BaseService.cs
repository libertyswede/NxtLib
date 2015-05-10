using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.Local;

namespace NxtLib
{
    public abstract class BaseService
    {
        private readonly IDateTimeConverter _dateTimeConverter;
        private readonly string _baseUrl;

        protected BaseService(IDateTimeConverter dateTimeConverter, string baseUrl = Constants.DefaultNxtUrl)
        {
            _dateTimeConverter = dateTimeConverter;
            _baseUrl = baseUrl;
        }

        protected async Task<JObject> Get(string requestType)
        {
            return await Get(requestType, new Dictionary<string, string>());
        }

        protected async Task<JObject> Get(string requestType, Dictionary<string, string> queryParameters)
        {
            var url = BuidUrl(requestType, queryParameters);

            using (var client = new HttpClient())
            using (var response = await client.GetAsync(url))
            using (var content = response.Content)
            {
                var json = await content.ReadAsStringAsync();
                CheckForErrorResponse(json, url);
                return JObject.Parse(json);
            }
        }

        protected async Task<JObject> Post(string requestType, Dictionary<string, string> queryParameters)
        {
            var url = BuidUrl(requestType, queryParameters);

            using (var client = new HttpClient())
            using (var response = await client.PostAsync(url, new StringContent(string.Empty, Encoding.UTF8, "application/json")))
            using (var content = response.Content)
            {
                var json = await content.ReadAsStringAsync();
                CheckForErrorResponse(json, url);
                return JObject.Parse(json);
            }
        }

        protected async Task<T> Get<T>(string requestType) where T : IBaseReply
        {
            return await Get<T>(requestType, new Dictionary<string, string>());
        }

        protected async Task<T> Get<T>(string requestType, Dictionary<string, string> queryParamsDictionary) where T : IBaseReply
        {
            var url = BuidUrl(requestType, queryParamsDictionary);
            return await GetUrl<T>(url);
        }

        protected async Task<T> Get<T>(string requestType, Dictionary<string, List<string>> queryParamsDictionary) where T : IBaseReply
        {
            var url = BuidUrl(requestType, queryParamsDictionary);
            return await GetUrl<T>(url);
        }

        private static async Task<T> GetUrl<T>(string url) where T : IBaseReply
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(url))
            using (var content = response.Content)
            {
                return await ReadAndDeserializeResponse<T>(content, url);
            }
        }

        protected async Task<T> Post<T>(string requestType) where T : IBaseReply
        {
            return await Post<T>(requestType, new Dictionary<string, string>());
        }

        protected async Task<T> Post<T>(string requestType, Dictionary<string, string> queryParamsDictionary) where T : IBaseReply
        {
            var url = BuidUrl(requestType, queryParamsDictionary);
            return await PostUrl<T>(url);
        }

        protected async Task<T> Post<T>(string requestType, Dictionary<string, List<string>> queryParamsDictionary) where T : IBaseReply
        {
            var url = BuidUrl(requestType, queryParamsDictionary);
            return await PostUrl<T>(url);
        }

        private static async Task<T> PostUrl<T>(string url) where T : IBaseReply
        {
            using (var client = new HttpClient())
            using (var response = await client.PostAsync(url, new StringContent(string.Empty, Encoding.UTF8, "application/json")))
            using (var content = response.Content)
            {
                return await ReadAndDeserializeResponse<T>(content, url);
            }
        }

        private string BuidUrl(string requestType, Dictionary<string, List<string>> queryParamsDictionary)
        {
            var url = _baseUrl + "?requestType=" + requestType;

            return queryParamsDictionary.Aggregate(url, (current1, keyValuePair) =>
                keyValuePair.Value.Aggregate(current1, (current, value) => current + ("&" + keyValuePair.Key + "=" + value)));
        }

        private string BuidUrl(string requestType, Dictionary<string, string> queryParamsDictionary)
        {
            var url = _baseUrl + "?requestType=" + requestType;
            url = queryParamsDictionary.Aggregate(url, (current, queryParam) => current + ("&" + queryParam.Key + "=" + queryParam.Value));
            return url;
        }

        private static async Task<T> ReadAndDeserializeResponse<T>(HttpContent content, string url) where T : IBaseReply
        {
            var json = await content.ReadAsStringAsync();
            CheckForErrorResponse(json, url);
            var response = JsonConvert.DeserializeObject<T>(json);
            response.RawJsonReply = json;
            response.RequestUri = url;
            return response;
        }

        private static void CheckForErrorResponse(string json, string url)
        {
            var jObject = JObject.Parse(json);
            var errorCode = jObject.SelectToken("errorCode");
            var error = jObject.SelectToken("error");
            var errorDescription = jObject.SelectToken("errorDescription");

            if (errorCode != null)
            {
                throw new NxtException((int)errorCode, json, url, errorDescription.ToString());
            }
            if (error != null)
            {
                throw new NxtException(-1, json, url, error.ToString());
            }
        }

        protected static void AddToParametersIfHasValue(string keyName, bool? value, Dictionary<string, string> queryParameters)
        {
            if (value.HasValue)
            {
                queryParameters.Add(keyName, value.Value.ToString());
            }
        }

        protected static void AddToParametersIfHasValue(string keyName, byte? value, Dictionary<string, string> queryParameters)
        {
            if (value.HasValue)
            {
                queryParameters.Add(keyName, value.Value.ToString());
            }
        }

        protected static void AddToParametersIfHasValue(string keyName, long? value, Dictionary<string, string> queryParameters)
        {
            if (value.HasValue)
            {
                queryParameters.Add(keyName, value.Value.ToString());
            }
        }

        protected static void AddToParametersIfHasValue(string keyName, ulong? value, Dictionary<string, string> queryParameters)
        {
            if (value.HasValue)
            {
                queryParameters.Add(keyName, value.Value.ToString());
            }
        }

        protected static void AddToParametersIfHasValue(string keyName, string value, Dictionary<string, string> queryParameters)
        {
            if (!string.IsNullOrEmpty(value))
            {
                queryParameters.Add(keyName, value);
            }
        }

        protected void AddToParametersIfHasValue(DateTime? timeStamp, Dictionary<string, string> queryParameters)
        {
            AddToParametersIfHasValue("timestamp", timeStamp, queryParameters);
        }

        protected void AddToParametersIfHasValue(string keyName, DateTime? timeStamp, Dictionary<string, string> queryParameters)
        {
            if (timeStamp.HasValue)
            {
                var convertedTimeStamp = _dateTimeConverter.GetEpochTime(timeStamp.Value.ToUniversalTime());
                queryParameters.Add(keyName, convertedTimeStamp.ToString());
            }
        }
    }
}