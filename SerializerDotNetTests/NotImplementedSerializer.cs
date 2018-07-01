using SerializerDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SerializerDotNetTests
{
	public class NotImplementedSerializer : ISerializer
	{
		public string DefaultContentType => throw new NotImplementedException();

		public T Deserialize<T>(Stream source)
		{
			throw new NotImplementedException();
		}

		public T Deserialize<T>(byte[] source)
		{
			throw new NotImplementedException();
		}

		public Task<T> DeserializeAsync<T>(Stream source)
		{
			throw new NotImplementedException();
		}

		public Task<T> DeserializeAsync<T>(byte[] source)
		{
			throw new NotImplementedException();
		}

		public void Serialize<T>(T instance, Stream destination)
		{
			throw new NotImplementedException();
		}

		public byte[] Serialize<T>(T instance)
		{
			throw new NotImplementedException();
		}

		public Task SerializeAsync<T>(T instance, Stream destination)
		{
			throw new NotImplementedException();
		}

		public Task<byte[]> SerializeAsync<T>(T instance)
		{
			throw new NotImplementedException();
		}
	}
}
