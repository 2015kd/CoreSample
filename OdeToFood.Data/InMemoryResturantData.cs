using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryResturantData : IResturantData
    {
        List<Resturant> resturants;

        public InMemoryResturantData()
        {
            resturants = new List<Resturant>()
            {
                new Resturant{ID=1, Name="Scott", Location="Seattle", Cuisine=CuisineType.Mexican},
                new Resturant{ID=2, Name="Alen", Location="NYC", Cuisine=CuisineType.Italian},
                new Resturant{ID=3, Name="Max", Location="India", Cuisine=CuisineType.Indian}
            };
        }

        public IEnumerable<Resturant> GetAll()
        {
            return from r in resturants  
                   orderby r.Name
                   select r;
        }

        public IEnumerable<Resturant> GetByName(string name =null)
        {
            return from r in resturants
                   //where string.IsNullOrEmpty(r.Name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Resturant GetByID(int? id)
        {
            return resturants.SingleOrDefault(r => r.ID == id);
            //return resturants.FirstOrDefault(r => r.ID == id);
        }

        public Resturant Add(Resturant newResturant)
        {
            resturants.Add(newResturant);
            newResturant.ID = resturants.Max(r => r.ID);
            return newResturant;
        }

        public Resturant Update(Resturant updatedResturant)
        {
            var resturant = resturants.SingleOrDefault(r => r.ID == updatedResturant.ID);
            if (resturant != null)
            {
                resturant.Name = updatedResturant.Name;
                resturant.Location = updatedResturant.Location;
                resturant.Cuisine = updatedResturant.Cuisine;
            }
            return resturant;
        }

        public Resturant Delete(int id)
        {
            var resturant = resturants.FirstOrDefault(r => r.ID == id);
            if (resturant != null)
            {
                resturants.Remove(resturant);
            }
            return resturant;
        }

        public int Commit()
        {
            return 0;
        }

        public int GetCountOfResturants()
        {
            throw new System.NotImplementedException();
        }
    }
}
