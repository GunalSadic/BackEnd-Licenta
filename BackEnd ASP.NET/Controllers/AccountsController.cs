using BackEnd_ASP.NET.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, 
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;

        }

        //[HttpPost("create")]
        //public async Task<ActionResult<AuthenticationResponse>> Create(
        //    [FromBody] UserCredentials userCredentials)
        //{
        //    var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };
        //    var result = await userManager.CreateAsync(user, userCredentials.Password);
        //    if (result.Succeeded) { 
            
        //    }
        //    else
        //        return BadRequest(result.Errors);
        //}

        //private AuthenticationResponse BuildToken(UserCredentials userCredentials)
        //{
        //    var Claims = new List<Claim>();
        //    {
        //        new Claim("email", userCredentials.Email);
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes();
        //}
    }
}
