using System;
using System.Threading.Tasks;
using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Core.Enums;
using Moonlight.Core.Import;
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
        internal Character(long id, string name, Client client, Miniland miniland) : base(id, name)
        {
            Client = client;
            Inventory = new Inventory(this);
            Miniland = miniland;
            Skills = new SafeObservableCollection<Skill>();
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
        ///     Represent character global inventory (gold, bags etc...)
        /// </summary>
        public Inventory Inventory { get; }

        /// <summary>
        ///     Current character gold
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        ///     Current sp points
        /// </summary>
        public int SpPoints { get; internal set; }

        /// <summary>
        ///     Current sp points
        /// </summary>
        public int AdditionalSpPoints { get; internal set; }

        /// <summary>
        ///     Current character production points
        /// </summary>
        public short ProductionPoints { get; internal set; }

        /// <summary>
        ///     Current character skills
        /// </summary>
        public SafeObservableCollection<Skill> Skills { get; }

        internal DateTime LastMovement { get; set; }

        public override byte HpPercentage => (byte)(Hp == 0 ? 0 : (double)Hp / MaxHp * 100);
        public override byte MpPercentage => (byte)(Mp == 0 ? 0 : (double)Mp / MaxMp * 100);

        /// <summary>
        ///     Walk to the specified position
        /// </summary>
        /// <param name="position">Position where you want to walk</param>
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
                await Task.Delay(100).ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     Walk to until in range of the specific position
        /// </summary>
        /// <param name="position">Position where you want to walk</param>
        /// <param name="range">Range wanted</param>
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

            await Walk(new Position((short)x, (short)y)).ConfigureAwait(false);
        }

        /// <summary>
        ///     Use a skill on yourself
        ///     Skill wont be used if
        ///     - Skill is not in your skills
        ///     - Skill is on cooldown
        ///     - If this skill can't target you
        /// </summary>
        /// <param name="skill">Skill to use</param>
        public async Task UseSkill(Skill skill)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }

            if (skill.IsOnCooldown)
            {
                return;
            }

            if (skill.TargetType == TargetType.TARGET || skill.TargetType == TargetType.NO_TARGET)
            {
                return;
            }

            Client.SendPacket($"u_s {skill.CastId} {(int)EntityType} {Id}");
            await Task.Delay(skill.CastTime * 100).ConfigureAwait(false);
        }

        /// <summary>
        ///     Use a skill on target (walk to target if not in range)
        ///     Skill wont be used if
        ///     - Skill is not in your skills
        ///     - Skill is on cooldown
        ///     - If this skill has no target
        /// </summary>
        /// <param name="skill">Skill to use</param>
        /// <param name="target">Target to hit with skill</param>
        public async Task UseSkillOn(Skill skill, LivingEntity target)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }

            if (skill.IsOnCooldown)
            {
                return;
            }

            if (skill.TargetType == TargetType.SELF)
            {
                await UseSkill(skill).ConfigureAwait(false);
                return;
            }

            if (skill.TargetType == TargetType.NO_TARGET)
            {
                return;
            }

            await WalkInRange(target.Position, skill.Range).ConfigureAwait(false);
            Client.SendPacket($"u_s {skill.CastId} {(int)target.EntityType} {target.Id}");
            await Task.Delay(skill.CastTime * 100).ConfigureAwait(false);
        }

        /// <summary>
        ///     Use a skill at position (walk to position range if not in range)
        ///     Skill wont be used if
        ///     - Skill is not in your skills
        ///     - Skill is on cooldown
        ///     - Skill need a target
        /// </summary>
        /// <param name="skill">Skill to use</param>
        /// <param name="position">Position where you want to use the skill</param>
        public async Task UseSkillAt(Skill skill, Position position)
        {
            if (!Skills.Contains(skill))
            {
                return;
            }

            if (skill.IsOnCooldown)
            {
                return;
            }

            if (skill.TargetType != TargetType.NO_TARGET)
            {
                return;
            }

            await WalkInRange(position, skill.Range).ConfigureAwait(false);
            Client.SendPacket($"u_as {skill.CastId} {position.X} {position.Y}");
            await Task.Delay(skill.CastTime * 100).ConfigureAwait(false);
        }

        /// <summary>
        ///     Pickup a drop (walk to drop if not in range)
        /// </summary>
        /// <param name="drop">Drop to pickup</param>
        public async Task PickUp(Drop drop)
        {
            await WalkInRange(drop.Position, 1).ConfigureAwait(false);
            Client.SendPacket($"get {(byte)EntityType} {Id} {drop.Id}");
        }
    }
}