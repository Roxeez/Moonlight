using System.IO;

namespace NtCore.Serialization
{
    public interface ISerializer
    {
        string Serialize(object obj);
        T Deserialize<T>(string value);
    }
}