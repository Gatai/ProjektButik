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

        public void AddProduct (Product p)
        {
            if (ProductsInCart.ContainsKey(p) )
            {
                ProductsInCart[p] += 1;
            }
            else
            {
                ProductsInCart.Add(p, 1);
            }
        } 

    }
}
