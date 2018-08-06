using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Domain.Abstract;

namespace FoodDelivery.Web.UI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectCategory = category;
            IEnumerable<string> categories = repository.Products
                .Select(product => product.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}