using System;
using Newtonsoft.Json;
using NxtLib.Local;

namespace NxtLib.Internal
{
    internal class DateTimeConverter : JsonConverter
    {
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public const long EpochBeginning = 1385294400000; // milliseconds between 1970-01-01 00:00:00 and 2013-11-24 12:00:00

        internal int GetNxtTimestamp(DateTime dateTime)
        {
            return (int)(((dateTime - Jan1St1970).TotalMilliseconds - EpochBeginning + 500) / 1000);
        }

        internal DateTime GetFromNxtTime(int dateTime)
        {
            return Constants.EpochBeginning.AddSeconds(dateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if ((reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer) &&
                objectType == typeof (DateTime))
            {
                int nxtTime;
                var stringValue = reader.Value.ToString();
                if (int.TryParse(stringValue, out nxtTime))
                {
                    return GetFromNxtTime(nxtTime);
                }
                DateTime timestamp;
                if (DateTime.TryParse(stringValue, out timestamp))
                {
                    return timestamp;
                }
            }
            throw new NotSupportedException($"objectType {objectType} and TokenType {reader.TokenType} is not supported");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
