using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BackEnd_ASP.NET.Entities
{
    public class Avatar
    {
        [Key]
        public int AvatarId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public Player User { get; set; }
    }
}
