using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Battle;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Battle;

namespace NtCore.Network.Handlers.Battle
{
    public class StPacketHandler : PacketHandler<StPacket>
    {
        public override void Handle(IClient client, StPacket packet)
        {
            Target target = client.Character.Target;

            if (target == null)
            {
                return;
            }

            LivingEntity entity = target.Entity;
            
            target.Hp = packet.CurrentHp;
            target.Mp = packet.CurrentMp;
            entity.HpPercentage = packet.HpPercentage;
            entity.MpPercentage = packet.MpPercentage;
        }
    }
}