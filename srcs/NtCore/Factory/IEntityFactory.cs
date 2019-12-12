﻿using NtCore.Enums;
 using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;

namespace NtCore.Factory
{
    public interface IEntityFactory
    {
        Monster CreateMonster(int id, int vnum, Position position, byte direction, byte hpPercentage, byte mpPercentage);
        Npc CreateNpc(int id, int vnum, Position position, byte direction, byte hpPercentage, byte mpPercentage);
        Drop CreateDrop(int id, int vnum, int amount, Position position, IPlayer owner);
        Player CreatePlayer(int id, string name, byte level, ClassType classType, byte direction, Gender gender, Position position, byte hpPercentage, byte mpPercentage);
    }
}