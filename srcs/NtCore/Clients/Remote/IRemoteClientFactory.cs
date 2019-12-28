using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtCore.Clients.Remote
{
    public interface IRemoteClientFactory
    {
        Task<LoginResult> Login(string username, string password, ClientInformation clientInformation, LoginServer server, LoginType type);
    }

    public class LoginResult
    {
        public bool IsConnected { get; }
        public IEnumerable<WorldServer> WorldServers { get; }

        public LoginResult(bool isConnected) => IsConnected = isConnected;

        public LoginResult(bool isConnected, IEnumerable<WorldServer> worldServers)
        {
            IsConnected = isConnected;
            WorldServers = worldServers;
        }
    }
}