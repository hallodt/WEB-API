using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
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
        private readonly AccountRepository accountRepository;
        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var data = accountRepository.Login(email, password);
                if (data != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "you are logged in",
                        Data = data
                        //Data = new
                        //{
                        //    Id = Convert.ToInt32(data[0]),
                        //    FullName = data[1],
                        //    Email = data[2],
                        //    Role = data[3]
                        //}
                        
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
        public IActionResult Register(string fullname,string email, DateTime birthDate,string password)
        {
            try
            {
                var data = accountRepository.Register(fullname, email,birthDate,password);
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
