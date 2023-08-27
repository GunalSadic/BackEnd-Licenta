using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_ASP.NET.Entities
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        [ForeignKey("WinningPlayer")]
        public int WinningPlayerId { get; set; }

        public virtual Player WinningPlayer { get; set; }

        [ForeignKey("LosingPlayer")]
        public int LosingPlayerId { get; set; }

        public virtual Player LosingPlayer { get; set; }

        public bool IsDraw { get; set; }
    }
}
