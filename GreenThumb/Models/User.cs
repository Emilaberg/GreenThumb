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

        [Column("password")]
        [EncryptColumn]
        public string Password { get; set; } = null!;

        public Garden? Garden { get; set; }
    }
}
