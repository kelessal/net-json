using Newtonsoft.Json;
using System;

namespace Net.Json
{
    public class DateTimeJsonConverter : JsonConverter
    {
        public static readonly DateTimeJsonConverter Default = new DateTimeJsonConverter();
        static DateTime MinDate = new DateTime(1902, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType==typeof(DateTime?) ;
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                if (objectType == typeof(DateTime)) return MinDate;
                return  null;
            }
            if(reader.ValueType==typeof(string))
            {
                if (DateTime.TryParse(reader.Value as string, out DateTime result)) return result;
                if(objectType==typeof(DateTime)) return MinDate;
                return null;
            }
            if (reader.ValueType == typeof(DateTime)) return reader.Value;
            var val = Convert.ToInt64(reader.Value);
            var netticks = 10000 * val + 621355968000000000;
            netticks = Math.Max(MinDate.Ticks, netticks);
            netticks = Math.Min(DateTime.MaxValue.Ticks, netticks);
            return new DateTime(netticks,DateTimeKind.Utc).ToLocalTime();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            if(value is DateTime t)
            {
                var jsonticks = t.ToUniversalTime().Ticks - 621355968000000000;
                jsonticks = jsonticks / 10000;
                writer.WriteValue(jsonticks);
            } else
            {
                writer.WriteNull();
            }
        }
    }
}
