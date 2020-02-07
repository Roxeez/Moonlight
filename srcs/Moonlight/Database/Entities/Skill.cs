using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    }
}