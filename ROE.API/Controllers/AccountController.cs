using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ROE.API.Model;
using ROE.DataAccess.Entities;
using ROE.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ROE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IUserServices _userServices;

        public AccountController(IConfiguration configuration, IUserServices userServices)
        {
            _configuration = configuration;
            _userServices = userServices;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] LoginModel model)
        {
            string returnCode = string.Empty;
            User user = _userServices.AuthenticateUser(model.UserName, model.Password, out returnCode);
            if (!string.IsNullOrEmpty(returnCode))
            {
                if (returnCode == "C200")
                {
                    if (user != null && user.PK_UserId > 0)
                    {
                        var issuer = _configuration?["JWT:Issuer"]?.ToString() ?? string.Empty;
                        var audience = _configuration?["JWT:Audience"]?.ToString() ?? string.Empty;
                        var key = Encoding.UTF8.GetBytes(_configuration?["JWT:Key"] ?? string.Empty);
                        var encryptionKey = Encoding.UTF8.GetBytes(_configuration?["JWT:EncryptionKey"] ?? string.Empty);
                        var expiryMinutes = Convert.ToDouble(_configuration?["JWT:TokenExpired"] ?? "30");

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[]
                            {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                            Issuer = issuer,
                            Audience = audience,
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                            //For encrypted JWT Token
                            //EncryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512)
                        };

                        var tokenHandler = new JwtSecurityTokenHandler()
                        {
                            TokenLifetimeInMinutes = (int)expiryMinutes
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var stringToken = tokenHandler.WriteToken(token);

                        return Ok(new { accessToken = stringToken });
                    }
                }
                else if (returnCode == "C402")
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { message = "user_not_active" });
                }
                else if (returnCode == "C403")
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { message = "user_not_apprve_by_admin" });
                }
            }
            return NotFound();
        }
    }
}
