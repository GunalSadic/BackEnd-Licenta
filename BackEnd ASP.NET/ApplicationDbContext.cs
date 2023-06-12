using BackEnd_ASP.NET.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BackEnd_ASP.NET
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext( [NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
