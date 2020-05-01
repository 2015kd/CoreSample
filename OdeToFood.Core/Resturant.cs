using System;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Core
{
    public class Resturant /*: IValidatableObject   can be used for validation*/
    {
        public int ID { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string Location { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
