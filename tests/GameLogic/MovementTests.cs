using System.Threading.Tasks;
using Moq;
using NFluent;
using NtCore.Clients;
using NtCore.Core;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps.Impl;
using NtCore.Tests.Utility;
using Xunit;

namespace NtCore.Tests.GameLogic
{
    public class MovementTests
    {
        public MovementTests()
        {
            var mock = new Mock<IClient>();

            mock.SetupGet(x => x.Type).Returns(ClientType.REMOTE); // Avoid error with NtNative

            mock.SetupGet(x => x.Character).Returns(new Character(mock.Object)
            {
                Speed = 10
            });

            _client = mock.Object;
        }

        private readonly IClient _client;

        [Fact]
        public async Task Move_On_Allowed_Position()
        {
            Map map = new MapBuilder().WithId(1).Create();

            ICharacter character = _client.Character;
            character.As<Character>().Position = new Position(0, 0);

            map.AddEntity(character);

            await character.Move(new Position(2, 3));

            Check.That(character.Position).IsEqualTo(new Position(2, 3));
        }
    }
}