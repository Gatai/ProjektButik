using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ProjektButik
{
    class Cart
    {
        public Dictionary<string, Discount> Discounts { get; set; }
        public Dictionary<Product, int> ProductsInCart { get; set; }
        public Discount CurrentDiscount { get; set; }

        public Cart()
        {
            ProductsInCart = new Dictionary<Product, int>();
            Discounts = Discount.LoadDiscounts();
        }

        public void AddProduct(Product product)
        {
            if (ProductsInCart.ContainsKey(product))
            {
                ProductsInCart[product] += 1;
            }
            else
            {
                ProductsInCart.Add(product, 1);
            }
        }

        public void RemoveProduct(Product product)
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

        public decimal TotalCost()
        {
            //gör samma sak nedan
            //return ProductsInCart.Sum(m => m.Key.Price * m.Value);
            
            decimal sum = 0;
            foreach (KeyValuePair<Product, int> p in ProductsInCart)
            {
                sum += p.Key.Price * p.Value;
            }

            if (CurrentDiscount != null)
            {
                sum = sum - (sum * (CurrentDiscount.Percentage / 100));
            }

            return sum;
        }

        public Discount IsDiscountCodeValid(string code)
        {
            if (Discounts.ContainsKey(code) == true)
            {
                return Discounts[code];
            }
            else
            {
                return null;
            }
        }

        public void SetDiscountCode(Discount discount)
        {
            CurrentDiscount = discount;
        }

    }
}
