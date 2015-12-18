using System;
using System.Collections.Generic;

namespace NxtLib
{
    public class NxtException : Exception
    {
        public int ErrorCode { get; }
        public string JsonReply { get; }
        public string RequestUri { get; }
        public IEnumerable<KeyValuePair<string, string>> QueryParameters { get; }

        public NxtException(int errorCode, string jsonReply, string requestUri, string message, IEnumerable<KeyValuePair<string, string>> queryParameters)
            : base(message)
        {
            ErrorCode = errorCode;
            JsonReply = jsonReply;
            RequestUri = requestUri;
            QueryParameters = queryParameters;
        }
    }
}
