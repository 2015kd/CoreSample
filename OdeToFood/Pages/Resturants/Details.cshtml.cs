using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Resturants
{
    public class DetailsModel : PageModel
    {
        private readonly IResturantData resturantData;

        [TempData]
        public string Message { get; set; }

        public Resturant Resturant { get; set; }

        public DetailsModel(IResturantData resturantData)
        {
            this.resturantData = resturantData;
        }

        //public void OnGet(int resturantID)
        //{
        //    //Resturant = new Resturant();
        //    //Resturant.ID = resturantID;

        //    Resturant = resturantData.GetByID(resturantID);
        //}

        public IActionResult OnGet(int resturantID)
        {
            Resturant = resturantData.GetByID(resturantID);
            if (Resturant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}