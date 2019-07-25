using System;
using Xunit;

namespace Net.Json.Test
{
    public class IdTest
    {
        [Fact]
        public void Test1()
        {
            var obj = new
            {
                _id = "Hello",
                Name = "World"
            };
            var result = obj.Serialize();
        }
    }
}
