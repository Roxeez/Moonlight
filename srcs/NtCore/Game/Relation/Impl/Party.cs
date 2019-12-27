using System.Collections.Generic;
using NtCore.Game.Entities;

namespace NtCore.Game.Relation.Impl
{
    public class Party : IParty
    {
        public Party(IPlayer owner, IEnumerable<ILivingEntity> members)
        {
            Owner = owner;
            Members = members;
        }

        public IPlayer Owner { get; }
        public IEnumerable<ILivingEntity> Members { get; }
    }
}