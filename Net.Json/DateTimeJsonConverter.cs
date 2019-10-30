using Newtonsoft.Json;
using System;

namespace Net.Json
{
    public class DateTimeJsonConverter : JsonConverter
    {
        public static readonly DateTimeJsonConverter Default = new DateTimeJsonConverter();

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType==typeof(DateTime?) ;
        }

        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                if (objectType == typeof(DateTime)) return DateTime.MinValue;
                return  null;
            }
            if(reader.ValueType==typeof(string))
            {
                if (DateTime.TryParse(reader.Value as string, out DateTime result)) return result;
                if(objectType==typeof(DateTime)) return DateTime.MinValue;
                return null;
            }
            if (reader.ValueType == typeof(DateTime)) return reader.Value;
            var val = Convert.ToInt64(reader.Value);
            var netticks = 10000 * val + 621355968000000000;
            netticks = Math.Max(DateTime.MinValue.Ticks, netticks);
            netticks = Math.Min(DateTime.MaxValue.Ticks, netticks);
            return new DateTime(netticks,DateTimeKind.Unspecified);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            if(value is DateTime t)
            {
                var jsonticks = t.Ticks - 621355968000000000;
                jsonticks = jsonticks / 10000;
                writer.WriteValue(jsonticks);
            } else
            {
                writer.WriteNull();
            }
        }
    }
}
