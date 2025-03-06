using Microsoft.AspNetCore.Mvc;
using ExClockify.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ExClockify.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ExClockifyContext _repository;
        private readonly IConfiguration _configuration;
        
        public AccountController(ExClockifyContext repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        
        // this is currently similar to our getTasks endpoint, as both return tasks
        // but we might change the User model later, so its better to have another
        // endpoint for getting user info
        [HttpGet("UserInfo")]
        [Authorize]
        public async Task<ActionResult> GetUserInfo(string deviceId)
        {
            var UserInfo = await _repository.Users.FindAsync(deviceId);
            // doesnt need null check if the used has to be authorized already 
            // but might be wrong 
            return Ok(UserInfo.Tracks); 
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> CreateNewUserByDeviceId(string deviceId)
        {
            bool UserExists = _repository.Users.Any(u => u.deviceId == deviceId);
            if (UserExists)
            {
                return BadRequest("An user already exists with the same deviceId");
            }
            
            User u = new User(){
                deviceId = deviceId
            };
            
            _repository.Users.Add(u); 
            await _repository.SaveChangesAsync();
            
            return Ok("registered with deviceId: " + deviceId);
        }
     
        [HttpPost("login")]
        public async Task<ActionResult> JwtLogin(string deviceId)
        {
            var user = await _repository.Users.FindAsync(deviceId);
            if (user == null)
            {
                return BadRequest("user was not found, please register");
            }
            
            string jwtToken = GenerateJWT(deviceId);
            string LoginResponse= "Bearer" + jwtToken;
            
            // add the token to the response header
            Response.Headers.Add("Authorization", "Bearer " + jwtToken);
            
            return Ok(LoginResponse);
        }
        
        private string GenerateJWT(string deviceId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            // creating the signature part
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
            // creating the payload part
            var userClaims = new[]
            {
                // seems enough for now
                new Claim(ClaimTypes.NameIdentifier, deviceId),
                // new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                // new Claim(ClaimTypes.Name, user.Name!),
                // new Claim(ClaimTypes.Email, user.Email!),
                // new Claim(ClaimTypes.UserData, user.UserName!),
            };
            
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                // 1 day expiration for now
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}