using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ProjektButik
{
    class Butik : Form
    {
        private Button addButton;
        private Button removeButton;
        private Button saveButton;
        private PictureBox pictureBox;
        private TextBox discountBox;

        private View viewItems;
        private Label label;

        private ListView productsItemsView;
        private ListView cartIteamsView;
        private TableLayoutPanel table;
        private TableLayoutPanel informationTable;
        private Label descriptionLabel;

        private List<Product> productList;
        private Cart cart;

        public Butik()
        {
            productList = Product.LoadProducts();
            cart = new Cart();

            Text = "Game Store";
            Size = new Size(900, 800);
            Font = new Font("corbel", 10);

            table = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 4,
                Dock = DockStyle.Fill,
                BackColor = Color.Bisque
            };
            Controls.Add(table);

            productsItemsView = new ListView
            {
                View = View.Details,
                //lägg till rubriker (produkt, pris )
                Size = new Size(300, 300),
                //Tag = gamesFile
            };
            table.Controls.Add(productsItemsView);

            CreateColumnHeaders(productsItemsView);

            // listItemsview.Columns.Add("Products");

            //listItemsBox = new ListBox
            //{
            //    //lägg till rubriker (produkt, pris )
            //    Size = new Size(300, 300),
            //    Tag = gamesFile,
            //};
            //table.Controls.Add(listItemsBox);

            informationTable = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 2,
                Anchor = AnchorStyles.Right,
                Dock = DockStyle.Fill,
            };
            table.Controls.Add(informationTable);

            pictureBox = new PictureBox
            {
                //Image = products.First().GetImage(),
                //Image = Image.FromFile(@"C:\Users\gatai\source\repos\ProjektButik\ProjektButik\Image\battelefield5.jpg"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Width = 150,
                Height = 150,
            };
            informationTable.Controls.Add(pictureBox);
            productsItemsView.SelectedIndexChanged += DisplayD;

            descriptionLabel = new Label()
            {
               
                Dock = DockStyle.Fill
            };

            informationTable.Controls.Add(descriptionLabel);

            cartIteamsView = new ListView
            {
                View = View.Details,
                //lägg till rubriker (produkt, pris )
                Size = new Size(300, 300),
                Dock = DockStyle.Right
            };
            table.Controls.Add(cartIteamsView);
            cartIteamsView.SelectedIndexChanged += selectedItems;

            

            CreateColumnHeadersCart(cartIteamsView);

            //_items.Add("items");
            //_items.Add("Price");
            //listItemsBox.DataSource = _items;

            foreach (Product product in productList)
            {
                productsItemsView.Items.Add(product.ToListViewItem());
            }

            addButton = new Button
            {
                Text = "Buy-->",
                Width = 150,
                Height = 40,
                Dock = DockStyle.None,
                BackColor = Color.Green
            };
            table.Controls.Add(addButton);
            addButton.Click += addButtonClick;

            removeButton = new Button
            {
                Text = "<--Remove",
                Width = 150,
                Height = 40,
                Dock = DockStyle.Right,
                BackColor = Color.Red
            };
            table.Controls.Add(removeButton);
            removeButton.Click += removeButtonClick;

            saveButton = new Button
            {
                Text = "Save",
                Width = 150,
                Height = 40,
                Dock = DockStyle.None,
                BackColor = Color.Blue
            };
            table.Controls.Add(saveButton);
            //lägga till så att när man klickar på knappen så ska varukorgen sparas i en textfile

            label = new Label
            {
                Text = "Discount Code",
                Dock = DockStyle.Right,
                BackColor = Color.Orange
            };
            table.Controls.Add(label);

            discountBox = new TextBox
            {
                Dock = DockStyle.None,
            };
            table.Controls.Add(discountBox);

            //ska ta fram en bild åt gången beroende på vad användaren klickar på
            //string[] filenames = Directory.GetFiles(@"c:\Users\gatai\source\repos\ProjektButik\ProjektButik\Image");
            //foreach (string name in filenames)
            //{
            //    PictureBox pictureBox = new PictureBox
            //    {
            //        Image = Image.FromFile(name),
            //        SizeMode = PictureBoxSizeMode.StretchImage,
            //        Dock = DockStyle.Bottom,
            //        Width = 100,
            //        Height = 100,
            //    };
            //    table.Controls.Add(pictureBox);
            //}

        }
        //utanför konstruktorn
        //utanför konstruktorn
        //utanför konstruktorn

        private void CreateColumnHeaders(ListView listView)
        {
            ColumnHeader colHead1 = new ColumnHeader();
            colHead1.Text = "Product";
            colHead1.Width = -2;
            listView.Columns.Add(colHead1);

            ColumnHeader colHead2 = new ColumnHeader();
            colHead2.Text = "Release";
            colHead2.Width = -2;
            listView.Columns.Add(colHead2);

            ColumnHeader colHead3 = new ColumnHeader();
            colHead3.Text = "Price";
            colHead3.Width = -2;
            listView.Columns.Add(colHead3);
        }

        private void CreateColumnHeadersCart(ListView listView)
        {
            ColumnHeader colHead1 = new ColumnHeader();
            colHead1.Text = "Product";
            colHead1.Width = -2;
            listView.Columns.Add(colHead1);

            ColumnHeader colHead2 = new ColumnHeader();
            colHead2.Text = "Price";
            colHead2.Width = -2;
            listView.Columns.Add(colHead2);

            ColumnHeader colHead3 = new ColumnHeader();
            colHead3.Text = "Count";
            colHead3.Width = -2;
            listView.Columns.Add(colHead3);

            ColumnHeader colHead4 = new ColumnHeader();
            colHead4.Text = "Total:";
            colHead4.Width = -2;
            listView.Columns.Add(colHead4);
        }

        private void selectedItems(object sender, EventArgs e)
        {
            //ListBox listBox = (ListBox)sender;

            //string[] gamesFile = (string[])listBox.Tag;

            //string row = gamesFile[listBox.SelectedIndex];
            //string[] parts = row.Split('|');

            //informationTable.Text = ("You have selected " + listBox.SelectedIndex +
            //      ": " +
            //    listBox.SelectedItem + " " + parts[1] + " " + parts[2]);
        }

        private void removeButtonClick(object sender, EventArgs e)
        {
            //selectedItemsView.Items.Remove(selectedItemsView.SelectedItem);
            foreach (ListViewItem item in cartIteamsView.SelectedItems)
            {
                Product selectedProduct = productList.Single(m => m.Name == item.Text);

                cart.RemoveProduct(selectedProduct);
            }
           

            cartIteamsView.Items.Clear();
            foreach (KeyValuePair<Product, int> item in cart.ProductsInCart)
            {
                cartIteamsView.Items.Add(item.Key.ToCartListViewItem(item.Value));

            }
        }

        private void addButtonClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in productsItemsView.SelectedItems)
            {
                Product selectedProduct = productList.Single(m => m.Name == item.Text);

                cart.AddProduct(selectedProduct);
            }

            cartIteamsView.Items.Clear();
            foreach (KeyValuePair<Product, int> item in cart.ProductsInCart)
            {
                cartIteamsView.Items.Add(item.Key.ToCartListViewItem(item.Value));             
                 
            }

            cartIteamsView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            cartIteamsView.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);

            //for (int i = 0; i < listView.Items.Count; i++)
            //{
            //if (listItemsView.GetSelected(i))
            //{
            //    string row = gamesFile[i];
            //    string[] parts = row.Split('|');

            //    selectedItemsBox.Items.Add(Text = parts[0] + " " + parts[1] + "kr");
            //}
            //}
        }
        private void DisplayD (object sender, EventArgs e)
        {
            if (productsItemsView.SelectedItems.Count > 0)
            {
                try
                {
                    //int selectedindex = listItemsView.SelectedIndices[0];

                    string x = productsItemsView.SelectedItems[0].SubItems[0].Text;

                    Product selectedProduct = productList.Single(m => m.Name == x);

                    pictureBox.Image = selectedProduct.GetImage();

                    descriptionLabel.Text = selectedProduct.Description; 
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("cant find picture! " + ex.Message);
                }
            }
        }





    }
}

