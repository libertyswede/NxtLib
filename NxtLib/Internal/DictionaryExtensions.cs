using System.Collections.Generic;

namespace NxtLib.Internal
{
    internal static class DictionaryExtensions
    {
        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                me.Add(key, value);
            }
        }
        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, BinaryHexString value)
        {
            if (value != null)
            {
                me.Add(key, value.ToHexString());
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, long? value)
        {
            if (value.HasValue)
            {
                me.Add(key, value.Value.ToString());
            }
        }
    }
}
