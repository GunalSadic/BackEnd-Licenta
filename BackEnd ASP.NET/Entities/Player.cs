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
        [ForeignKey("AspNetUser")]
        public string AspNetUserId { get; set; }
        public Avatar Avatar { get; set; }

        public int Elo { get; set; }
        public string Email { get; set; }
        public int GamesPlayed { get; set; }

    }
}
