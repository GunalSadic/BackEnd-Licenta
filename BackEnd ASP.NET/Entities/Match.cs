using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_ASP.NET.Entities
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        [ForeignKey("User")]
        public int WinningUserId { get; set; }

        [ForeignKey("User")]
        public int LosingUserId { get; set; }
    }
}
