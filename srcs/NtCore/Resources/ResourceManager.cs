using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using NtCore.Serialization;

namespace NtCore.Resources
{
    public class ResourceManager
    {
        private readonly ISerializer _serializer;
        public ResourceManager(ISerializer serializer)
        {
            _serializer = serializer;
        }
        
        public T Load<T>(string name)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NtCore.Resources." + name))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Can't load {name} from resources");
                }
                
                var reader = new StreamReader(stream);
                return _serializer.Deserialize<T>(reader.ReadToEnd());
            }
        }

        public static byte[] Read(string name)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NtCore.Resources." + name))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Can't load {name} from resources");
                }
                
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }
    }
}