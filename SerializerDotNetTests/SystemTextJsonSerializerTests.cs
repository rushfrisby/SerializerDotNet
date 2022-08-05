using SerializerDotNet;
using System.Text;
using Xunit;

namespace SerializerDotNetTests
{
    public class SystemTextJsonSerializerTests
    {
        [Fact]
        public void Serialize_Poco_NotNull()
        {
            var serializer = new SystemTextJsonSerializer();
            var poco = new Poco();
            var data = serializer.Serialize(poco);
            Assert.NotNull(data);
        }

        [Fact]
        public void Deserialize_GenericPoco_NotNull()
        {
            var serializer = new SystemTextJsonSerializer();
            var data = serializer.Serialize(new Poco());
            var poco = serializer.Deserialize<Poco>(data);
            Assert.NotNull(poco);
            Assert.Equal(new Poco(), poco);
        }

        [Fact]
        public void Deserialize_TypePoco_NotNull()
        {
            var serializer = new SystemTextJsonSerializer();
            var data = serializer.Serialize(new Poco());
            var poco = serializer.Deserialize(data, typeof(Poco));
            Assert.NotNull(poco);
            Assert.True(typeof(Poco) == poco.GetType());
        }
    }
}
