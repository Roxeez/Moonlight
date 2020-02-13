using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Moonlight.Database.DAL;

namespace Moonlight.Database.Entities
{
    [Table("monsters")]
    public class Monster : IEntity<int>
    {
        [Required]
        public string NameKey { get; set; }

        [Required]
        public int Level { get; set; }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
    }
}