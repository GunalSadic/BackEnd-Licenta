using System.ComponentModel.DataAnnotations;

namespace BackEnd_ASP.NET.DTO
{
    public class UserCredentials
    {
        [Required]
        public string Username { get; set; }
        [Required] 
        public string Password { get; set; }


    }
}
