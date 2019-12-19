using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace NtCore.Resources
{
    public static class Resource
    {
        public static T LoadJson<T>(string name)
        {
            var serializer = new JsonSerializer();
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NtCore.Resources." + name))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Can't load {name} from resources");
                }
                
                var reader = new StreamReader(stream);
                return (T)serializer.Deserialize(reader, typeof(T));
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

                using (var memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
        }
    }
}