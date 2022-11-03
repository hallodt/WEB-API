using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repositories.Interface;

namespace WebApi.Repositories.Data
{
    public class DivisionRepository : IRepository<Division, int>
    {
        private MyContext myContext;

        public DivisionRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //Get All
        public IEnumerable<Division> Get()
        {
            return myContext.Divisions.ToList();
        }

        //Get By ID
        public Division GetById(int Id)
        {
            return myContext.Divisions.Find(Id);
        }

        //Create
        public int Create(Division division)
        {
            myContext.Divisions.Add(division);
            var result = myContext.SaveChanges();
            return result;
        }

        //Update
        public int Update(Division division)
        {
            myContext.Entry(division).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        //Delete
        public int Delete(int Id)
        {
            var data = myContext.Divisions.Find(Id);
            if (data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }

        public IEnumerable<Division> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
