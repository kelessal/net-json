using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Xunit;

namespace Net.Json.Test
{
    public  class SerializationTest
    {
        [Fact]
        public void SerializeObjectTest()
        {
            var obj = new
            {
                Property1 = "Property 1",
                Property2 = 2,
                _id = "3"
            };
            var result=obj.Serialize();
        }
        [Fact]
        public void CustomSerializeObjectTest()
        {
            var obj = new
            {
                Property1 = "Property 1",
                Property2 = 2,
                _id = "3"
            };
            var resolver = new DynamicContractResolver()
            {
                MongoIdConversion = false,
                LowerFirstLetter = false
            };
            SerializationExtensions.DefaultSettings.ContractResolver = resolver;
            var serializeText = obj.Serialize();
            var deserializeText= serializeText.Deserialize<ExpandoObject>();
        }
        [Fact]
        public void AsExpandoTest()
        {
            var obj = new
            {
                Property1 = "Property 1",
                Property2 = 2,
                _id = "3",
                List=new List<object>(new[]
                {
                    new
                    {
                        SubProp1="Sub Prop 1",
                        SubProp2=2,
                        _id="2"
                    }
                })
            };
            dynamic expando = obj.AsExpandoObject();
            var list = expando.list;
        }
    }
}
