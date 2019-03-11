using Net.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq.Expressions;
using System.Reflection;

namespace Net.Json
{
    public class DynamicContractResolver : CamelCasePropertyNamesContractResolver
    {
      
        protected override JsonProperty CreateProperty(MemberInfo member
                                                 , MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
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

