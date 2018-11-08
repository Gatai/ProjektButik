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
    class GameStore : Form
    {
        private Button addButton;
        private Button removeButton;
        private Button clearCart;
        private Button receiptButton;
        private PictureBox pictureBox;
        private TextBox discountBox;

        private ListView productsItemsView;
        private ListView cartIteamsView;
        private TableLayoutPanel table;
        private TableLayoutPanel informationTable;
        private Label descriptionLabel;

        private List<Product> productList;
        private Cart cart;
        private Label totalCost;

        public GameStore()
        {
            productList = Product.LoadProducts();
            Text = "Game Store";
            Size = new Size(1000, 500);
            Font = new Font("corbel", 10);
            MinimumSize = new Size(700, 500);
            StartPosition = FormStartPosition.CenterScreen;            

            TableLayoutPanel mainPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 3,
                BackColor = Color.LightBlue
            };
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));


            Controls.Add(mainPanel);

            Label items = new Label()
            {
                Text = "Products for sales",
                Height = 30,
                Font = new Font("corbel", 14),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            mainPanel.Controls.Add(items);

            Label store = new Label()
            {
                Text = "Game Store",
                Height = 30,
                Font = new Font("corbel", 20),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            mainPanel.Controls.Add(store);

            Label cartView = new Label()
            {
                Text = "Cart",
                Height = 30,
                Font = new Font("corbel", 14),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter

            };

            mainPanel.Controls.Add(cartView);

            table = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 2,
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackColor = Color.LightBlue,
                //CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
            };
            mainPanel.SetColumnSpan(table, 3);
            mainPanel.Controls.Add(table);

            table.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Percent, Width = 35 });
            table.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Percent, Width = 30 });
            table.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Percent, Width = 35 });

            table.RowStyles.Add(new RowStyle() { SizeType = SizeType.Percent, Height = 100 });
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            productsItemsView = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill,
                Size = new Size(300, 300),
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
            productsItemsView.SelectedIndexChanged += DisplayDescription;

            descriptionLabel = new Label()
            {
                Dock = DockStyle.Fill
            };
            informationTable.Controls.Add(descriptionLabel);

            cartIteamsView = new ListView
            {
                View = View.Details,
                Size = new Size(300, 300),
                Dock = DockStyle.Fill,
            };
            // table.SetColumnSpan(cartIteamsView, 2);
            table.Controls.Add(cartIteamsView);

            /////för att få ut en totalkostnaden på skärmen
            //Label TotalcoustLabel = new Label()
            //{
            //    Dock = DockStyle.Fill
            //};
            //table.Controls.Add(TotalcoustLabel);

            CreateColumnHeadersCart(cartIteamsView);

            //_items.Add("items");
            //_items.Add("Price");
            //listItemsBox.DataSource = _items;

            foreach (Product product in productList)
            {
                productsItemsView.Items.Add(product.ToListViewItem());
            }

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };
            table.Controls.Add(buttonPanel);

            addButton = new Button
            {
                Text = "Add",
                Height = 40,
                BackColor = Color.LightGray,
            };
            buttonPanel.Controls.Add(addButton);
            addButton.Click += AddButtonClick;

            removeButton = new Button
            {
                Text = "Remove",
                Height = 40,
                BackColor = Color.LightGray
            };
            buttonPanel.Controls.Add(removeButton);
            removeButton.Click += RemoveButtonClick;

            clearCart = new Button
            {
                Text = "Clear cart",
                Height = 40,
                BackColor = Color.LightGray
            };
            buttonPanel.Controls.Add(clearCart);
            clearCart.Click += ClearCart_Click; ;

            //panel som skräpas bara 
            table.Controls.Add(new Panel());

            //lägga till så att när man klickar på knappen så ska varukorgen sparas i en textfile
            FlowLayoutPanel costPanel = new FlowLayoutPanel
            {
                //ColumnCount = 2,
                //RowCount = 2,
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill,
            };
            table.Controls.Add(costPanel);

            discountBox = new TextBox
            {
                Text = "Discount Code",
                //Dock = DockStyle.Left,
                //BackColor = Color.Green
            };
            costPanel.Controls.Add(discountBox);
            discountBox.Click += DiscountBox_Click;
            discountBox.KeyUp += DiscountBox_KeyUp;

            totalCost = new Label
            {
                Margin = new Padding(0, 5, 0, 0),
                Text = "Total:",
                //Dock = DockStyle.Fill,
                //BackColor = Color.Blue
            };
            costPanel.Controls.Add(totalCost);

            receiptButton = new Button
            {
                Text = "Show receipt",
                Width = 100,
                Height = 40,
                BackColor = Color.LightGray,
                //Dock = DockStyle.Right,
            };
            costPanel.Controls.Add(receiptButton);
            receiptButton.Click += ReceiptButton_Click;

            cart = new Cart();
            cart.LoadCart(productList);
            UpdateCartListView();
        }

        private void ReceiptButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cart.Receipt());
        }

        private void DiscountBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (discountBox.Text == "")
                {
                    cart.SetDiscountCode(null);
                    MessageBox.Show("You have no discount");
                }
                else
                {
                    Discount discount = cart.IsDiscountCodeValid(discountBox.Text);

                    if (discount != null)
                    {
                        MessageBox.Show(string.Format("Your discount is {0}%", discount.Percentage));
                        cart.SetDiscountCode(discount);
                    }
                    else
                    {
                        MessageBox.Show("Invalid discount code!");
                    }
                }
                // Update the card list view with the current cart
                UpdateCartListView();
            }
        }

        private void DiscountBox_Click(object sender, EventArgs e)
        {
            discountBox.SelectAll();
        }

        private void UpdateCartListView()
        {
            // Update the card list view with the current cart
            cartIteamsView.Items.Clear();
            foreach (KeyValuePair<Product, int> item in cart.ProductsInCart)
            {
                cartIteamsView.Items.Add(item.Key.ToCartListViewItem(item.Value));

            }
            cartIteamsView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            cartIteamsView.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);

            totalCost.Text = "Total: " + cart.TotalCost().ToString();
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
            colHead4.Text = "Total";
            colHead4.Width = -2;
            listView.Columns.Add(colHead4);
        }

        //productList
        private void RemoveButtonClick(object sender, EventArgs e)
        {
            //selectedItemsView.Items.Remove(selectedItemsView.SelectedItem);
            foreach (ListViewItem item in cartIteamsView.SelectedItems)
            {
                Product selectedProduct = productList.Single(m => m.Name == item.Text);

                cart.RemoveProduct(selectedProduct);
            }

            UpdateCartListView();

            cart.SaveCart();
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            // Add selected product to cart
            foreach (ListViewItem item in productsItemsView.SelectedItems)
            {
                Product selectedProduct = productList.Single(m => m.Name == item.Text);

                cart.AddProduct(selectedProduct);
            }
            // Update the card list view with the current cart
            UpdateCartListView();

            cart.SaveCart();
        }

        private void ClearCart_Click(object sender, EventArgs e)
        {
            cartIteamsView.Items.Clear();
            cartIteamsView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            cartIteamsView.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);

            cart.ProductsInCart.Clear();
            cart.SaveCart();
        }

        private void DisplayDescription(object sender, EventArgs e)
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

