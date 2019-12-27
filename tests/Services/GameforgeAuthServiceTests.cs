using System;
using System.Threading.Tasks;
using NFluent;
using NtCore.Core;
using NtCore.Serialization;
using NtCore.Services.Gameforge;
using Xunit;

namespace NtCore.Tests.Services
{
    public class GameforgeAuthServiceTests
    {
        private const string Username = "";
        private const string Password = "";
        
        private readonly IGameforgeAuthService _gameforgeAuthService;
        
        public GameforgeAuthServiceTests()
        {
            _gameforgeAuthService = new GameforgeAuthService(new JsonSerializer());
        }

        [Fact]
        public async Task Service_Should_Return_Token()
        {
            Optional<GameforgeAccount> account = await _gameforgeAuthService.Connect(Username, Password, Language.FR);
            Check.That(account.IsPresent()).IsTrue();
            
            Optional<string> token = await _gameforgeAuthService.GetToken(account.Get(), Guid.NewGuid());
            Check.That(token.IsPresent()).IsTrue();
        }
    }
}