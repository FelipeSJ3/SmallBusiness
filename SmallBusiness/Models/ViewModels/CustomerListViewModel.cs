using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SmallBusiness.Models.ViewModels
{
    public class CustomerListViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }
        public IEnumerable<SelectListItem> Classifications { get; set; }
        public IEnumerable<SelectListItem> Sellers { get; set; }

        public CustomerFilterViewModel Filters { get; set; }
    }
}
