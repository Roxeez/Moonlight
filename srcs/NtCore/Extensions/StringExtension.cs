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
    }
}