using System;
using System.Collections.Generic;
using System.Linq;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Game.Battle;
using NtCore.Game.Relation;

namespace NtCore.Game.Entities.Impl
{
    public class Character : Player, ICharacter
    {
        public Character(IClient client)
        {
            Client = client;
            LastMapChange = DateTime.Now;
            Skills = new HashSet<ISkill>();
        }

        public IClient Client { get; }
        public int SpPoints { get; set; }
        public int AdditionalSpPoints { get; set; }
        public int MaximumSpPoints { get; set; }
        public int MaximumAdditionalSpPoints { get; set; }
        public int Gold { get; set; }
        public DateTime LastMapChange { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }
        public HashSet<ISkill> Skills { get; }
        public IEnumerable<IFriend> Friends { get; set; }

        public void UseSkill(ISkill skill)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }

            if (skill.Info.TargetingType != TargetingType.SELF && skill.Info.TargetingType != TargetingType.SELF_OR_TARGET)
            {
                return;
            }
            
            Client.SendPacket($"u_s {skill.Info.CastId} {(byte)EntityType} {Id}");
        }

        public void UseSkill(ISkill skill, ILivingEntity target)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }

            if (skill.Info.TargetingType == TargetingType.SELF)
            {
                return;
            }

            if (!Position.IsInRange(target.Position, skill.Info.Range + 4)) // Add some range because target can move
            {
                return;
            }
            
            Client.SendPacket($"u_s {skill.Info.CastId} {(byte)target.EntityType} {target.Id}");
        }

        public void UseSkill(ISkill skill, Position position)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }

            if (skill.Info.TargetingType != TargetingType.NO_TARGET)
            {
                return;
            }
            
            if (!Position.IsInRange(position, skill.Info.Range))
            {
                return;
            }
            
            Client.SendPacket($"u_as {skill.Info.CastId} {position.X} {position.Y}");
        }

        public byte JobLevel { get; set; }

        public void Move(Position position)
        {
            Client.SendPacket($"walk {position.X} {position.Y} 0 {Speed}");

            if (Client.Type == ClientType.LOCAL) // Trick for moving player (need to find something better)
            {
                Client.ReceivePacket($"tp {(byte)EntityType} {Id} {position.X} {position.Y} 0");
            }
        }

        public void SendFriendRequest(IPlayer player)
        {
            Client.SendPacket($"fins {(byte)player.EntityType} {player.Id}");
        }

        public void ShowInfoDialog(string message)
        {
            Client.ReceivePacket($"info {message}");
        }

        public void ReceiveMessage(string message, MessageType messageType)
        {
            Client.ReceivePacket($"msg {(byte)messageType} {message}");
        }

        public void ReceiveChatMessage(string message, ChatMessageColor messageColor)
        {
            Client.ReceivePacket($"say {(byte)EntityType} {Id} {(byte)messageColor} {message}");
        }

        public void ShowBubbleMessage(string message)
        {
            Client.ReceivePacket($"say {(byte)EntityType} {Id} 1 {message}");
        }

        public void ShowBubbleMessage(string message, ILivingEntity entity)
        {
            Client.ReceivePacket($"say {(byte)entity.EntityType} {entity.Id} 1 {message}");
        }
    }
}