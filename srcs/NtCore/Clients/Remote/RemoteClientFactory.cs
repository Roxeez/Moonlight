using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NtCore.Clients.Remote.Network;
using NtCore.Core;
using NtCore.Cryptography;
using NtCore.Extensions;
using NtCore.Services.Gameforge;
using NtCore.Services.Registry;

namespace NtCore.Clients.Remote
{
    public class RemoteClientFactory : IRemoteClientFactory
    {
        private const string RegistryPath = "SOFTWARE\\WOW6432Node\\Gameforge4d\\TNTClient\\MainApp";
        private const string RegistryKey = "InstallationId";
        private readonly IGameforgeAuthService _gameforgeAuthService;

        private readonly Random _random = new Random();
        private readonly IRegistryReader _registryReader;

        public RemoteClientFactory(IGameforgeAuthService gameforgeAuthService, IRegistryReader registryReader)
        {
            _gameforgeAuthService = gameforgeAuthService;
            _registryReader = registryReader;
        }

        public async Task<LoginResult> Login(string username, string password, ClientInformation clientInformation, LoginServer server, LoginType type)
        {
            INetworkClient client = new NetworkClient(new LoginEncryption());

            bool connected = await client.Connect(server.Ip, server.Port);
            if (!connected)
            {
                return new LoginResult(false);
            }

            Guid installationId = _registryReader.GetValue<Guid>(Microsoft.Win32.Registry.LocalMachine, RegistryPath, RegistryKey).OrElse(Guid.NewGuid());

            string packet;
            if (type == LoginType.NEW)
            {
                Optional<string> authToken = await _gameforgeAuthService.GetAuthToken(username, password, server.Language);
                if (!authToken.IsPresent())
                {
                    return new LoginResult(false);
                }

                Optional<IEnumerable<GameforgeAccount>> accounts = await _gameforgeAuthService.GetAllAccounts(authToken.Get(), installationId);
                if (!accounts.IsPresent())
                {
                    return new LoginResult(false);
                }

                Optional<string> token = await _gameforgeAuthService.GetSessionToken(authToken.Get(), accounts.Get().FirstOrDefault(), installationId);
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
                ;
            }
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