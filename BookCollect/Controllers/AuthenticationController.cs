using BookCollect.Data;
using BookCollect.Models;
using BookCollect.Services.ViewModels.Authentications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCollect.Controllers
{
    [Route("api/authentications")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;


        public AuthenticationController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext appDbContext,
            IConfiguration configuration
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterVM payload)
        {
            //verify is the user email exist
            var userExists = await _userManager.FindByEmailAsync(payload.Email);

            //if the userExist is different from null
            if(userExists != null)
            {
                return BadRequest($"User {payload.Email} already exist");
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                Email = payload.Email,
                UserName = payload.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, payload.Password);
            if(!result.Succeeded)
            {
                return BadRequest("User could not be created!");
            }
            return Created(nameof(Register), $"User {payload.Email} created");

        }
    }
}
