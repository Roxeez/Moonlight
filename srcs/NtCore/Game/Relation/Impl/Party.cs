using System.Collections.Generic;
using NtCore.Game.Entities;

namespace NtCore.Game.Relation.Impl
{
    public class Party : IParty
    {
        public IPlayer Owner { get; }
        public IEnumerable<ILivingEntity> Members { get; }

        public Party(IPlayer owner, IEnumerable<ILivingEntity> members)
        {
            Owner = owner;
            Members = members;
        }
    }
}