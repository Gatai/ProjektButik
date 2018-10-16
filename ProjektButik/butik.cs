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
        private View viewItems;
        private Label label;

        private ListBox listItemsBox;
        private ListBox selectedItemsBox;

        string[] gamesFile = File.ReadAllLines("Games.txt");


        //List<string> _items = new List<string>();

        public Butik()
        {
            Text = "Game Store";
            Size = new Size(800, 600);
            Font = new Font("corbel", 10);


            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 4,
                Dock = DockStyle.Fill,
                BackColor = Color.Bisque

            };
            Controls.Add(table);
            //den hittar ej filen why?
            //view browser
            //string[] gamesFile = File.ReadAllLines("Games.txt");

            listItemsBox = new ListBox
            {
                //lägg till rubriker (produkt, pris )
                Size = new Size(300, 300),

            };
            table.Controls.Add(listItemsBox);

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

           
           

            foreach (string row in gamesFile)
            {
                string[] parts = row.Split('|');

                //string[] tempitem = parts[0] + " " + parts[1] + "kr";

                listItemsBox.Items.Add(parts[0] + " " + parts[1] + "kr");

            }

            //if (listItemsBox.Items.)

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
        }

        private void selectedItems(object sender, EventArgs e)
        {
          
        }

        private void removeButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("hello");
        }

        private void addButtonClick(object sender, EventArgs e)
        {
            foreach (string row in gamesFile)
            {
                string[] parts = row.Split('|');
                selectedItemsBox.Items.Add(parts[0] + " " + parts[1] + "kr");

            }
            selectedItemsBox.Text = ("You have selected " + selectedItemsBox.SelectedIndex);
        }



        
    }
}

