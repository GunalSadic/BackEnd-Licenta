namespace BackEnd_ASP.NET.DTO
{
    public class AuthenticationResponse
    {
        public String Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
