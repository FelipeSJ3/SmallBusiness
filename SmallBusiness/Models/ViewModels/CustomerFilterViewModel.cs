using System;

namespace SmallBusiness.Models.ViewModels
{
    public class CustomerFilterViewModel
    {
        public string NameFilter { get; set; }
        public string GenderFilter { get; set; }
        public string CityFilter { get; set; }
        public string RegionFilter { get; set; }
        public DateTime? LastPurchaseFromFilter { get; set; }
        public DateTime? LastPurchaseToFilter { get; set; }
        public string ClassificationFilter { get; set; }
        public string SellerFilter { get; set; }
    }
}