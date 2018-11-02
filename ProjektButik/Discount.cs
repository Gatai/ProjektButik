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
        public decimal Percentage;
        public DateTime Expire;


        public Discount(string code, decimal percentage, DateTime expire)
        {
            this.Code = code;
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

                discounts.Add(parts[0], new Discount(parts[0], decimal.Parse(parts[1]), DateTime.Parse(parts[2])));
            }
            return discounts;
        }
    }
}
