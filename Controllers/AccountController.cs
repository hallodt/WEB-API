using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Handler;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MyContext myContext;

        public AccountController(MyContext myContext)
        {
            this.myContext = myContext;
        }



        [HttpPost]
        [Route ("Login")]
        public IActionResult Login(string email, string password)
        { 
            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Employee.Email == email);
            var validate = Hashing.ValidatePass(password, data.Password);
            if (data != null && validate)
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "You Are Logged In"
                });
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Email Or Password Incorrect"
                });
            }

        }
    }
}
