using System.Collections.Generic;

namespace NxtLib
{
    public abstract class LocatorBase
    {
        private readonly Dictionary<string, string> _queryParameters;

        internal Dictionary<string, string> QueryParameters => new Dictionary<string, string>(_queryParameters);

        protected LocatorBase(string key, string value)
        {
            _queryParameters = new Dictionary<string, string> {{key, value}};
        }

        protected LocatorBase(Dictionary<string, string> parameters)
        {
            _queryParameters = parameters;
        }
    }
}