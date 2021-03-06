﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektButik
{
    class Product
    {
        public string Name;
        public int Price;
        public string ImageFileName;
        public int Release;
        public string Description;

        public Product(string name, int price, string imageFileName, int release, string description)
        {
            this.Name = name;
            this.Price = price;
            this.ImageFileName = imageFileName;
            this.Release = release;
            this.Description = description;
        }

        public Image GetImage()
        {
            return Image.FromFile(@"Image\" + ImageFileName);
        }
            
        public ListViewItem ToListViewItem()
        {
            return new ListViewItem(new string[] { Name, Release.ToString(), Price.ToString() });
        }

        public ListViewItem ToCartListViewItem(int count)
        {
            return new ListViewItem(new string[] { Name, Price.ToString(), count.ToString(), (count * Price).ToString() });
        }

        public static List<Product> LoadProducts()
        {
            List<Product> products = new List<Product>();

            string[] gamesFile = File.ReadAllLines("Games.txt");

            foreach (string row in gamesFile)
            {
                string[] parts = row.Split('|');

                products.Add(new Product(parts[0], int.Parse(parts[1]), parts[2], int.Parse(parts[3]), parts[4]));

                /* Skapa produkt utan konstruktor */
                //products.Add(new Product()
                //{
                //    Name = parts[0],
                //    Price = int.Parse(parts[1])
                //});

                /* Samma som ovan, men tillfällig variabel */
                //Product product = new Product()
                //{
                //    Name = parts[0],
                //    Price = int.Parse(parts[1])
                //};
                //products.Add(product);

                /* Samma som ovan, gammalt skrivsätt */
                //Product product = new Product();
                //product.Name = parts[0];
                //product.Price = int.Parse(parts[1]);
                //products.Add(product);
            }
            return products;
        }
    }
}
