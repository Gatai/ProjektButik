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

        private ListView listItemsView;
        private ListView selectedItemsView;
        private TableLayoutPanel table;
        private TableLayoutPanel informationTable;
        private Label descriptionLabel;

        //string[] gamesFile = File.ReadAllLines("Games.txt");

        private List<Product> products;

        public Butik()
        {
            products = Product.LoadProducts();

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

            listItemsView = new ListView
            {
                View = View.Details,
                //lägg till rubriker (produkt, pris )
                Size = new Size(300, 300),
                //Tag = gamesFile
            };
            table.Controls.Add(listItemsView);

            CreateColumnHeaders(listItemsView);

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
                ColumnCount = 2,
                RowCount = 1,
                Size = new Size(20, 20),
                Dock = DockStyle.Fill,
            };
            table.Controls.Add(informationTable);

            pictureBox = new PictureBox
            {
                //Image = products.First().GetImage(),
                //Image = Image.FromFile(@"C:\Users\gatai\source\repos\ProjektButik\ProjektButik\Image\battelefield5.jpg"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Dock = DockStyle.Top,
                Width = 100,
                Height = 100,
            };

            informationTable.Controls.Add(pictureBox);

            descriptionLabel = new Label();

            informationTable.Controls.Add(descriptionLabel);

            selectedItemsView = new ListView
            {
                View = View.Details,
                //lägg till rubriker (produkt, pris )
                Size = new Size(300, 300),
                Dock = DockStyle.Right
            };
            table.Controls.Add(selectedItemsView);
            selectedItemsView.SelectedIndexChanged += selectedItems;

            CreateColumnHeaders(selectedItemsView);

            //_items.Add("items");
            //_items.Add("Price");
            //listItemsBox.DataSource = _items;

            foreach (Product product in products)
            {
                listItemsView.Items.Add(product.ToListViewItem());
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
            colHead2.Text = "Price";
            colHead2.Width = -2;
            listView.Columns.Add(colHead2);
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
        }

        private void addButtonClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listItemsView.SelectedItems)
            {
                //foreach (Product product in products)
                //{
                //    if (product.Name == item.Text)
                //    {
                //        selectedItemsView.Items.Add(product.ToListViewItem());
                //    }
                //}

                Product selectedProduct = products.Single(m => m.Name == item.Text);

                selectedItemsView.Items.Add(selectedProduct.ToListViewItem());
            }

            selectedItemsView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            selectedItemsView.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);

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






    }
}

