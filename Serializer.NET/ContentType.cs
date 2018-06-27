using System;
using System.Collections.Generic;

namespace Serializer.NET
{
	public class ContentType
	{
		public const string Json = "application/json";
		public const string Protobuf = "application/protobuf";

		private static IDictionary<string, Type> _mapping = GetDefaultSerializerMapping();

		public static IDictionary<string, Type> GetDefaultSerializerMapping()
		{
			return new Dictionary<string, Type>()
			{
				{ Json, typeof(JsonSerializer) },
				{ Protobuf, typeof(ProtobufSerializer) },
			};
		}

		public static void SetSerializerMapping(IDictionary<string, Type> mapping)
		{
			_mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
		}

		public static ISerializer GetSerializerFor(string contentType)
		{
			if(!_mapping.ContainsKey(contentType))
			{
				return null;
			}
			return (ISerializer)Activator.CreateInstance(_mapping[contentType]);
		}
	}
}
