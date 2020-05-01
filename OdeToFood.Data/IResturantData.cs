using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public interface IResturantData
    {
        IEnumerable<Resturant> GetAll();
        IEnumerable<Resturant> GetByName(string name);
        Resturant GetByID(int? id);
        Resturant Update(Resturant updatedResturant);
        Resturant Add(Resturant newResturant);
        Resturant Delete(int id);
        int GetCountOfResturants();
        int Commit();
    }
}
