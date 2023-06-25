using BackEnd_ASP.NET.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd_ASP.NET.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;

        
        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, ApplicationDbContext context)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;

        }

        #region Endpoints

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Create(
            [FromBody] RegistrationData registrationData)
            {
            var user = new IdentityUser { UserName = registrationData.Username, Email = registrationData.Email};
            var result = await userManager.CreateAsync(user, registrationData.Password);
            if (result.Succeeded)
            {
                context.Players.Add(new Entities.Player() { 
                    AspNetUserId = user.Id,
                    Elo = 1201,
                    GamesPlayed = 0,
                    Email = user.Email
                });
                context.SaveChanges();
                return BuildToken(registrationData);
            }
            else
                return BadRequest(result.Errors);
        }

       

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(
            [FromBody] UserCredentials userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.Username, userCredentials.Password,
                isPersistent: false, lockoutOnFailure: true);
            
            if (result.Succeeded)
            {
                var email = context.Users.Where(x => x.UserName == userCredentials.Username).Select(x => x.Email).FirstOrDefault(); 
               return BuildToken(
                   new RegistrationData(
                       )
                   {
                      Username = userCredentials.Username, 
                      Password = userCredentials.Password,
                      Email = email
                   }
                   );;
            }
            else
            {
                return BadRequest("Incorrect Login");
            }
        }
        #endregion

        private AuthenticationResponse BuildToken(RegistrationData registrationData)
        {
            var Claims = new List<Claim>()
                {
                    new Claim("email", registrationData.Email),
                    new Claim("username", registrationData.Username)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiraton = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(issuer: null, audience: null,
                claims: Claims, expires: expiraton, signingCredentials: creds);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiraton
            };
        }
    }
}
