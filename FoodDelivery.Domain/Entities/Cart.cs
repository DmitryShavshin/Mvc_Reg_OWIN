using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Entities
{
    public class Cart
    {

        private List<CartLine> lineCollection = new List<CartLine>();   // Создать коллекцию имеющую тип CartLine 
                                                                        // Будет использована для хранения данных корзины

        // Добавление товара в корзину
        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            if (line == null)                           // Если в корзине нет указанного элемента 
            {
                lineCollection.Add(new CartLine         // Добавить один элемент в корзину 
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else                                        // Если элемент находится в корзине 
            {
                line.Quantity += quantity;              // Увеличить колличество на один
            }
        }

        // Удаление элемента из корзины
        public void RemoveLine(Product product)
        {
            // Удалить из коллекции все элементы c совпадающим ID 
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        // Посчитать сумму
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        // Очистить коллекцию
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
