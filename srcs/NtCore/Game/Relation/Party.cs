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

        /// <summary>
        /// Owner of the party
        /// </summary>
        public Player Owner { get; }
        
        /// <summary>
        /// All entities in party (pets/players)
        /// </summary>
        public IEnumerable<LivingEntity> Members { get; }
    }
}