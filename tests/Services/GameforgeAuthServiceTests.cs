using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFluent;
using NtCore.Core;
using NtCore.Serialization;
using NtCore.Services.Gameforge;
using NtCore.Services.Registry;
using Xunit;

namespace NtCore.Tests.Services
{
    public class GameforgeAuthServiceTests
    {
        private const string Username = "";
        private const string Password = "";
        private const string AccountName = "";
        
        private readonly IGameforgeAuthService _gameforgeAuthService;
        private readonly IRegistryReader _registryReader;
        
        public GameforgeAuthServiceTests()
        {
            _gameforgeAuthService = new GameforgeAuthService(new JsonSerializer());
            _registryReader = new RegistryReader();
        }

        [Fact]
        public async Task Service_Should_Return_Token()
        {
            Guid installationId = _registryReader.GetValue<Guid>(Microsoft.Win32.Registry.LocalMachine, "SOFTWARE\\WOW6432Node\\Gameforge4d\\TNTClient\\MainApp", "InstallationId").OrElse(Guid.NewGuid());
            
            Optional<string> authToken = await _gameforgeAuthService.GetAuthToken(Username, Password, Language.FR);
            Check.That(authToken.IsPresent()).IsTrue();

            Optional<IEnumerable<GameforgeAccount>> accounts = await _gameforgeAuthService.GetAllAccounts(authToken.Get(), installationId);
            Check.That(accounts.IsPresent()).IsTrue();
            
            GameforgeAccount account = accounts.Get().FirstOrDefault(x => x.Name == AccountName);
            Optional<string> token = await _gameforgeAuthService.GetSessionToken(authToken.Get(), account, installationId);
            Check.That(token.IsPresent());
        }
    }
}