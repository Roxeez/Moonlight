using System.Collections.Generic;
using NtCore.Game.Entities;

namespace NtCore.Game.Relation
{
    public class Party
    {
        public Party(Player owner, IEnumerable<LivingEntity> members)
        {
            Owner = owner;
            Members = members;
        }

        public Player Owner { get; }
        public IEnumerable<LivingEntity> Members { get; }
    }
}