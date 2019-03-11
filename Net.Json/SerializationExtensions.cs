using Net.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Net.Json
{
    public static class SerializationExtensions
    {
        public static JsonSerializerSettings CreateDefaultSettings()
        {
           var result= new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                Converters = new List<JsonConverter>()
                    {
                        BoolJsonConverter.Default,
                        ConcreteConverter.Default,
                        DateTimeJsonConverter.Default,
                        EnumJsonConverter.Default,
                        ExpressionJsonConverter.Default,
                        NumberJsonConverter.Default,
                        StringJsonConverter.Default,
                        TimeSpanJsonConverter.Default

                    },
                ContractResolver = new DynamicContractResolver()
            };
            return result;
        }
        static JsonSerializerSettings DefaultSettings = CreateDefaultSettings();
        public static string Serialize(this object item)
        {
            if (item == null) return string.Empty;
           return JsonConvert.SerializeObject(item, DefaultSettings);
        }
        public static T Deserialize<T>(this string serializationText)
            => (T)Deserialize(serializationText, typeof(T));
        public static object Deserialize(this string serializationText,Type deserializeType)
        {
            if (serializationText.IsEmpty()) return null;
            return JsonConvert.DeserializeObject(serializationText,deserializeType, DefaultSettings);
        }
    }
}
