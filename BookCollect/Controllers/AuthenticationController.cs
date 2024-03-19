using BookCollect.Data;
using BookCollect.Models;
using BookCollect.Services.ViewModels;
using BookCollect.Services.ViewModels.Authentications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            //If the users do not provide the require field.
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }
            //verify is the user email exist
            var userExists = await _userManager.FindByEmailAsync(payload.Email);

            //if the userExist is different from null
            if(userExists != null)
            {
                return BadRequest($"User {payload.Email} already exist");
            }

            //when the user does not exist
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

        //Login and authorize user
        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody] LoginVM payload)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if(user != null & await _userManager.CheckPasswordAsync(user, payload.Password))
            {
                var tokenValue = await GenerateJwtToken(user);
                return Ok(tokenValue);
            }

            return Unauthorized();
        }



        private async Task<AuthResultVM> GenerateJwtToken(ApplicationUser user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(1), // 5 - 10mins
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()

            };
            await _appDbContext.RefreshTokens.AddAsync(refreshToken);
            await _appDbContext.SaveChangesAsync(); 

            var response = new AuthResultVM()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpireAt = token.ValidTo
            };
            return response;
        }

    }
}
