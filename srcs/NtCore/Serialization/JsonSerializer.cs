using Newtonsoft.Json;

namespace NtCore.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

        public T Deserialize<T>(string value) => JsonConvert.DeserializeObject<T>(value);
    }
}