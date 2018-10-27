using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektButik
{
    class Cart
    {
        public Dictionary<Product, int> ProductsInCart { get; set; }

        public Cart()
        {
            ProductsInCart = new Dictionary<Product, int>();
        }

        public void AddProduct (Product product)
        {
            if (ProductsInCart.ContainsKey(product) )
            {
                ProductsInCart[product] += 1;
            }
            else
            {
                ProductsInCart.Add(product, 1);
            }
        }
        public void RemoveProduct (Product product)
        {
            int count = ProductsInCart[product];

            if (count > 1)
            {
                ProductsInCart[product] -= 1;
            }
            else
            {
                ProductsInCart.Remove(product);
            }

        }

    }
}
