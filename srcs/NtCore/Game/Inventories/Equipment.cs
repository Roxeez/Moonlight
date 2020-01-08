using JetBrains.Annotations;
using NtCore.Game.Items;

namespace NtCore.Game.Inventories
{
    public class Equipment
    {
        /// <summary>
        /// Main weapon currently equipped
        /// </summary>
        [CanBeNull]
        public Weapon MainWeapon { get; internal set; }
        
        /// <summary>
        /// Armor currently equipped
        /// </summary>
        [CanBeNull]
        public Armor Armor { get; internal set; }
        
        /// <summary>
        /// Secondary weapon currently equipped
        /// </summary>
        [CanBeNull]
        public Weapon SecondaryWeapon { get; internal set; }
        
        /// <summary>
        /// Fairy currently equipped
        /// </summary>
        [CanBeNull]
        public Fairy Fairy { get; internal set; }
    }
}