using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Core.Enums;
using Moonlight.Core.Import;
using Moonlight.Core.Logging;
using Moonlight.Game.Battle;
using Moonlight.Game.Inventories;
using Moonlight.Game.Maps;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent the Character of a session
    ///     A character can be controlled not like a Player entity
    /// </summary>
    public class Character : Player
    {
        private readonly ILogger _logger;

        internal Character(long id, string name, Client client, Miniland miniland, ILogger logger) : base(id, name)
        {
            _logger = logger;
            
            Client = client;
            Inventory = new Inventory();
            Miniland = miniland;
        }

        /// <summary>
        ///     Client used for updating values of this Character
        /// </summary>
        public Client Client { get; }

        /// <summary>
        ///     Player job level
        /// </summary>
        public byte JobLevel { get; internal set; }

        /// <summary>
        ///     Current player Mp
        /// </summary>
        public int Hp { get; internal set; }

        /// <summary>
        ///     Maximum player Mp
        /// </summary>
        public int MaxHp { get; internal set; }

        /// <summary>
        ///     Current player Mp
        /// </summary>
        public int Mp { get; internal set; }

        /// <summary>
        ///     Maximum player Mp
        /// </summary>
        public int MaxMp { get; internal set; }

        /// <summary>
        ///     Character own miniland
        /// </summary>
        public Miniland Miniland { get; }

        /// <summary>
        ///     Character inventory
        /// </summary>
        public Inventory Inventory { get; }

        /// <summary>
        ///     Current sp points
        /// </summary>
        public int SpPoints { get; internal set; }

        /// <summary>
        ///     Current character production points
        /// </summary>
        public short ProductionPoints { get; internal set; }
        
        /// <summary>
        /// Current character skills
        /// </summary>
        public IEnumerable<Skill> Skills { get; internal set; }
        
        internal DateTime LastMovement { get; set; }

        public override byte HpPercentage => (byte)(Hp == 0 ? 0 : (double)Hp / MaxHp * 100);
        public override byte MpPercentage => (byte)(Mp == 0 ? 0 : (double)Mp / MaxMp * 100);

        public async Task Walk(Position position)
        {
            if (Position.Equals(position))
            {
                return;
            }

            Moon.Walk(position.X, position.Y);
            LastMovement = DateTime.Now;
            
            while (LastMovement.AddSeconds(1) < DateTime.Now)
            {
                await Task.Delay(100);
            }
        }

        public async Task WalkInRange(Position position, int range)
        {
            if (Position.IsInRange(position, range))
            {
                return;
            }
            
            int distance = Position.GetDistance(position);
            if (distance <= range)
            {
                return;
            }
            
            double distRatio = (distance - range) / (double)distance;
            double x = Position.X + distRatio * (position.X - Position.X);
            double y = Position.Y + distRatio * (position.Y - Position.Y);
            
            await Walk(new Position((short)x, (short)y));
        }

        public async Task Attack(Skill skill)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }
            
            if (skill.TargetType == TargetType.TARGET || skill.TargetType == TargetType.NO_TARGET)
            {
                return;
            }

            Client.SendPacket($"u_s {skill.CastId} {(int)EntityType} {Id}");
            skill.IsOnCooldown = true;
            await Task.Delay(skill.CastTime);
        }

        public async Task Attack(Skill skill, LivingEntity target)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }
            
            if (skill.TargetType == TargetType.SELF || skill.TargetType == TargetType.NO_TARGET)
            {
                return;
            }

            await WalkInRange(target.Position, skill.Range);
            Client.SendPacket($"u_s {skill.CastId} {(int)target.EntityType} {target.Id}");
            skill.IsOnCooldown = true;
            await Task.Delay(skill.CastTime);
        }

        public async Task Attack(Skill skill, Position position)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }
            
            if (skill.TargetType != TargetType.NO_TARGET)
            {
                return;
            }
        
            await WalkInRange(position, skill.Range);
            Client.SendPacket($"u_as {skill.CastId} {position.X} {position.Y}");
            skill.IsOnCooldown = true;
            await Task.Delay(skill.CastTime);
        }

        public async Task PickUp(Drop drop)
        {
            await WalkInRange(drop.Position, 1);
            Client.SendPacket($"get {(byte)EntityType} {Id} {drop.Id}");
        }
    }
}