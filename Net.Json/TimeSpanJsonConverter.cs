using Newtonsoft.Json;
using System;

namespace Net.Json
{
    public class TimeSpanJsonConverter : JsonConverter
    {
        public static readonly TimeSpanJsonConverter Default = new TimeSpanJsonConverter();

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan);
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value == null ? new TimeSpan() : TimeSpan.Parse(reader.Value as string);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }
    }
}
