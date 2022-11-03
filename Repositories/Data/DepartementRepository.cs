using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repositories.Interface;

namespace WebApi.Repositories.Data
{
    public class DepartementRepository : IRepository<Departement, int>
    {
       private MyContext myContext;

        public DepartementRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Create(Departement Entity)
        {
            myContext.Departements.Add(Entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Delete(int Id)
        {
            var data = myContext.Departements.Find(Id);
            if (data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }

        public IEnumerable<Departement> GetAll()
        {
            return myContext.Departements.ToList();
        }

        public Departement GetById(int Id)
        {
            return myContext.Departements.Find(Id);
        }

        public int Update(Departement Entity)
        {
            myContext.Entry(Entity).State = EntityState.Modified;
            return myContext.SaveChanges();
        }
    }
}
