using NtCore.Clients;
using NtCore.Enums;

namespace NtCore.Extensions
{
    public static class ClientExtension
    {
        public static bool IsLocal(this IClient client) => client.Type == ClientType.LOCAL;
    }
}