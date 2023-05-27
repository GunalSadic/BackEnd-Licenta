using System.ComponentModel.DataAnnotations;
namespace BackEnd_ASP.NET.DTO
{
    public class UserCredentials
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }

    }
}
