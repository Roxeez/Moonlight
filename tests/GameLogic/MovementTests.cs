using System;
using System.Diagnostics;
using System.Linq;
using Moq;
using NFluent;
using NtCore.Clients;
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
        private readonly IClient _client;

        public MovementTests()
        {
            var mock = new Mock<IClient>();
            
            mock.SetupGet(x => x.Character).Returns(new Character(mock.Object)
            {
                Speed = 10
            });

            _client = mock.Object;
        }

        [Fact]
        public async void Move_On_Allowed_Position()
        {
            Map map = new MapBuilder().WithId(1).Create();
            
            ICharacter character = _client.Character;
            character.As<Character>().Position = new Position(0, 0);
            
            map.AddEntity(character);

            short y = BitConverter.ToInt16(character.Map.Data.Take(2).ToArray(), 0);
            short x = BitConverter.ToInt16(character.Map.Data.Skip(2).Take(2).ToArray(), 0);
            
            Trace.WriteLine(y);
            Trace.WriteLine(x);
            
            bool moved = await character.Move(new Position(2, 3));
            
            Check.That(moved).IsTrue();
            Check.That(character.Position).IsEqualTo(new Position(2, 3));
        }

    }
}