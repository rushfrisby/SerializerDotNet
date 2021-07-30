using System;
using System.IO;
using System.Threading.Tasks;

namespace SerializerDotNet
{
    public class ProtobufSerializer : ISerializer
    {
        public string DefaultContentType { get { return ContentType.Protobuf; } }

        #region Serialize

        public void Serialize<T>(T instance, Stream destination)
        {
            ProtoBuf.Serializer.Serialize(destination, instance);
        }

        public byte[] Serialize<T>(T instance)
        {
            using (var ms = new MemoryStream())
            {
                Serialize(instance, ms);
                return ms.ToArray();
            }
        }

        public async Task SerializeAsync<T>(T instance, Stream destination)
        {
            await Task.Factory.StartNew(() => ProtoBuf.Serializer.Serialize(destination, instance));
        }

        public async Task<byte[]> SerializeAsync<T>(T instance)
        {
            return await Task.Factory.StartNew(() => Serialize(instance));
        }

        #endregion

        #region Deserialize

        public T Deserialize<T>(Stream source)
        {
            return ProtoBuf.Serializer.Deserialize<T>(source);
        }

        public object Deserialize(Stream source, Type type)
        {
            return ProtoBuf.Serializer.Deserialize(type, source);
        }

        public T Deserialize<T>(byte[] source)
        {
            using var ms = new MemoryStream(source);
            return Deserialize<T>(ms);
        }

        public object Deserialize(byte[] source, Type type)
        {
            using var ms = new MemoryStream(source);
            return Deserialize(ms, type);
        }

        public async Task<T> DeserializeAsync<T>(Stream source)
        {
            return await Task.Factory.StartNew(() => ProtoBuf.Serializer.Deserialize<T>(source));
        }

        public async Task<object> DeserializeAsync(Stream source, Type type)
        {
            return await Task.Factory.StartNew(() => Deserialize(source, type));
        }

        public async Task<T> DeserializeAsync<T>(byte[] source)
        {
            return await Task.Factory.StartNew(() => Deserialize<T>(source));
        }

        public async Task<object> DeserializeAsync(byte[] source, Type type)
        {
            return await Task.Factory.StartNew(() => Deserialize(source, type));
        }

        #endregion
    }
}
