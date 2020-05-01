using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Resturants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IResturantData resturantData;
        private readonly ILogger<ListModel> logger;

        public string message { get; set; }
        public IEnumerable<Resturant> Resturants { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config, IResturantData resturantData, ILogger<ListModel> logger)
        {
            this._config = config;
            this.resturantData = resturantData;
            this.logger = logger;
        }

        //response to http get requests
        public void OnGet()    //if searchTerm is Int then application will throw error but having string is like this parameter is optional
        {
            logger.LogError("Executing List Model");

            //message = "hello lockdown!";
            message = _config["Message"];
            //Resturants = resturantData.GetAll();

            //HttpContext.Request.QueryString: can be used to find search term
            Resturants = resturantData.GetByName(SearchTerm);

        }
    }
}