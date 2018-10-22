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

        private View viewItems;
        private Label label;

        private ListBox listItemsBox;
        private ListBox selectedItemsBox;
        private TableLayoutPanel table;
        private TableLayoutPanel informationTable;

        string[] gamesFile = File.ReadAllLines("Games.txt");


        //List<string> _items = new List<string>();

        public Butik()
        {
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

            listItemsBox = new ListBox
            {
                //lägg till rubriker (produkt, pris )
                Size = new Size(300, 300),
                Tag = gamesFile,
            };
            table.Controls.Add(listItemsBox);

            informationTable = new TableLayoutPanel
            {
                ColumnCount = 2,
                Size = new Size(20, 20),
                Dock = DockStyle.Top,
            };
            informationTable.Controls.Add(new Button { Text = "hej" });
            table.Controls.Add(informationTable);

            selectedItemsBox = new ListBox
            {
                //lägg till rubriker (produkt, pris )

                Size = new Size(300, 300),
                Dock = DockStyle.Right
            };
            table.Controls.Add(selectedItemsBox);
            selectedItemsBox.SelectedIndexChanged += selectedItems;

            //_items.Add("items");
            //_items.Add("Price");
            //listItemsBox.DataSource = _items;

            List<Label> ld = new List<Label>();

            foreach (string row in gamesFile)
            {
                string[] parts = row.Split('|');
                Label ds = new Label { Name = parts[0] + " " + parts[1], Text = parts[2] };
                ld.Add(ds);
                listItemsBox.Items.Add(ds.Name + "kr");
            }

            addButton = new Button
            {
                Text = "Add -->",
                Width = 150,
                Height = 40,
                Dock = DockStyle.None,
                BackColor = Color.Green
            };
            table.Controls.Add(addButton);
            //lägga till så att när man klickar på knappen så ska något event ske
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
            //lägga till så att när man klickar på knappen så ska något event ske

            label = new Label
            {
                Text = "Discount Code",
                Dock = DockStyle.Right,
                BackColor = Color.Orange
            };
            table.Controls.Add(label);

            TextBox discountBox = new TextBox
            {
                Dock = DockStyle.None,
            };
            table.Controls.Add(discountBox);

            //ska ta fram en bild åt gången beroende på vad användaren klickar på
            string[] filenames = Directory.GetFiles(@"c:\Users\gatai\source\repos\ProjektButik\ProjektButik\Image");
            foreach (string name in filenames)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Image = Image.FromFile(name),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Dock = DockStyle.Bottom,
                    Width = 100,
                    Height = 100,
                };
                table.Controls.Add(pictureBox);
            }

        }
        //utanför konstruktorn
        //utanför konstruktorn
        //utanför konstruktorn
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
            selectedItemsBox.Items.Remove(selectedItemsBox.SelectedItem);
        }

        private void addButtonClick(object sender, EventArgs e)
        {
            for (int i = 0; i < listItemsBox.Items.Count; i++)
            {
                if (listItemsBox.GetSelected(i))
                {
                    string row = gamesFile[i];
                    string[] parts = row.Split('|');

                    selectedItemsBox.Items.Add(Text = parts[0] + " " + parts[1] + "kr");
                }
            }
        }






    }
}

