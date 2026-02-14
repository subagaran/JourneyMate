using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HotelPOS
{
    public partial class FormSALEPOS : Form
    {
        private int SelectedCustomerId = 0;
        private bool _isSearchingCustomer = false;

        // Keyboard sizing
        private int _keySize;
        private int _keyFontSize;

        public FormSALEPOS()
        {
            InitializeComponent();
        }

        private void FormSALEPOS_Load(object sender, EventArgs e)
        {
            InitializeCartListView();
            LoadCategories();
            LoadTables();

           // ApplyResponsiveLayout();
            SetupKeyboardLayout();
            BuildBillingKeyboard();
            txtItemSearch.Focus();
        }

        // =====================================================
        // RESPONSIVE LAYOUT (CRITICAL FOR 1024x768)
        // =====================================================

        // =====================================================
        // TOUCH KEYBOARD (HOTEL POS VERSION)
        // =====================================================

        private void SetupKeyboardLayout()
        {
            tblKeyboard.ColumnStyles.Clear();
            tblKeyboard.RowStyles.Clear();

            for (int i = 0; i < 10; i++)
                tblKeyboard.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 10F));

            for (int i = 0; i < 5; i++)
                tblKeyboard.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 20F));
        }
        private void BuildBillingKeyboard()
        {
            tblKeyboard.Controls.Clear();

            // Define keys layout
            string[,] keys =
            {
        { "1","2","3","4","5","6","7","8","9","0" },
        { "Q","W","E","R","T","Y","U","I","O","P" },
        { "A","S","D","F","G","H","J","K","L","⌫" },
        { "Z","X","C","V","B","N","M",".","ENTER","" }, // ENTER will span
        { "SPACE","","","","","","CLEAR ALL","","","" }  // Bottom row for SPACE and CLEAR ALL
    };

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    string key = keys[r, c];
                    if (string.IsNullOrEmpty(key)) continue;

                    Button btn = new Button
                    {
                        Text = key,
                        Dock = DockStyle.Fill,
                        Margin = new Padding(4),
                        Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                        BackColor = Color.FromArgb(60, 60, 60),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    btn.FlatAppearance.BorderSize = 0;
                    // btn.Click += KeyboardButton_Click; // Attach your click handler here

                    tblKeyboard.Controls.Add(btn, c, r);

                    // Span logic
                    if (key == "SPACE")
                        tblKeyboard.SetColumnSpan(btn, 6); // Wide space button

                    if (key == "ENTER")
                    {
                        tblKeyboard.SetColumnSpan(btn, 2); // Span 2 columns
                        tblKeyboard.SetRowSpan(btn, 2);    // Span 2 rows
                    }

                    if (key == "CLEAR ALL")
                    {
                        tblKeyboard.SetColumnSpan(btn, 2); // Span 2 columns 
                    }
                }
            }
        }

        private Button CreateKeyButton(string key)
        {
            Button btn = new Button
            {
                Text = key,
                Width = _keySize,
                Height = _keySize,
                Font = new Font("Segoe UI", _keyFontSize, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Margin = new Padding(3)
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.Click += Keyboard_Click;

            switch (key)
            {
                case "SPACE":
                    btn.Width = _keySize * 5;
                    btn.BackColor = Color.FromArgb(90, 90, 90);
                    break;

                case "BACK":
                    btn.Text = "⌫";
                    btn.Width = _keySize * 2;
                    btn.BackColor = Color.DarkOrange;
                    break;

                case "CLEAR":
                    btn.Width = _keySize * 2;
                    btn.BackColor = Color.DarkRed;
                    break;

                default:
                    btn.BackColor = Color.FromArgb(60, 60, 60);
                    break;
            }

            return btn;
        }

        private void Keyboard_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;

            switch (btn.Text)
            {
                case "CLEAR":
                    txtItemSearch.Clear();
                    break;

                case "⌫":
                    if (txtItemSearch.Text.Length > 0)
                        txtItemSearch.Text =
                            txtItemSearch.Text.Substring(0, txtItemSearch.Text.Length - 1);
                    break;

                case "SPACE":
                    txtItemSearch.Text += " ";
                    break;

                default:
                    txtItemSearch.Text += btn.Text;
                    break;
            }

            txtItemSearch.SelectionStart = txtItemSearch.Text.Length;
            txtItemSearch.Focus();
        }

        // =====================================================
        // CART
        // =====================================================
        private void InitializeCartListView()
        {
            lvCart.Clear();
            lvCart.View = View.Details;
            lvCart.FullRowSelect = true;
            lvCart.GridLines = true;
            lvCart.Font = new Font("Segoe UI", 10);

            lvCart.Columns.Add("ItemId", 0);
            lvCart.Columns.Add("Item Name", 170);
            lvCart.Columns.Add("Price", 70);
            lvCart.Columns.Add("Qty", 50);
            lvCart.Columns.Add("Total", 80);
        }

        // =====================================================
        // TABLES (SAMPLE)
        // =====================================================
        private void LoadTables()
        {
            flpTables.Controls.Clear();

            for (int i = 1; i <= 12; i++)
            {
                Button btn = new Button
                {
                    Text = "Table " + i,
                    Width = 100,
                    Height = 45,
                    BackColor = Color.Green,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(5)
                };

                btn.FlatAppearance.BorderSize = 0;
                flpTables.Controls.Add(btn);
            }
        }

        // =====================================================
        // CATEGORIES (DB)
        // =====================================================
        private void LoadCategories()
        {
            flpCategories.Controls.Clear();

            DataTable dt = DBHelper.GetData(
                "SELECT CategoryId, CategoryName FROM Categories WHERE IsActive = 1");

            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button
                {
                    Text = row["CategoryName"].ToString(),
                    Tag = row["CategoryId"],
                    Width = 160,
                    Height = 65,
                    BackColor = Color.FromArgb(70, 70, 70),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(5)
                };

                btn.FlatAppearance.BorderSize = 0;
                btn.Click += (s, e) =>
                {
                    txtItemSearch.Clear();
                    LoadItems(Convert.ToInt32(btn.Tag));
                };

                flpCategories.Controls.Add(btn);
            }
        }

        // =====================================================
        // ITEMS
        // =====================================================
        private void LoadItems(int categoryId)
        {
            DataTable dt = DBHelper.GetData(
     "SELECT ItemId, ItemName, Price, ImagePath FROM Items WHERE CategoryId=@C",
     new SqlParameter[]
     {
        new SqlParameter("@C", categoryId)
     }
 );


            flpItems.Controls.Clear();

            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button
                {
                    Width = 150,
                    Height = 150,
                    Margin = new Padding(8),
                    Tag = row["ItemId"],
                    BackColor = Color.FromArgb(90, 90, 90),
                    FlatStyle = FlatStyle.Flat
                };

                btn.FlatAppearance.BorderSize = 0;

                btn.Paint += (s, e) =>
                {
                    Rectangle r = new Rectangle(0, btn.Height - 45, btn.Width, 45);
                    e.Graphics.FillRectangle(
                        new SolidBrush(Color.FromArgb(200, 0, 0, 0)), r);

                    e.Graphics.DrawString(
                        $"{row["ItemName"]}\nRs. {row["Price"]}",
                        new Font("Segoe UI", 9, FontStyle.Bold),
                        Brushes.White,
                        r,
                        new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        });
                };

                flpItems.Controls.Add(btn);
            }
        }
    }
}
