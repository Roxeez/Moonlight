using System.Security.Cryptography;
using System.Text;

namespace NtCore.Extensions
{
    public static class StringExtension
    {
        public static string ToHex(this string value)
        {
            var sb = new StringBuilder();

            byte[] bytes = Encoding.Default.GetBytes(value);
            foreach (byte t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string ToMd5(this string value)
        {
            using (var md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
                var sb = new StringBuilder();

                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static string ToSha512(this string value)
        {
            using (var md5 = SHA512.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
                var sb = new StringBuilder();

                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}