using Moonlight.Database.Dto;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;
using Moonlight.Tests.Extensions;
using Moonlight.Tests.Handling;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Events
{
    public class ListenerTest : PacketHandlingTest
    {
        private readonly TestObject _testObject;
        
        public ListenerTest()
        {
            _testObject = new TestObject
            {
                EntityLeaveId = 0
            };
            
            Moonlight.AddListener(new EntityLeaveTestListener(_testObject));
        }
        
        [Fact]
        public void Test_Entity_Leave_Event_Listener()
        {
            Map map = Character.SetFakeMap();
            
            map.AddEntity(new Monster(1000, new MonsterDto(), "dummy"));
            
            Client.ReceivePacket("out 3 1000");

            Check.That(_testObject.EntityLeaveId).Is(1000);
        }
    }
}