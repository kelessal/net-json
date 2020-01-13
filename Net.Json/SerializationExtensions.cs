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
                        EnumJsonConverter.Default,
                        ExpressionJsonConverter.Default,
                        NumberJsonConverter.Default,
                        StringJsonConverter.Default,
                        TimeSpanJsonConverter.Default

                    },
                ContractResolver = new DynamicContractResolver(),
            };
            return result;
        }
        static JsonSerializerSettings DefaultSettings = CreateDefaultSettings();
        static JsonSerializerSettings IndentedSettings = CreateIndentedSettings();

        private static JsonSerializerSettings CreateIndentedSettings()
        {
            var settings = CreateDefaultSettings();
            settings.Formatting = Formatting.Indented;
            return settings;
        }

        public static string Serialize(this object item,bool indent=false)
        {
            if (item == null) return string.Empty;
           var result= JsonConvert.SerializeObject(item,indent?
               IndentedSettings: DefaultSettings);
            return result;

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
