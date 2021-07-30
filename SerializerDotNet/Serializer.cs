using System;
using System.Collections.Generic;

namespace SerializerDotNet
{
    public class Serializer
    {
        private static Lazy<IDictionary<string, ISerializer>> _maps = new Lazy<IDictionary<string, ISerializer>>(() => GetDefaultMappings());

        private static IDictionary<string, ISerializer> Maps { get { return _maps.Value; } }

        public static IDictionary<string, ISerializer> GetDefaultMappings()
        {
            return new Dictionary<string, ISerializer>()
            {
                { ContentType.Json, new JsonSerializer() },
                { ContentType.Protobuf, new ProtobufSerializer() },
                { ContentType.XProtobuf, new ProtobufSerializer() },
            };
        }

        public static void SetMappings(IDictionary<string, ISerializer> mappings)
        {
            if (mappings == null)
            {
                throw new ArgumentNullException(nameof(mappings));
            }
            _maps = new Lazy<IDictionary<string, ISerializer>>(() => mappings);
        }

        public static ISerializer GetSerializerFor(string contentType)
        {
            if (!Maps.ContainsKey(contentType))
            {
                return null;
            }
            return Maps[contentType];
        }
    }
}
