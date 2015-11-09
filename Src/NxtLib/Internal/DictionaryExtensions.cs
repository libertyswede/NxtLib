using System;
using System.Collections.Generic;
using NxtLib.VotingSystem;

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

        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, bool? value)
        {
            if (value.HasValue)
            {
                me.Add(key, value.Value.ToString());
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, Account value)
        {
            if (value != null)
            {
                me.Add(key, value.AccountId.ToString());
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, BinaryHexString value)
        {
            if (value != null)
            {
                me.Add(key, value.ToHexString());
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, DateTime? timeStamp)
        {
            if (timeStamp.HasValue)
            {
                var dateTimeConverter = new DateTimeConverter();
                var convertedTimeStamp = dateTimeConverter.GetNxtTimestamp(timeStamp.Value.ToUniversalTime());
                me.Add(key, convertedTimeStamp.ToString());
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, MinBalanceModel? minBalanceModel)
        {
            if (minBalanceModel.HasValue)
            {
                me.Add(key, ((int)minBalanceModel.Value).ToString());
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, byte? value)
        {
            if (value.HasValue)
            {
                me.Add(key, value.Value.ToString());
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, long? value)
        {
            if (value.HasValue)
            {
                me.Add(key, value.Value.ToString());
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, string> me, string key, ulong? value)
        {
            if (value.HasValue)
            {
                me.Add(key, value.Value.ToString());
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, List<string>> me, string key, int? value)
        {
            if (value.HasValue)
            {
                me.Add(key, new List<string> {value.Value.ToString()});
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, List<string>> me, string key, ulong? value)
        {
            if (value.HasValue)
            {
                me.Add(key, new List<string> {value.Value.ToString()});
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, List<string>> me, string key, bool? value)
        {
            if (value.HasValue)
            {
                me.Add(key, new List<string> {value.Value.ToString()});
            }
        }

        internal static void AddIfHasValue(this Dictionary<string, List<string>> me, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                me.Add(key, new List<string> {value});
            }
        }
    }
}