using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Web.UI.Models
{
    public class ProductsListView
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}