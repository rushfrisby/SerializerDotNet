using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SerializerDotNet
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializer(JsonSerializerSettings settings = null)
        {
            _settings = settings ?? new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
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
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(instance, _settings));
        }

        public async Task<byte[]> SerializeAsync<T>(T instance)
        {
            return await Task.Factory.StartNew(() => Serialize(instance));
        }

        public async Task SerializeAsync<T>(T instance, Stream destination)
        {
            await Task.Factory.StartNew(() => Serialize(instance, destination));
        }

        #endregion

        #region Deserialize

        public T Deserialize<T>(Stream source)
        {
            using var r = new StreamReader(source, Encoding.UTF8);
            return JsonConvert.DeserializeObject<T>(r.ReadToEnd(), _settings);
        }

        public object Deserialize(Stream source, Type type)
        {
            using var r = new StreamReader(source, Encoding.UTF8);
            return JsonConvert.DeserializeObject(r.ReadToEnd(), type, _settings);
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
            return await Task.Factory.StartNew(() => Deserialize<T>(source));
        }

        public async Task<T> DeserializeAsync<T>(byte[] source)
        {
            return await Task.Factory.StartNew(() => Deserialize<T>(source));
        }

        public async Task<object> DeserializeAsync(Stream source, Type type)
        {
            return await Task.Factory.StartNew(() => Deserialize(source, type));
        }

        public async Task<object> DeserializeAsync(byte[] source, Type type)
        {
            return await Task.Factory.StartNew(() => Deserialize(source, type));
        }

        #endregion
    }
}
