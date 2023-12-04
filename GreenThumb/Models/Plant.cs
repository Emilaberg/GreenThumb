using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    public class Plant
    {
        [Key]
        [Column("id")]
        public int PlantId { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;

        public List<Garden> Gardens { get; set; } = new();

        //one to many relaionship med Instructions
        public List<Instruction> Instructions { get; set; } = new();

    }
}
