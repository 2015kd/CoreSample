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
    public class DeleteModel : PageModel
    {
        private readonly IResturantData resturantData;

        public Resturant Resturant { get; set; }

        public DeleteModel(IResturantData resturantData)
        {
            this.resturantData = resturantData;
        }

        public IActionResult OnGet(int resturantID)
        {
            Resturant = resturantData.GetByID(resturantID);
            if (Resturant  == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int resturantID)
        {
            var resturant = resturantData.Delete(resturantID);
            resturantData.Commit();

            if (resturant == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{resturant.Name} Deleted";
            return RedirectToPage("./List");
        }
    }
}