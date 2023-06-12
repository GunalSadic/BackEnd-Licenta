using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_ASP.NET.Entities
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [ForeignKey("AspNetUser")]
        public string AspNetUserId { get; set; }
        public Avatar Avatar { get; set; }

        public int Elo { get; set; }

    }
}
