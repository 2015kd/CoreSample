using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Pages.ViewComponents
{
    public class RestuarantCountViewComponent : ViewComponent
    {
        private readonly IResturantData resturantData;

        public RestuarantCountViewComponent(IResturantData resturantData)
        {
            this.resturantData = resturantData;
        }

        public IViewComponentResult Invoke()
        {
            var count = resturantData.GetCountOfResturants();
            return View(count);
        }
    } 
}
