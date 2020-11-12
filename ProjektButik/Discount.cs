using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektButik
{
    class Discount
    {
        public string Code;
        public string ProductName;
        public decimal Percentage;
        public DateTime Expire;

        public Discount(string code, string productName, decimal percentage, DateTime expire)
        {
            this.Code = code;
            this.ProductName = productName;
            this.Percentage = percentage;
            this.Expire= expire;
        }

        public static Dictionary<string, Discount> LoadDiscounts()
        {
            Dictionary<string, Discount> discounts = new Dictionary<string, Discount>();

            string[] discountFile = File.ReadAllLines("Discount.txt");

            foreach (string row in discountFile)
            {
                string[] parts = row.Split('|');

                discounts.Add(parts[0], new Discount(parts[0], parts[1], decimal.Parse(parts[2]), DateTime.Parse(parts[3])));
            }
            return discounts;
        }
        //lägga till metoden discount_keyUp här
    }
}
