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
        public void IndentSerialize()
        {
            var obj = new
            {
                _id = "Hello",
                GIRIS_TARIHI = "World"
            };
            var result = obj.Serialize(true);
        }

    }
}
