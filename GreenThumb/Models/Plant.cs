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

        [Column("instruction_id")]
        public int InstructionId { get; set; }
        public Instruction? Instruction { get; set; }

        public List<Garden> Gardens { get; set; } = new();
    }
}
