using System.Linq;
using JetBrains.Annotations;
using NtCore.Game.Entities;
using NtCore.Game.Relation;

namespace NtCore.Extensions
{
    public static class CharacterExtension
    {
        [CanBeNull]
        public static IFriend FindFriendById<T>(this T character, int id) where T : ICharacter
        {
            return character.Friends.FirstOrDefault(x => x.Id == id);
        }

        [CanBeNull]
        public static IFriend FindFriendByName<T>(this T character, [NotNull] string name) where T : ICharacter
        {
            return character.Friends.FirstOrDefault(x => x.Name == name);
        }
    }
}