using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Moonlight.Database.DAL;

namespace Moonlight.Database.Entities
{
    [Table("maps")]
    internal class Map : IEntity<int>
    {
        public string NameKey { get; set; }

        [Required]
        public byte[] Grid { get; set; }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
    }
}