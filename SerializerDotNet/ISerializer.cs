using System.IO;
using System.Threading.Tasks;

namespace SerializerDotNet
{
	public interface ISerializer
	{
		string DefaultContentType { get; }

		void Serialize<T>(T instance, Stream destination);

		Task SerializeAsync<T>(T instance, Stream destination);

		byte[] Serialize<T>(T instance);

		Task<byte[]> SerializeAsync<T>(T instance);

		T Deserialize<T>(Stream source);

		Task<T> DeserializeAsync<T>(Stream source);

		T Deserialize<T>(byte[] source);

		Task<T> DeserializeAsync<T>(byte[] source);
	}
}
