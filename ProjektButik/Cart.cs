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

        public decimal TotalDiscount()
        {
            if (CurrentDiscount != null)
            {
                if (CurrentDiscount.ProductName == "*")
                {
                    decimal sum = 0;
                    foreach (KeyValuePair<Product, int> p in ProductsInCart)
                    {
                        sum += p.Key.Price * p.Value;
                    }

                    return (sum * (CurrentDiscount.Percentage / 100));
                }
                else // Discount gäller bara en produkt
                {
                    foreach (KeyValuePair<Product, int> p in ProductsInCart)
                    {
                        if (p.Key.Name == CurrentDiscount.ProductName)
                        {
                            decimal sum = p.Key.Price * p.Value;

                            return (sum * (CurrentDiscount.Percentage / 100));
                        }
                    }
                }
            }

            return 0;
        }

        public decimal TotalCost()
        {
            decimal sum = 0;
            foreach (KeyValuePair<Product, int> p in ProductsInCart)
            {
                sum += p.Key.Price * p.Value;
            }

            sum -= TotalDiscount();

            return sum;
        }

        public Discount DiscountCodeExists(string code)
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

        public bool ContainDiscountProduct(Discount discount)
        {
            return ProductsInCart.Any(m => m.Key.Name == discount.ProductName);
        }

        public void SetDiscountCode(Discount discount)
        {
            CurrentDiscount = discount;
        }

        //varorna ifrån carten i en text fil
        public void LoadCart(List<Product> products)
        {
            //om filen inte finns så läses inte filen 
            if (!File.Exists(@"C:\Windows\Temp\SaveCart.txt"))
            {
                return;
            }

            string[] cartFile = File.ReadAllLines(@"C:\Windows\Temp\SaveCart.txt");
            
            if (string.IsNullOrEmpty(cartFile[0]) == false)
            {
                CurrentDiscount = Discounts.SingleOrDefault(m => m.Key == cartFile[0]).Value;
            }
                
            foreach (string row in cartFile.Skip(1))
            {
                string[] parts = row.Split('|');

                string name = parts[0];
                int count = int.Parse(parts[1]);

                foreach (Product p in products)
                {
                    if (p.Name == name)
                    {
                        ProductsInCart.Add(p, count);
                    }
                }
            }
        }

        public void SaveCart()
        {
            //spara i text fil
            List<string> lines = new List<string>();

            if (CurrentDiscount == null)
            {
                lines.Add("");
            }
            else
            {
                lines.Add(CurrentDiscount.Code);
            }

            foreach (var item in ProductsInCart)
            {
                lines.Add(item.Key.Name + "|" + item.Value);
            }

            //skapar en file om den inte finns. lägger till värden man har i lines
            File.WriteAllLines(@"C:\Windows\Temp\SaveCart.txt", lines);
        }

        public string Receipt()
        {
            List<string> lines = new List<string>();
            lines.Add("Game Store");
            lines.Add("");

            foreach (var item in ProductsInCart)
            {
                string productName = item.Key.Name;
                if (productName.Length > 20)
                {
                    productName = productName.Substring(0, 20);
                }
                //index 0 och -8 tecken lång
                lines.Add(string.Format("{0,-8} {1,-25} {2,7} {3,10}", item.Value, 
                                                                       productName, 
                                                                       item.Value > 1 ? item.Key.Price.ToString() : string.Empty, 
                                                                       (item.Key.Price * item.Value)));

                //if (item.Value > 1)
                //{
                //    lines.Add(string.Format("{0,-8} {1,-25} {2,7} {3,10}", item.Value, productName, item.Key.Price, (item.Key.Price * item.Value)));
                //}
                //else
                //{
                //    lines.Add(string.Format("{0,-8} {1,-25} {2,7} {3,10}", item.Value, productName, string.Empty, (item.Key.Price * item.Value)));
                //}
            }

            lines.Add("");

            if (CurrentDiscount != null)
            {
                if (CurrentDiscount.ProductName == "*")
                {
                    lines.Add(string.Format("Discount {0} %", CurrentDiscount.Percentage));
                }
                else
                {
                    lines.Add(string.Format("Discount {0} % for {1}", CurrentDiscount.Percentage, CurrentDiscount.ProductName));
                }
            }

            lines.Add(string.Format("Total Discount {0} kr", TotalDiscount()));

            lines.Add(string.Format("Total {0} Kr", TotalCost()));

            return string.Join("\n", lines);
        }
    }
}
