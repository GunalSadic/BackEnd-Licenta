using BackEnd_ASP.NET.DTO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BackEnd_ASP.NET
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext( [NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserCredentials> Users { get; set; }
    }
}
