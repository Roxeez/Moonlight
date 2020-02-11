using Moonlight.Event;
using Moonlight.Event.Maps;

namespace Moonlight.Tests.Events
{
    public class EntityLeaveTestListener : EventListener<EntityLeaveEvent>
    {
        private readonly TestObject _testObject;
        
        public EntityLeaveTestListener(TestObject testObject)
        {
            _testObject = testObject;
        }
        
        protected override void Handle(EntityLeaveEvent notification)
        {
            _testObject.EntityLeaveId = notification.Entity.Id;
        }
    }
}