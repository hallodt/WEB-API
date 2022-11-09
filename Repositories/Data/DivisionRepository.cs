using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repositories.Interface;

namespace WebApi.Repositories.Data
{
    public class DivisionRepository : GeneralRepository<Division>
    {
        private readonly MyContext myContext;

        public DivisionRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
