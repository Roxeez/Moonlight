using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NtCore.Enums;
using NtCore.Game.Battle;
using NtCore.Game.Relation;

namespace NtCore.Game.Entities
{
    public interface ICharacter : IPlayer
    {
        /// <summary>
        ///     Job level
        /// </summary>
        byte JobLevel { get; }

        /// <summary>
        ///     Current sp points
        /// </summary>
        int SpPoints { get; }

        /// <summary>
        ///     Current additional sp points
        /// </summary>
        int AdditionalSpPoints { get; }

        /// <summary>
        ///     Maximum sp points
        /// </summary>
        int MaximumSpPoints { get; }

        /// <summary>
        ///     Maximum additional sp points
        /// </summary>
        int MaximumAdditionalSpPoints { get; }

        /// <summary>
        ///     Current golds
        /// </summary>
        int Gold { get; }

        /// <summary>
        ///     Last time map changed
        /// </summary>
        DateTime LastMapChange { get; }

        /// <summary>
        ///     Current character hp
        /// </summary>
        int Hp { get; }

        /// <summary>
        ///     Character max hp
        /// </summary>
        int MaxHp { get; }

        /// <summary>
        ///     Current character mp
        /// </summary>
        int Mp { get; }

        /// <summary>
        ///     Character max mp
        /// </summary>
        int MaxMp { get; }

        /// <summary>
        ///     Current target
        /// </summary>
        ITarget Target { get; }

        /// <summary>
        /// Contains all character skills
        /// </summary>
        HashSet<ISkill> Skills { get; }
        
        IEnumerable<IFriend> Friends { get; }

        IFriend GetFriendByName(string name);
        IFriend GetFriendById(int id);
        
        /// <summary>
        /// Use selected skill on self
        /// </summary>
        /// <param name="skill">Skill to use</param>
        void UseSkill([NotNull] ISkill skill);
        
        /// <summary>
        /// Use skill on defined target
        /// </summary>
        /// <param name="skill">Skill to use</param>
        /// <param name="target">Target of the skill</param>
        void UseSkill([NotNull] ISkill skill, [NotNull] ILivingEntity target);

        /// <summary>
        /// Use skill at specific position
        /// </summary>
        /// <param name="skill">Skill to use</param>
        /// <param name="position">Target position</param>
        void UseSkill([NotNull] ISkill skill, Position position);
        
        /// <summary>
        ///     Make your character move
        /// </summary>
        /// <param name="position">Position where you want to move</param>
        void Move(Position position);

        /// <summary>
        /// Send a friend request to selected player
        /// </summary>
        /// <param name="player">Selected player</param>
        void SendFriendRequest(IPlayer player);

        /// <summary>
        /// Show info dialog with selected message
        /// </summary>
        /// <param name="message">Message to show</param>
        void ShowInfoDialog(string message);

        /// <summary>
        ///     Show a message to character (clientside only it's a received packet)
        /// </summary>
        /// <param name="message">Message to show</param>
        /// <param name="messageType">Type of message</param>
        void ReceiveMessage([NotNull] string message, MessageType messageType);

        /// <summary>
        ///     Show a chat message to character (clientside only it's a received packet)
        /// </summary>
        /// <param name="message">Message to show</param>
        /// <param name="messageColor">Type of message</param>
        void ReceiveChatMessage([NotNull] string message, ChatMessageColor messageColor);

        /// <summary>
        ///     Show a bubble message on top of character with selected message
        /// </summary>
        /// <param name="message">Message to show</param>
        void ShowBubbleMessage([NotNull] string message);

        /// <summary>
        ///     Show a bubble message on top of entity with selected message
        /// </summary>
        /// <param name="message">Message to show</param>
        /// <param name="entity">Entity where bubble need to be</param>
        void ShowBubbleMessage([NotNull] string message, [NotNull] ILivingEntity entity);
        
        
    }
}