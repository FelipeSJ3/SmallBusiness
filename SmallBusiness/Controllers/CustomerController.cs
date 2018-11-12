using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmallBusiness.Models.Repositories;
using SmallBusiness.Models.ViewModels;
using System;
using System.Linq;

namespace SmallBusiness.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private IAuthorizationService authorizationService;
        private ICustomerRepository customerRepository;
        private IGenderRepository genderRepository;
        private ICityRepository cityRepository;
        private IRegionRepository regionRepository;
        private IClassificationRepository classificationRepository;
        private IUserRepository userRepository;

        public CustomerController(
            IAuthorizationService authserv, ICustomerRepository cusrepo, IGenderRepository genrepo,
            ICityRepository citrepo, IRegionRepository regrepo, IClassificationRepository clarepo,
            IUserRepository userepo)
        {
            authorizationService = authserv;
            customerRepository = cusrepo;
            genderRepository = genrepo;
            cityRepository = citrepo;
            regionRepository = regrepo;
            classificationRepository = clarepo;
            userRepository = userepo;
        }

        public ViewResult List(CustomerListViewModel viewModel)
        {
            var customers = customerRepository.Customers;

            if (!string.IsNullOrEmpty(viewModel?.Filters?.NameFilter))
            {
                customers = customers.Where(c => c.Name.Contains(viewModel.Filters.NameFilter));
            }

            if (!string.IsNullOrEmpty(viewModel?.Filters?.GenderFilter))
            {
                customers = customers.Where(c => c.GenderId.ToString() == viewModel.Filters.GenderFilter);
            }

            if (!string.IsNullOrEmpty(viewModel?.Filters?.CityFilter))
            {
                customers = customers.Where(c => c.CityId.ToString() == viewModel.Filters.CityFilter);
            }

            if (!string.IsNullOrEmpty(viewModel?.Filters?.RegionFilter))
            {
                customers = customers.Where(c => c.RegionId.ToString() == viewModel.Filters.RegionFilter);
            }

            if (!string.IsNullOrEmpty(viewModel?.Filters?.ClassificationFilter))
            {
                customers = customers.Where(c => c.ClassificationId.ToString() == viewModel.Filters.ClassificationFilter);
            }

            if (viewModel?.Filters?.LastPurchaseFromFilter != null &&
                viewModel?.Filters?.LastPurchaseToFilter != null)
            {
                customers = customers.Where(c => c.LastPurchase >=  viewModel.Filters.LastPurchaseFromFilter && c.LastPurchase <=  viewModel.Filters.LastPurchaseToFilter);
            }

            if (!string.IsNullOrEmpty(viewModel?.Filters?.SellerFilter))
            {
                var isAdmin = authorizationService.AuthorizeAsync(User, "IsAdmin").Result.Succeeded;
                if (isAdmin) customers = customers.Where(c => c.UserId.ToString() == viewModel.Filters.SellerFilter);
            }

            customers.OrderBy(c => c.Id);

            return View(new CustomerListViewModel
            {
                Customers =customers
                .Include(c => c.Gender)
                .Include(c => c.City)
                .Include(c => c.Region)
                .Include(c => c.Classification)
                .Include(c => c.User)
                .AsNoTracking().ToList(),
                Genders = genderRepository.Genders.AsNoTracking().Select(g =>
                new SelectListItem()
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                }).ToList(),
                Cities = cityRepository.Cities.AsNoTracking().Select(c =>
                new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList(),
                Regions = regionRepository.Regions.AsNoTracking().Select(r =>
                new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }).ToList(),
                Classifications = classificationRepository.Classifications.AsNoTracking().Select(c =>
                new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList(),
                Sellers = userRepository.Users.AsNoTracking().Where(s => s.UserRoleId == 2).Select(u =>
                new SelectListItem()
                {
                    Text = u.Login,
                    Value = u.Id.ToString()
                }).ToList(),
            });
        }
    }
}