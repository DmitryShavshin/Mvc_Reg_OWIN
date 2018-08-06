using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Domain.Abstract;
using FoodDelivery.Web.UI.Models;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Web.UI.HtmlHelpes;

namespace FoodDelivery.Web.UI.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository repository;
        public int pageSize = 10;

        public ProductController(IProductRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult List()
        {
            return View();
        }

        public ViewResult Products(string category, int page = 1)
        {

            if (category == null)
            {
                ProductsListView model = new ProductsListView()
                {
                    Products = repository.Products
                         .OrderBy(p => p.ProductID)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize)
                };
                ViewBag.PageSize = pageSize;
                return View(model);
            }
            else
            {
                ProductsListView model = new ProductsListView()
                {
                    Products = repository.Products
                         .Where(p => p.Category == category)
                         .OrderBy(p => p.ProductID)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(product => product.Category == category).Count()
                    },
                    CurrentCategory = category
                };
                return View(model);
            }
        }

        public FileContentResult GetImage(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}