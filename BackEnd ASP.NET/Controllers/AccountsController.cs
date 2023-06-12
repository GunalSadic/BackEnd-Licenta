﻿using BackEnd_ASP.NET.DTO;
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

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;

        }

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Create(
            [FromBody] UserCredentials userCredentials)
            {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };
            var result = await userManager.CreateAsync(user, userCredentials.Password);
            if (result.Succeeded)
            {
                return BuildToken(userCredentials);
            }
            else
                return BadRequest(result.Errors);
        }

        private AuthenticationResponse BuildToken(UserCredentials userCredentials)
        {
            var Claims = new List<Claim>()
                {
                    new Claim("email", userCredentials.Email)
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

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(
            [FromBody] UserCredentials userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password,
                isPersistent: false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
               return BuildToken(userCredentials);
            }
            else
            {
                return BadRequest("Incorrect Login");
            }
        }
    }
}
