using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using WebApi.Context;
using WebApi.Handler;
using WebApi.Models;
using WebApi.Repositories.Data;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IConfiguration _configuration;
        private AccountRepository accountRepository;
        //private readonly AccountViewModel accountViewModel;

        public AccountController(IConfiguration config, MyContext myContext, AccountRepository accountRepository)
        {
            _configuration = config;
            this.accountRepository = accountRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM login)
        {
            try
            {
                var data = accountRepository.Login(login.Email, login.Password);
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
                    //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    var tokenCode = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Berhasil Login",
                        Token = tokenCode
                    });
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

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(string fullname, string email, DateTime birthDate, string password)
        {
            try
            {
                var data = accountRepository.Register(fullname, email, birthDate, password);
                if (data == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "account created successfully",
                    });
                }
                else if (data == 1)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "account failed to create"
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

        [HttpPut]
        [Route("Change Password")]
        public IActionResult ChangePassword(string email,string oldPassword,string newPassword)
        {
            try
            {
                var data = accountRepository.ChangePassword(email,oldPassword,newPassword);
                if (data == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "password changed successfully",
                    });
                }
                else if (data == 1)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "password failed to change"
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

        [HttpPut]
        [Route("Forgot Password")]
        public IActionResult ForgotPassword(string email, DateTime birthdate, string newPassword)
        {
            try
            {
                var data = accountRepository.ForgotPassword(email, birthdate, newPassword);
                if (data == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "data changed successfully",
                    });
                }
                else if (data == 1)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "data failed to change"
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
