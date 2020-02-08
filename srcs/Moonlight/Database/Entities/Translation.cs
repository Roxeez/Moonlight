using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Moonlight.Database.DAL;

namespace Moonlight.Database.Entities
{
    [Table("translations")]
    internal class Translation : IEntity<string>
    {
        [Required]
        public string Value { get; set; }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
    }
}