using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    public class Garden
    {
        [Key]
        [Column("id")]
        public int GardenId { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("size")]
        public int? Size { get; set; }

        [Column("level")]
        public int Level { get; set; }

        //One to one relationship med usern

        [Column("user_id")]
        public int UserId { get; set; }
        public User? User { get; set; }

        //One to one relationship med usern

        //Many to many relaionship med plants

        public List<Plant> Plants { get; set; } = new();

        //Many to many relaionship med plants
    }
}
