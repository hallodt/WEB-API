using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repositories.Interface;

namespace WebApi.Repositories.Data
{
    public class GeneralRepository<Entity> : IRepository<Entity, int>
        where Entity : class
    {
        MyContext myContext;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
    
        public int Create(Entity Entity)
        {
            myContext.Add(Entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Delete(int Id)
        {
            var data = GetById(Id);
            myContext.Set<Entity>().Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> GetAll()
        {
            return myContext.Set<Entity>().ToList();
        }

        public Entity GetById(int Id)
        {
            return myContext.Set<Entity>().Find(Id);
        }

        public int Update(Entity Entity)
        {
            myContext.Entry(Entity).State = EntityState.Modified;
            return myContext.SaveChanges();
        }
    }
}
