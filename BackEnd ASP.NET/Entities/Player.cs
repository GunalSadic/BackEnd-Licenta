using Microsoft.AspNetCore.Identity;
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
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public string? AvatarURL { get; set; }
        public int Elo { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int GamesPlayed { get; set; }

    }
}
