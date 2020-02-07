using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Moonlight.Core.Enums;
using Moonlight.Database.DAL;

namespace Moonlight.Database.Entities
{
    [Table("skills")]
    internal class Skill : IEntity<int>
    {
        [Required]
        public string NameKey { get; set; }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        public short Range { get; set; }
        
        public int Cooldown { get; set; }
        
        public SkillType SkillType { get; set; }
        
        public int MpCost { get; set; }
        
        public int CastId { get; set; }
        
        public TargetType TargetType { get; set; }
    }
}