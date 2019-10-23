using Net.Extensions;
using Net.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Net.Json
{
    public class DynamicContractResolver : CamelCasePropertyNamesContractResolver
    {

        protected override string ResolveDictionaryKey(string dictionaryKey)
        {
            return dictionaryKey == "_id" ? "id" : dictionaryKey.ToLowerFirstLetter(isInvariant: true);
        }
     
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLowerFirstLetter(isInvariant:true);
        }
        protected override JsonProperty CreateProperty(MemberInfo member
                                                 , MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (prop.PropertyName == "_id")
            {
                prop.PropertyName = "id";
            } else
            {
                prop.PropertyName = member.Name.ToLowerFirstLetter(isInvariant:true);
            }
            if (member.ReflectedType.IsInterface && !member.ReflectedType.IsCollectionType())
            {
                prop.Converter = ConcreteConverter.Default;
                prop.Writable = true;
                return prop;
            }
            if (prop.Readable)
            {
                var property = member as PropertyInfo;
                if (property != null)
                {
                    var hasPrivateSetter = property.GetSetMethod(true) != null;
                    prop.Writable = hasPrivateSetter;
                }
            }
            if (member.ReflectedType.IsAssignableTo<Expression>())
                prop.Converter = ExpressionJsonConverter.Default;
            return prop;
        }
    }
    
}

