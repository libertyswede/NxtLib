using System;
using Newtonsoft.Json;

namespace NxtLib.Internal
{
    internal class DateTimeConverter : JsonConverter, IDateTimeConverter
    {
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly DateTime Nov24Th2013 = new DateTime(2013, 11, 24, 12, 0, 0, DateTimeKind.Utc);
        public const long EpochBeginning = 1385294400000; // milliseconds between 1970-01-01 00:00:00 and 2013-11-24 12:00:00

        internal static int GetNxtTime(DateTime dateTime)
        {
            return (int)(((dateTime - Jan1St1970).TotalMilliseconds - EpochBeginning + 500) / 1000);
        }

        internal static DateTime GetDateTime(int dateTime)
        {
            return Nov24Th2013.AddSeconds(dateTime);
        }

        public int GetEpochTime(DateTime dateTime)
        {
            return (int)(((dateTime - Jan1St1970).TotalMilliseconds - EpochBeginning + 500) / 1000);
        }

        public DateTime GetFromNxtTime(int dateTime)
        {
            return Nov24Th2013.AddSeconds(dateTime);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if ((reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer) &&
                objectType == typeof (DateTime))
            {
                int nxtTime;
                var stringValue = reader.Value.ToString();
                if (Int32.TryParse(stringValue, out nxtTime))
                {
                    return GetFromNxtTime(nxtTime);
                }
                DateTime timestamp;
                if (DateTime.TryParse(stringValue, out timestamp))
                {
                    return timestamp;
                }
            }
            throw new NotSupportedException(string.Format("objectType {0} and TokenType {1} is not supported", objectType, reader.TokenType));
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
