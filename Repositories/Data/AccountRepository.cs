using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using WebApi.Context;
using WebApi.Handler;
using WebApi.Models;
using WebApi.Repositories.Interface;
using WebApi.ViewModels;

namespace WebApi.Repositories.Data
{
    public class AccountRepository
    {
        private MyContext myContext;
        //public AccountViewModel AccountViewModel { get; set; }

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public ArrayList Login(string email, string password)
        {
            var data = myContext.Users
            .Include(x => x.Employee)
            .Include(x => x.Role)
            .FirstOrDefault(x => x.Employee.Email == email);
            if (data != null && Hashing.ValidatePass(password, data.Password))
            {
                var arrayList = new ArrayList();
                arrayList.Add(data.Id.ToString());
                arrayList.Add(data.Employee.FullName);
                arrayList.Add(data.Employee.Email);
                arrayList.Add(data.Role.Nama);

                return arrayList;
                
            }
            else
            {
                return null ;
            }
        }

        public int Register(string fullName, string email, DateTime birthdate, string password)
        {
            //var data = myContext.Users
            //.Include(x => x.Employee)
            //.Include(x => x.Role)
            //.SingleOrDefault(x => x.Employee.Email.Equals(email));
            if (myContext.Employees.Any(x => x.Email == email))
            {
                return 1;
            }
            Employee employee = new Employee()
            {
                FullName = fullName,
                Email = email,
                BirthDate = birthdate
            };
            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email == email).Id;
                User user = new User()
                {
                    Id = id,
                    Password = Hashing.HashPass(password),
                    RoleId = 1
                };
                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
            }
            return 0;
        }
        public int ChangePassword(string email,string oldPassword,string newPassword)
        {
            var data = myContext.Users
                .Include(x => x.Employee)
                .SingleOrDefault(x => x.Employee.Email == email);
            if (data != null && Hashing.ValidatePass(oldPassword, data.Password))
            {
                data.Password = Hashing.HashPass(newPassword);
                myContext.Entry(data).State = EntityState.Modified;
                var resultUser = myContext.SaveChanges();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int ForgotPassword(string email, DateTime birthdate, string newPassword)
        {
            var data = myContext.Users
            .Include(x => x.Employee)
            .SingleOrDefault(x => x.Employee.Email == email && x.Employee.BirthDate == birthdate);
            //var validate = Hashing.ValidatePass(data.Password, user.Password);
            if (data != null)
            {
                data.Password = Hashing.HashPass(newPassword);
                myContext.Entry(data).State = EntityState.Modified;
                var resultUser = myContext.SaveChanges();
                return 0;
  
            }
            else
            {
                return 1;
            }
            
        }
    }
}
