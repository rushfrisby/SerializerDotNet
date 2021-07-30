using System;
using System.IO;
using System.Threading.Tasks;

namespace SerializerDotNet
{
    public interface ISerializer
    {
        string DefaultContentType { get; }

        void Serialize<T>(T instance, Stream destination);

        byte[] Serialize<T>(T instance);

        Task SerializeAsync<T>(T instance, Stream destination);

        Task<byte[]> SerializeAsync<T>(T instance);

        T Deserialize<T>(Stream source);

        object Deserialize(Stream source, Type type);

        T Deserialize<T>(byte[] source);

        object Deserialize(byte[] source, Type type);

        Task<T> DeserializeAsync<T>(byte[] source);

        Task<object> DeserializeAsync(byte[] source, Type type);

        Task<T> DeserializeAsync<T>(Stream source);

        Task<object> DeserializeAsync(Stream source, Type type);
    }
}
