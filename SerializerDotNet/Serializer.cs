using System;
using System.Collections.Generic;

namespace SerializerDotNet
{
	public class Serializer
	{
		private static IDictionary<string, ISerializer> _maps = GetDefaultMappings();

		private static readonly IDictionary<string, ISerializer> _defaultMaps = new Dictionary<string, ISerializer>()
		{
			{ ContentType.Json, new JsonSerializer() },
			{ ContentType.Protobuf, new ProtobufSerializer() },
			{ ContentType.XProtobuf, new ProtobufSerializer() },
		};

		public static IDictionary<string, ISerializer> GetDefaultMappings()
		{
			return _defaultMaps;
		}

		public static void SetMappings(IDictionary<string, ISerializer> mappings)
		{
			_maps = mappings ?? throw new ArgumentNullException(nameof(mappings));
		}

		public static ISerializer GetSerializerFor(string contentType)
		{
			if (!_maps.ContainsKey(contentType))
			{
				return null;
			}
			return _maps[contentType];
		}
	}
}
