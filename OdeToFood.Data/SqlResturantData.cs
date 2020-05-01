using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlResturantData : IResturantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlResturantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Resturant Add(Resturant newResturant)
        {
            db.Add(newResturant);
            return newResturant;
        }
         
        public int Commit()
        {
            return db.SaveChanges();    //this changes occur atomitically
        }

        public Resturant Delete(int id)  
        {
            var resturant = GetByID(id);
            if (resturant != null)
            {
                db.Resturants.Remove(resturant);
            }
            return resturant;
        }

        public IEnumerable<Resturant> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Resturant GetByID(int? id)
        {
            return db.Resturants.Find(id);
        }

        public IEnumerable<Resturant> GetByName(string name)
        {
            var query = from r in db.Resturants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public int GetCountOfResturants()
        {
            return db.Resturants.Count();
        }

        public Resturant Update(Resturant updatedResturant)
        {
            var entity = db.Resturants.Attach(updatedResturant);    //Attach is an object that track changes 
            entity.State = EntityState.Modified;
            return updatedResturant;
        }
    }
}
