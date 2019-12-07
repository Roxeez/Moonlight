using NtCore.API.Clients;
using NtCore.API.Extensions;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class MltObjPacketHandler : PacketHandler<MltObjPacket>
    {
        public override void Handle(IClient client, MltObjPacket packet)
        {
            var miniland = client.Character.Map.As<Miniland>();

            if (miniland == null)
            {
                return;
            }

            foreach (MltObjSubPacket obj in packet.MinilandObjects)
            {
                miniland.AddMinilandObject(new MinilandObject
                {
                    Vnum = obj.Vnum,
                    Id = obj.Id,
                    Position = obj.Position
                });
            }
        }
    }
}