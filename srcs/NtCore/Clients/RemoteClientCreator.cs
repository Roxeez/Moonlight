using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NtCore.Clients.Cryptography;

namespace NtCore.Clients
{
    public class RemoteClientCreator
    {
        private NetworkClient _networkClient;
        private readonly RemoteClientInfo _remoteClientInfo;

        public RemoteClientCreator(ClientInfo clientInfo) =>
            _remoteClientInfo = new RemoteClientInfo
            {
                ClientInfo = clientInfo
            };

        public async Task<LoginStep> Login(string username, string password, IPEndPoint login)
        {
            _networkClient = new NetworkClient(login, new LoginEncryption());

            // TODO : send login packet
            await _networkClient.SendPacket("");

            IEnumerable<string> response = await _networkClient.ReceivePackets();

            if (response.Any(x => x.StartsWith("failc")))
            {
                return new LoginStep(false);
            }
            
            
            
            _remoteClientInfo.Username = username;
            _remoteClientInfo.Password = password;

            return new LoginStep(true);
        }

        public RemoteClientInfo Create() => _remoteClientInfo;
    }

    public class LoginStep
    {
        public bool IsConnected { get; }
        public List<IPEndPoint> Servers { get; }

        public LoginStep(bool isConnected)
        {
            IsConnected = isConnected;
            Servers = new List<IPEndPoint>();
        }
        
        public LoginStep(bool isConnected, List<IPEndPoint> servers)
        {
            IsConnected = isConnected;
            Servers = servers;
        }
    }
}