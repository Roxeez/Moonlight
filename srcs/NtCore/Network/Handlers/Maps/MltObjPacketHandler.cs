using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class MltObjPacketHandler : PacketHandler<MltObjPacket>
    {
        public override void Handle(IClient client, MltObjPacket packet)
        {
            if (!(client.Character.Map is Miniland miniland))
            {
                return;
            }

            foreach (MltObjSubPacket obj in packet.MinilandObjects)
            {
                miniland.AddMinilandObject(new MinilandObject(obj.Vnum, obj.Id, obj.Position));
            }
        }
    }
}