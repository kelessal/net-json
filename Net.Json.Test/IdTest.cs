using System;
using System.Dynamic;
using Xunit;

namespace Net.Json.Test
{
    public class IdTest
    {
        [Fact]
        public void Test1()
        {
            dynamic obj = new ExpandoObject();
            obj._id = "Hello";
            obj.Name = "World";
            var result = SerializationExtensions.Serialize(obj);
        }
        [Fact]
        public void TestCamelCase()
        {
            var obj = new
            {
                BOLGE_ADI="ANTALYA",
                CIKIS_TARIHI="123"
            };
            var result = SerializationExtensions.Serialize(obj);
            var r2 = SerializationExtensions.Deserialize(result,obj.GetType());
        }
        [Fact]
        public void TestArray()
        {
            var obj = new[] { 1, 2, 3 };
            var result = SerializationExtensions.Serialize(obj);
            var r2 = SerializationExtensions.Deserialize(result, obj.GetType());
        }

        [Fact]
        public void IndentSerialize()
        {
            var obj = new
            {
                _id = "Hello",
                GIRIS_TARIHI = "World"
            };
            var result = obj.Serialize(true);
        }
        [Fact]
        public void TestDateTime()
        {

            var obj1 = new
            {
                _id = "Hello",
                GIRIS_TARIHI =(DateTime?) null
            };
          
            var result = obj1.Serialize(true);
            var deserialized = "{GIRIS_TARIHI:23435}".Deserialize(obj1.GetType());
        }

    }
}
