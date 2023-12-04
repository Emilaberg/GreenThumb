using EntityFrameworkCore.EncryptColumn.Attribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        public int UserId { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [EncryptColumn]
        [Column("password")]
        public int Password { get; set; }

        [Column("garden_id")]
        public int GardenId { get; set; }
        public Garden? Garden { get; set; }
    }
}
