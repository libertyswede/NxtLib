using System;

namespace NxtLib
{
    public class NxtException : Exception
    {
        public int ErrorCode { get; set; }
        public string JsonReply { get; set; }
        public string RequestUri { get; set; }

        public NxtException(int errorCode, string jsonReply, string requestUri, string message)
            : base(message)
        {
            ErrorCode = errorCode;
            JsonReply = jsonReply;
            RequestUri = requestUri;
        }
    }
}
