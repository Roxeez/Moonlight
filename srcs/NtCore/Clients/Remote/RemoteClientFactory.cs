using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NtCore.Clients.Remote.Network;
using NtCore.Core;
using NtCore.Cryptography;
using NtCore.Extensions;
using NtCore.Services.Gameforge;

namespace NtCore.Clients.Remote
{
    public class RemoteClientFactory : IRemoteClientFactory
    {
        private readonly Random _random = new Random();
        private readonly IGameforgeAuthService _gameforgeAuthService;
        
        public RemoteClientFactory(IGameforgeAuthService gameforgeAuthService)
        {
            _gameforgeAuthService = gameforgeAuthService;
        }

        public async Task<LoginResult> Login(string username, string password, ClientInformation clientInformation, LoginServer server, LoginType type)
        {
            INetworkClient client = new NetworkClient(new LoginEncryption());

            bool connected = await client.Connect(server.Ip, server.Port);
            if (!connected)
            {
                return new LoginResult(false);
            }
            
            Guid installationId = Guid.NewGuid(); // TODO : Use registry reader
            string packet;
            if (type == LoginType.NEW)
            {
                Optional<GameforgeAccount> account = await _gameforgeAuthService.Connect(username, password, server.Language);
                if (!account.IsPresent())
                {
                    return new LoginResult(false);
                }

                Optional<string> token = await _gameforgeAuthService.GetToken(account.Get(), installationId);
                if (!token.IsPresent())
                {
                    return new LoginResult(false);
                }
                
                string version = "00" + _random.Next(0, 126).ToString("X")
                    + _random.Next(0, 126).ToString("X")
                    + _random.Next(0, 126).ToString("X")
                    + $"{(char)0xB}" + clientInformation.Version;
                
                string fileHash = (clientInformation.DxHash.ToUpper() + clientInformation.GlHash.ToUpper()).ToMd5();
                
                packet = $"NoS0577 {token.Get().ToHex()} {installationId.ToString()} {version} 0 {fileHash}";
;            }
            else
            {
                string usernameHash = (clientInformation.DxHash.ToUpper() + clientInformation.GlHash.ToUpper() + username).ToMd5();
                string passwordHash = password.ToSha512();

                string version = "00" + _random.Next(0, 126).ToString("X")
                    + _random.Next(0, 126).ToString("X")
                    + _random.Next(0, 126).ToString("X")
                    + "\v" + clientInformation.Version;

                packet = $"NoS0575 7040266 {username} {passwordHash} {installationId.ToString()} {version} 0 {usernameHash}";
            }

            await client.SendPacket(packet);

            IEnumerable<string> packets = await client.ReceivePackets();
            if (packets.Any(x => x.Contains("failc")))
            {
                return new LoginResult(false);
            }
            
            return new LoginResult(true, new WorldServer[0]);
        }
    }
}