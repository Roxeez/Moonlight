using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NtCore.Game.Entities;
using NtCore.Game.Relation;

namespace NtCore.Extensions
{
    public static class CharacterExtension
    {
        /// <summary>
        /// Get a friend by id
        /// </summary>
        /// <param name="character">character</param>
        /// <param name="id">Id of the friend</param>
        /// <returns>Friend found or null if none</returns>
        [CanBeNull]
        public static Friend FindFriendById(this Character character, int id)
        {
            return character.Friends.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Get a friend by name
        /// </summary>
        /// <param name="character">character</param>
        /// <param name="name">Name of the friend</param>
        /// <returns>Friend found or null if none</returns>
        [CanBeNull]
        public static Friend FindFriendByName(this Character character, [NotNull] string name)
        {
            return character.Friends.FirstOrDefault(x => x.Name == name);
        }
    }
}