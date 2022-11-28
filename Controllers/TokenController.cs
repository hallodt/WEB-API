using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Context;
using WebApi.Repositories.Data;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private AccountRepository accountRepository;
        //private readonly AccountViewModel accountViewModel;

        public TokenController(IConfiguration config, MyContext myContext, AccountRepository accountRepository)
        {
            _configuration = config;
            this.accountRepository = accountRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var data = accountRepository.Login(email, password);
                if (data != null)
                {

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", data.Id.ToString()),
                        new Claim("FullName", data.FullName),
                        new Claim("Email", data.Email),
                        new Claim("role", data.Role)

                        //new Claim("Id", accountViewModel.Id.ToString()),
                        //new Claim("FullName", accountViewModel.FullName),
                        //new Claim("Email", accountViewModel.Email),
                        //new Claim("Role", accountViewModel.Role)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "email or password incorrect"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
            return Ok();
        }

    }
}
