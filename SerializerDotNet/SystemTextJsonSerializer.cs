using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using STJJsonSerializer = System.Text.Json.JsonSerializer;

namespace SerializerDotNet
{
    public class SystemTextJsonSerializer : ISerializer
    {
        private readonly JsonSerializerOptions _options;

        public SystemTextJsonSerializer(JsonSerializerOptions options = null)
        {
            _options = options ?? new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }

        public string DefaultContentType { get { return ContentType.Json; } }

        #region Serialize

        public void Serialize<T>(T instance, Stream destination)
        {
            var data = Serialize(instance);
            destination.Write(data, 0, data.Length);
        }

        public byte[] Serialize<T>(T instance)
        {
            return Encoding.UTF8.GetBytes(STJJsonSerializer.Serialize(instance, _options));
        }

        public async Task<byte[]> SerializeAsync<T>(T instance)
        {
            using var stream = new MemoryStream();
            await SerializeAsync(instance, stream);
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return Encoding.UTF8.GetBytes(await reader.ReadToEndAsync());
        }

        public async Task SerializeAsync<T>(T instance, Stream destination)
        {
            await STJJsonSerializer.SerializeAsync(destination, instance, _options);
        }

        #endregion

        #region Deserialize

        public T Deserialize<T>(Stream source)
        {
            using var reader = new StreamReader(source, Encoding.UTF8);
            return STJJsonSerializer.Deserialize<T>(reader.ReadToEnd(), _options);
        }

        public object Deserialize(Stream source, Type type)
        {
            using var reader = new StreamReader(source, Encoding.UTF8);
            return STJJsonSerializer.Deserialize(reader.ReadToEnd(), type, _options);
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
            return await STJJsonSerializer.DeserializeAsync<T>(source, _options);
        }

        public async Task<T> DeserializeAsync<T>(byte[] source)
        {
            using var ms = new MemoryStream(source);
            return await DeserializeAsync<T>(ms);
        }

        public async Task<object> DeserializeAsync(Stream source, Type type)
        {
            return await STJJsonSerializer.DeserializeAsync(source, type, _options);
        }

        public async Task<object> DeserializeAsync(byte[] source, Type type)
        {
            using var ms = new MemoryStream(source);
            return await DeserializeAsync(ms, type);
        }

        #endregion
    }
}
