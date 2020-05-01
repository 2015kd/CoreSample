using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Resturants
{
    public class EditModel : PageModel
    {
        private readonly IResturantData resturantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Resturant Resturant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IResturantData resturantData, IHtmlHelper htmlHelper)
        {
            this.resturantData = resturantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? resturantID)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (resturantID.HasValue)
            {
                Resturant = resturantData.GetByID(resturantID);
            }
            else
            {
                Resturant = new Resturant();
            }
            
            if (Resturant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();                
            }

            Resturant = Resturant.ID > 0 ? resturantData.Update(Resturant) : resturantData.Add(Resturant);
            resturantData.Commit();
            TempData["Message"] = "Restaurant Saved!";
            return RedirectToPage("./Details", new { resturantID = Resturant.ID });

        }

    }
}