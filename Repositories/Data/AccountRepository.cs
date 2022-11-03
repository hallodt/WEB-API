using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Handler;
using WebApi.Models;
using WebApi.Repositories.Interface;

namespace WebApi.Repositories.Data
{
    public class AccountRepository
    {
        private MyContext myContext;

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Login(string email, string password)
        {
            var data = myContext.Users
            .Include(x => x.Employee)
            .Include(x => x.Role)
            .FirstOrDefault(x => x.Employee.Email == email);
            var validate = Hashing.ValidatePass(password, data.Password);
            if (data != null && validate)
            {
                return 0;
            }
            return 1;
        }
    }
}
