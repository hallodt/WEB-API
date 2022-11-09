using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repositories.Interface;

namespace WebApi.Repositories.Data
{
    public class DepartementRepository : GeneralRepository<Departement>
    {
        private MyContext myContext;

        public DepartementRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
