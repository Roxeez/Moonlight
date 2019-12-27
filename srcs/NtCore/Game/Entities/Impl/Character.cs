using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NtCore.Clients;
using NtCore.Core;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Battle;
using NtCore.Game.Inventories;
using NtCore.Game.Inventories.Impl;
using NtCore.Game.Relation;
using NtCore.Import;

namespace NtCore.Game.Entities.Impl
{
    public class Character : Player, ICharacter
    {
        public Character(IClient client)
        {
            Client = client;
            LastMapChange = DateTime.Now;
            Equipment = new Equipment();
            Skills = new HashSet<ISkill>();
            Friends = new List<IFriend>();
        }

        private IClient Client { get; }
        public IEquipment Equipment { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }
        public IParty Party { get; set; }
        public byte JobLevel { get; set; }
        public int SpPoints { get; set; }
        public int AdditionalSpPoints { get; set; }
        public int MaximumSpPoints { get; set; }
        public int MaximumAdditionalSpPoints { get; set; }
        public int Gold { get; set; }
        public DateTime LastMapChange { get; set; }
        public HashSet<ISkill> Skills { get; }
        public IEnumerable<IFriend> Friends { get; set; }

        public async Task UseSkill(ISkill skill)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }

            if (skill.Info.TargetingType != TargetingType.SELF && skill.Info.TargetingType != TargetingType.SELF_OR_TARGET)
            {
                return;
            }

            await Client.SendPacket($"u_s {skill.Info.CastId} {(byte)EntityType} {Id}");
        }

        public async Task UseSkill(ISkill skill, ILivingEntity target)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }

            if (skill.Info.TargetingType == TargetingType.SELF)
            {
                return;
            }

            if (!Position.IsInRange(target.Position, skill.Info.Range + 4))
            {
                return;
            }

            await Client.SendPacket($"u_s {skill.Info.CastId} {(byte)target.EntityType} {target.Id}");
        }

        public async Task UseSkill(ISkill skill, Position position)
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

            await Client.SendPacket($"u_as {skill.Info.CastId} {position.X} {position.Y}");
        }

        public async Task PickUp(IDrop drop)
        {
            if (drop.Owner != null && !drop.Owner.Equals(this))
            {
                return;
            }

            if (!drop.Position.IsInArea(Position, 2))
            {
                return;
            }

            await Client.SendPacket($"get {(byte)EntityType} {Id} {drop.Id}");
        }

        public async Task Move(Position destination)
        {
            if (!Map.IsWalkable(destination))
            {
                return;
            }

            bool positiveX = destination.X > Position.X;
            bool positiveY = destination.Y > Position.Y;

            while (!Position.Equals(destination))
            {
                var position = new Position(Position.X, Position.Y);

                int distanceX = position.GetDistanceX(destination);
                int distanceY = position.GetDistanceY(destination);

                int stepX = distanceX >= 5 ? 5 : distanceX;
                int stepY = distanceY >= 5 ? 5 : distanceY;

                position.X = (short)((positiveX ? 1 : -1) * stepX + position.X);
                position.Y = (short)((positiveY ? 1 : -1) * stepY + position.Y);

                if (!Map.IsWalkable(position))
                {
                    return;
                }

                if (Client.IsLocal())
                {
                    NtNative.Walk(position.X, position.Y);
                }
                else
                {
                    await Client.SendPacket($"walk {position.X} {position.Y} {(position.X + position.Y) % 3 % 2} {Speed}");
                }

                await Task.Delay((stepX + stepY) * (1200 / Speed)).ConfigureAwait(false);
                Position = position;
            }
        }

        public async Task SendFriendRequest(IPlayer player)
        {
            await Client.SendPacket($"fins {(byte)player.EntityType} {player.Id}");
        }

        public async Task ShowInfoDialog(string message)
        {
            await Client.ReceivePacket($"info {message}");
        }

        public async Task ReceiveMessage(string message, MessageType messageType)
        {
            await Client.ReceivePacket($"msg {(byte)messageType} {message}");
        }

        public async Task ReceiveChatMessage(string message, ChatMessageColor messageColor)
        {
            await Client.ReceivePacket($"say {(byte)EntityType} {Id} {(byte)messageColor} {message}");
        }

        public async Task ShowBubbleMessage(string message)
        {
            await Client.ReceivePacket($"say {(byte)EntityType} {Id} 1 {message}");
        }

        public async Task ShowBubbleMessage(string message, ILivingEntity entity)
        {
            await Client.ReceivePacket($"say {(byte)entity.EntityType} {entity.Id} 1 {message}");
        }
    }
}