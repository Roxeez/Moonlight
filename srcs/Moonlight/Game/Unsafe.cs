using System.Threading.Tasks;
using Moonlight.Game.Entities;

namespace Moonlight.Game
{
    public class Unsafe
    {
        private readonly Character _character;
        
        public Unsafe(Character character)
        {
            _character = character;
        }
        
        public async Task UseLeverRemotely(GroundItem groundItem)
        {
            _character.Client.SendPacket($"#git^{groundItem.Id}");
        }
    }
}