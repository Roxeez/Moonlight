using System.Collections.Generic;
using NtCore.Game.Entities;

namespace NtCore.Game.Relation
{
    public interface IParty
    {
        IPlayer Owner { get; }
        IEnumerable<ILivingEntity> Members { get; }
    }
}