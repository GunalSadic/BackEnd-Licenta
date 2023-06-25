using System.ComponentModel.DataAnnotations;

namespace BackEnd_ASP.NET.DTO
{
    public class RegistrationData
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string Username { get; set; }
    }
}
