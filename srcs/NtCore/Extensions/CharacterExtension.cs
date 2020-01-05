using System.Linq;
using JetBrains.Annotations;
using NtCore.Game.Entities;
using NtCore.Game.Relation;

namespace NtCore.Extensions
{
    public static class CharacterExtension
    {
        [CanBeNull]
        public static Friend FindFriendById(this Character character, int id)
        {
            return character.Friends.FirstOrDefault(x => x.Id == id);
        }

        [CanBeNull]
        public static Friend FindFriendByName(this Character character, [NotNull] string name)
        {
            return character.Friends.FirstOrDefault(x => x.Name == name);
        }
    }
}