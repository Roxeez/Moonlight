using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Battle.Impl;
using NtCore.Game.Entities.Impl;
using NtCore.Network.Packets.Battle;

namespace NtCore.Network.Handlers.Battle
{
    public class StPacketHandler : PacketHandler<StPacket>
    {
        public override void Handle(IClient client, StPacket packet)
        {
            var target = client.Character.Target.As<Target>();

            if (target == null)
            {
                return;
            }
            
            var entity = target.Entity.As<LivingEntity>();
            
            target.Hp = packet.CurrentHp;
            target.Mp = packet.CurrentMp;
            entity.HpPercentage = packet.HpPercentage;
            entity.MpPercentage = packet.MpPercentage;
        }
    }
}