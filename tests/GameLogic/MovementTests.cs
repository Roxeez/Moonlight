using Moq;
using NFluent;
using NtCore.Clients;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps.Impl;
using NtCore.Tests.Utility;
using Xunit;

namespace NtCore.Tests.GameLogic
{
    public class MovementTests
    {
        private readonly IClient _client;

        public MovementTests()
        {
            var mock = new Mock<IClient>();
            
            mock.SetupGet(x => x.Character).Returns(new Character(mock.Object));

            _client = mock.Object;
        }

        [Fact]
        public async void Move_On_Allowed_Position()
        {
            Map map = new MapBuilder().WithId(1).Create();
            
            ICharacter character = _client.Character;
            
            map.AddEntity(character);

            bool moved = await character.Move(new Position(2, 3));
            
            Check.That(moved).IsTrue();
            Check.That(character.Position).IsEqualTo(new Position(5, 4));
        }

    }
}