using System;
using System.Collections.Generic;
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

        // Class-level dictionary to reuse buttons
        private Dictionary<string, Button> _keyboardButtons = new Dictionary<string, Button>();

        // Class representing each key
        private class KeyDefinition
        {
            public string Text { get; set; }
            public int Row { get; set; }
            public int Column { get; set; }
            public int ColSpan { get; set; } = 1;
            public int RowSpan { get; set; } = 1;
            public Color BackColor { get; set; } = Color.DarkGray;
        }

        // Track which textbox is currently active
        private TextBox _activeTextBox = null;

        // Attach Enter events to all input textboxes
        private void SetupTextBoxes()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox tb)
                    tb.Enter += (s, e) => _activeTextBox = tb;
            }

            // Or manually:
            // txtItemSearch.Enter += (s, e) => _activeTextBox = txtItemSearch;
            // txtQty.Enter += (s, e) => _activeTextBox = txtQty;
        }

        private void BuildBillingKeyboard()
        {
            tblKeyboard.Controls.Clear();
            tblKeyboard.SuspendLayout();

            // Define all keys
            var keys = new List<KeyDefinition>
    {
        // Numbers
        new KeyDefinition{Text="1", Row=0, Column=0, BackColor=Color.FromArgb(0,120,215)},
        new KeyDefinition{Text="2", Row=0, Column=1, BackColor=Color.FromArgb(0,120,215)},
        new KeyDefinition{Text="3", Row=0, Column=2, BackColor=Color.FromArgb(0,120,215)},
        new KeyDefinition{Text="4", Row=0, Column=3, BackColor=Color.FromArgb(0,120,215)},
        new KeyDefinition{Text="5", Row=0, Column=4, BackColor=Color.FromArgb(0,120,215)},
        new KeyDefinition{Text="6", Row=0, Column=5, BackColor=Color.FromArgb(0,120,215)},
        new KeyDefinition{Text="7", Row=0, Column=6, BackColor=Color.FromArgb(0,120,215)},
        new KeyDefinition{Text="8", Row=0, Column=7, BackColor=Color.FromArgb(0,120,215)},
        new KeyDefinition{Text="9", Row=0, Column=8, BackColor=Color.FromArgb(0,120,215)},
        new KeyDefinition{Text="0", Row=0, Column=9, BackColor=Color.FromArgb(0,120,215)},

        // Letters Row 1
        new KeyDefinition{Text="Q", Row=1, Column=0}, new KeyDefinition{Text="W", Row=1, Column=1},
        new KeyDefinition{Text="E", Row=1, Column=2}, new KeyDefinition{Text="R", Row=1, Column=3},
        new KeyDefinition{Text="T", Row=1, Column=4}, new KeyDefinition{Text="Y", Row=1, Column=5},
        new KeyDefinition{Text="U", Row=1, Column=6}, new KeyDefinition{Text="I", Row=1, Column=7},
        new KeyDefinition{Text="O", Row=1, Column=8}, new KeyDefinition{Text="P", Row=1, Column=9},

        // Letters Row 2
        new KeyDefinition{Text="A", Row=2, Column=0}, new KeyDefinition{Text="S", Row=2, Column=1},
        new KeyDefinition{Text="D", Row=2, Column=2}, new KeyDefinition{Text="F", Row=2, Column=3},
        new KeyDefinition{Text="G", Row=2, Column=4}, new KeyDefinition{Text="H", Row=2, Column=5},
        new KeyDefinition{Text="J", Row=2, Column=6}, new KeyDefinition{Text="K", Row=2, Column=7},
        new KeyDefinition{Text="L", Row=2, Column=8},
        new KeyDefinition{Text="⌫", Row=2, Column=9, BackColor=Color.Orange},

        // Letters Row 3 + ENTER
        new KeyDefinition{Text="Z", Row=3, Column=0}, new KeyDefinition{Text="X", Row=3, Column=1},
        new KeyDefinition{Text="C", Row=3, Column=2}, new KeyDefinition{Text="V", Row=3, Column=3},
        new KeyDefinition{Text="B", Row=3, Column=4}, new KeyDefinition{Text="N", Row=3, Column=5},
        new KeyDefinition{Text="M", Row=3, Column=6}, new KeyDefinition{Text=".", Row=3, Column=7, BackColor=Color.Teal},
        new KeyDefinition{Text="ENTER", Row=3, Column=8, ColSpan=2, RowSpan=2, BackColor=Color.Green},

        // Bottom Row SPACE + CLEAR ALL
        new KeyDefinition{Text="SPACE", Row=4, Column=0, ColSpan=6, BackColor=Color.Gray},
        new KeyDefinition{Text="CLEAR ALL", Row=4, Column=6, ColSpan=2, BackColor=Color.Red}
    };

            // Add buttons
            foreach (var k in keys)
            {
                Button btn;
                string keyId = $"{k.Row}-{k.Column}";
                if (_keyboardButtons.ContainsKey(keyId))
                    btn = _keyboardButtons[keyId]; // reuse
                else
                {
                    btn = new Button { Dock = DockStyle.Fill, Margin = new Padding(3), FlatStyle = FlatStyle.Flat, ForeColor = Color.White };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += Keyboard_Click;
                    _keyboardButtons[keyId] = btn;
                }

                btn.Text = k.Text;
                btn.BackColor = k.BackColor;
                btn.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

                // Optional icons
                if (k.Text == "ENTER") btn.Text = "✔\nENTER";
                if (k.Text == "⌫") btn.Text = "←\nBACK";
                if (k.Text == "CLEAR ALL") btn.Text = "✖\nCLEAR";

                tblKeyboard.Controls.Add(btn, k.Column, k.Row);

                if (k.ColSpan > 1) tblKeyboard.SetColumnSpan(btn, k.ColSpan);
                if (k.RowSpan > 1) tblKeyboard.SetRowSpan(btn, k.RowSpan);
            }

            tblKeyboard.ResumeLayout();
        }

        // Keyboard click handler (multi-textbox support)
        private void Keyboard_Click(object sender, EventArgs e)
        {
            if (_activeTextBox == null || !(sender is Button btn)) return;

            switch (btn.Text)
            {
                case "✖\nCLEAR":
                case "CLEAR ALL":
                    _activeTextBox.Clear();
                    break;

                case "←\nBACK":
                case "⌫":
                    if (_activeTextBox.Text.Length > 0)
                        _activeTextBox.Text = _activeTextBox.Text.Substring(0, _activeTextBox.Text.Length - 1);
                    break;

                case "SPACE":
                    _activeTextBox.Text += " ";
                    break;

                case "✔\nENTER":
                case "ENTER":
                    this.SelectNextControl(_activeTextBox, true, true, true, true); // focus next textbox
                    break;

                default:
                    _activeTextBox.Text += btn.Text;
                    break;
            }

            _activeTextBox.SelectionStart = _activeTextBox.Text.Length;
            _activeTextBox.Focus();
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
    //    private void BuildBillingKeyboard()
    //    {
    //        tblKeyboard.Controls.Clear();

    //        // Define keys layout
    //        string[,] keys =
    //        {
    //    { "1","2","3","4","5","6","7","8","9","0" },
    //    { "Q","W","E","R","T","Y","U","I","O","P" },
    //    { "A","S","D","F","G","H","J","K","L","⌫" },
    //    { "Z","X","C","V","B","N","M",".","ENTER","" },
    //    { "SPACE","","","","","","CLEAR ALL","","","" }
    //};

    //        for (int r = 0; r < 5; r++)
    //        {
    //            for (int c = 0; c < 10; c++)
    //            {
    //                string key = keys[r, c];
    //                if (string.IsNullOrEmpty(key)) continue;

    //                Button btn = new Button
    //                {
    //                    Text = key,
    //                    Dock = DockStyle.Fill,
    //                    Margin = new Padding(4),
    //                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
    //                    FlatStyle = FlatStyle.Flat
    //                };
    //                btn.FlatAppearance.BorderSize = 0;

    //                // Assign colors by key type
    //                if ("0123456789".Contains(key)) // Numbers
    //                    btn.BackColor = Color.FromArgb(0, 120, 215); // Blue
    //                else if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(key)) // Letters
    //                    btn.BackColor = Color.FromArgb(80, 80, 80); // Dark Gray
    //                else if (key == ".")
    //                    btn.BackColor = Color.FromArgb(0, 150, 136); // Teal
    //                else if (key == "ENTER")
    //                {
    //                    btn.BackColor = Color.FromArgb(0, 200, 0); // Green
    //                    btn.ForeColor = Color.White;
    //                    btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
    //                    tblKeyboard.SetColumnSpan(btn, 2);
    //                    tblKeyboard.SetRowSpan(btn, 2);
    //                }
    //                else if (key == "CLEAR ALL")
    //                {
    //                    btn.BackColor = Color.FromArgb(200, 0, 0); // Red
    //                    btn.ForeColor = Color.White;
    //                    btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
    //                    tblKeyboard.SetColumnSpan(btn, 2);
    //                }
    //                else if (key == "⌫") // Backspace
    //                    btn.BackColor = Color.FromArgb(255, 140, 0); // Orange
    //                else if (key == "SPACE")
    //                {
    //                    btn.BackColor = Color.FromArgb(120, 120, 120); // Gray
    //                    tblKeyboard.SetColumnSpan(btn, 6);
    //                }

    //                btn.ForeColor = Color.White;

    //                // Optional: add icon for ENTER or BACKSPACE
    //                if (key == "ENTER")
    //                    btn.Text = "✔\nENTER"; // tick icon + text
    //                else if (key == "⌫")
    //                    btn.Text = "←\nBACK";

    //                btn.Click += Keyboard_Click;

    //                tblKeyboard.Controls.Add(btn, c, r);
    //            }
    //        }
    //    }


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

        //private void Keyboard_Click(object sender, EventArgs e)
        //{
        //    if (!(sender is Button btn)) return;

        //    switch (btn.Text)
        //    {
        //        case "CLEAR":
        //            txtItemSearch.Clear();
        //            break;

        //        case "⌫":
        //            if (txtItemSearch.Text.Length > 0)
        //                txtItemSearch.Text =
        //                    txtItemSearch.Text.Substring(0, txtItemSearch.Text.Length - 1);
        //            break;

        //        case "SPACE":
        //            txtItemSearch.Text += " ";
        //            break;

        //        default:
        //            txtItemSearch.Text += btn.Text;
        //            break;
        //    }

        //    txtItemSearch.SelectionStart = txtItemSearch.Text.Length;
        //    txtItemSearch.Focus();
        //}

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
