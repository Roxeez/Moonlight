using System.Collections.Generic;
using Moonlight.Clients;
using Moonlight.Core;
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
        /// <summary>
        ///     Create a new character
        /// </summary>
        /// <param name="id">Character id (retrieved from c_info packet)</param>
        /// <param name="name">Character name (retrieved from c_info packet)</param>
        /// <param name="client">Client used for updating this character</param>
        /// <param name="miniland">Character own miniland</param>
        internal Character(long id, string name, Client client, Miniland miniland) : base(id, name)
        {
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

        public override byte HpPercentage => (byte)(Hp == 0 ? 0 : (double)Hp / MaxHp * 100);
        public override byte MpPercentage => (byte)(Mp == 0 ? 0 : (double)Mp / MaxMp * 100);

        public void Walk(Position position)
        {
            Moon.Walk(position.X, position.Y);
        }

        public void WalkInRange(Position position, int range)
        {
            int distance = Position.GetDistance(position);
            if (distance <= range)
            {
                return;
            }
            
            double distRatio = (distance - range) / (double)distance;
            double x = Position.X + distRatio * (position.X - Position.X);
            double y = Position.Y + distRatio * (position.Y - Position.Y);
            
            Walk(new Position((short)x, (short)y));
        }

        public void Attack(Skill skill)
        {
            
        }
        
        public void Attack(Skill skill, LivingEntity target)
        {
            
        }
    }
}