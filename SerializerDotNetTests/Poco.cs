using System.Runtime.Serialization;

namespace SerializerDotNetTests
{
    [DataContract]
    public record Poco()
    {
        [DataMember(Order = 1)]
        public int Id { get; set; } = int.MaxValue;

        [DataMember(Order = 2)]
        public string Name { get; set; } = "I'm in love with the POCO";
    }
}
