using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;

namespace NtCore.Factory
{
    public interface IEntityFactory
    {
        Monster CreateMonster(int vnum);
        Npc CreateNpc(int vnum);
        Drop CreateDrop(int vnum);
    }
}