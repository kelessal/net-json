using Newtonsoft.Json;
using System;

namespace Net.Json
{
    public class DateTimeJsonConverter : JsonConverter
    {
        public static readonly DateTimeJsonConverter Default = new DateTimeJsonConverter();

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value ?? DateTime.UtcNow;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }
    }
}
