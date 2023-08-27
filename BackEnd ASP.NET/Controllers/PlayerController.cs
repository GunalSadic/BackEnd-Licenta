using BackEnd_ASP.NET.DTO;
using BackEnd_ASP.NET.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackEnd_ASP.NET.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private ApplicationDbContext context;

        public PlayerController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetAll(int page = 1, int pageSize = 8) {
            var totalItems = await this.context.Players.CountAsync();
            var players = await this.context.Players
                                    .OrderByDescending(p => p.Elo)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            var response = new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Players = players.Select(p => new PlayerDTO { Name = p.Name, Elo = p.Elo })
            };
            return Ok(response);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAvatar()
        {
            return Ok();
        }
    }
}
