using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelPOS
{
    partial class FormSALEPOS
    {
        private System.ComponentModel.IContainer components = null;

        // -------------------- Class-level declarations --------------------
        // Header
        private Panel pnlHeader;
        private Label lblSystemTitle;
        private Label lblUser;

        // Search / Customer
        private Panel pnlSearch;
        private TextBox txtItemSearch;
        private Label lblCustomer;
        private TextBox txtCustomerPhone;
        private Button btnSelectCustomer;
        private Button btnAddCustomer;
        private Button btnClearCustomer;

        // Tables / Categories / Center / Bill
        private Panel pnlTables;
        private FlowLayoutPanel flpTables;

        private Panel pnlCategories;
        private FlowLayoutPanel flpCategories;

        private Panel pnlCenterContainer;
        private Panel pnlItems;
        private FlowLayoutPanel flpItems;

        private Panel pnlKeyboard; 

        private Panel pnlBill;
        private Label lblCartTitle;
        private ListView lvCart;
        private Label lblTotal;
        private Button btnHold;
        private Button btnCancel;
        private Button btnPay;

        private TableLayoutPanel tblKeyboard;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSystemTitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtItemSearch = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.btnSelectCustomer = new System.Windows.Forms.Button();
            this.txtCustomerPhone = new System.Windows.Forms.TextBox();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnClearCustomer = new System.Windows.Forms.Button();
            this.pnlTables = new System.Windows.Forms.Panel();
            this.flpTables = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCategories = new System.Windows.Forms.Panel();
            this.flpCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCenterContainer = new System.Windows.Forms.Panel();
            this.pnlItems = new System.Windows.Forms.Panel();
            this.flpItems = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlKeyboard = new System.Windows.Forms.Panel();
            this.tblKeyboard = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBill = new System.Windows.Forms.Panel();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.lvCart = new System.Windows.Forms.ListView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnPay = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnHold = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlTables.SuspendLayout();
            this.pnlCategories.SuspendLayout();
            this.pnlCenterContainer.SuspendLayout();
            this.pnlItems.SuspendLayout();
            this.pnlKeyboard.SuspendLayout();
            this.pnlBill.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.pnlHeader.Controls.Add(this.lblSystemTitle);
            this.pnlHeader.Controls.Add(this.lblUser);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHeader.Size = new System.Drawing.Size(1280, 60);
            this.pnlHeader.TabIndex = 5;
            // 
            // lblSystemTitle
            // 
            this.lblSystemTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSystemTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSystemTitle.ForeColor = System.Drawing.Color.Gold;
            this.lblSystemTitle.Location = new System.Drawing.Point(10, 10);
            this.lblSystemTitle.Name = "lblSystemTitle";
            this.lblSystemTitle.Size = new System.Drawing.Size(400, 40);
            this.lblSystemTitle.TabIndex = 0;
            this.lblSystemTitle.Text = "Hotel POS Touch";
            // 
            // lblUser
            // 
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(1070, 10);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(200, 40);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "CASHIER";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.pnlSearch.Controls.Add(this.txtItemSearch);
            this.pnlSearch.Controls.Add(this.lblCustomer);
            this.pnlSearch.Controls.Add(this.btnSelectCustomer);
            this.pnlSearch.Controls.Add(this.txtCustomerPhone);
            this.pnlSearch.Controls.Add(this.btnAddCustomer);
            this.pnlSearch.Controls.Add(this.btnClearCustomer);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 60);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlSearch.Size = new System.Drawing.Size(1280, 60);
            this.pnlSearch.TabIndex = 4;
            // 
            // txtItemSearch
            // 
            this.txtItemSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtItemSearch.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.txtItemSearch.Location = new System.Drawing.Point(715, 10);
            this.txtItemSearch.Name = "txtItemSearch";
            this.txtItemSearch.Size = new System.Drawing.Size(420, 36);
            this.txtItemSearch.TabIndex = 0;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCustomer.ForeColor = System.Drawing.Color.White;
            this.lblCustomer.Location = new System.Drawing.Point(455, 10);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(260, 40);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Customer : WALK-IN";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectCustomer
            // 
            this.btnSelectCustomer.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSelectCustomer.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelectCustomer.FlatAppearance.BorderSize = 0;
            this.btnSelectCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectCustomer.ForeColor = System.Drawing.Color.White;
            this.btnSelectCustomer.Location = new System.Drawing.Point(275, 10);
            this.btnSelectCustomer.Name = "btnSelectCustomer";
            this.btnSelectCustomer.Size = new System.Drawing.Size(180, 40);
            this.btnSelectCustomer.TabIndex = 2;
            this.btnSelectCustomer.Text = "CUSTOMER / LOYALTY";
            this.btnSelectCustomer.UseVisualStyleBackColor = false;
            // 
            // txtCustomerPhone
            // 
            this.txtCustomerPhone.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.txtCustomerPhone.Location = new System.Drawing.Point(15, 10);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.Size = new System.Drawing.Size(260, 32);
            this.txtCustomerPhone.TabIndex = 3;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAddCustomer.FlatAppearance.BorderSize = 0;
            this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCustomer.ForeColor = System.Drawing.Color.White;
            this.btnAddCustomer.Location = new System.Drawing.Point(0, 0);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(160, 36);
            this.btnAddCustomer.TabIndex = 4;
            this.btnAddCustomer.Text = "Add New Customer";
            this.btnAddCustomer.UseVisualStyleBackColor = false;
            this.btnAddCustomer.Visible = false;
            // 
            // btnClearCustomer
            // 
            this.btnClearCustomer.BackColor = System.Drawing.Color.DimGray;
            this.btnClearCustomer.FlatAppearance.BorderSize = 0;
            this.btnClearCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCustomer.ForeColor = System.Drawing.Color.White;
            this.btnClearCustomer.Location = new System.Drawing.Point(0, 0);
            this.btnClearCustomer.Name = "btnClearCustomer";
            this.btnClearCustomer.Size = new System.Drawing.Size(40, 36);
            this.btnClearCustomer.TabIndex = 5;
            this.btnClearCustomer.Text = "X";
            this.btnClearCustomer.UseVisualStyleBackColor = false;
            this.btnClearCustomer.Visible = false;
            // 
            // pnlTables
            // 
            this.pnlTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.pnlTables.Controls.Add(this.flpTables);
            this.pnlTables.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTables.Location = new System.Drawing.Point(0, 120);
            this.pnlTables.Name = "pnlTables";
            this.pnlTables.Padding = new System.Windows.Forms.Padding(5);
            this.pnlTables.Size = new System.Drawing.Size(1280, 70);
            this.pnlTables.TabIndex = 3;
            // 
            // flpTables
            // 
            this.flpTables.AutoScroll = true;
            this.flpTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTables.Location = new System.Drawing.Point(5, 5);
            this.flpTables.Name = "flpTables";
            this.flpTables.Size = new System.Drawing.Size(1270, 60);
            this.flpTables.TabIndex = 0;
            // 
            // pnlCategories
            // 
            this.pnlCategories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlCategories.Controls.Add(this.flpCategories);
            this.pnlCategories.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCategories.Location = new System.Drawing.Point(0, 190);
            this.pnlCategories.Name = "pnlCategories";
            this.pnlCategories.Padding = new System.Windows.Forms.Padding(5);
            this.pnlCategories.Size = new System.Drawing.Size(180, 598);
            this.pnlCategories.TabIndex = 2;
            // 
            // flpCategories
            // 
            this.flpCategories.AutoScroll = true;
            this.flpCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCategories.Location = new System.Drawing.Point(5, 5);
            this.flpCategories.Name = "flpCategories";
            this.flpCategories.Size = new System.Drawing.Size(170, 588);
            this.flpCategories.TabIndex = 0;
            // 
            // pnlCenterContainer
            // 
            this.pnlCenterContainer.BackColor = System.Drawing.Color.LightGray;
            this.pnlCenterContainer.Controls.Add(this.pnlItems);
            this.pnlCenterContainer.Controls.Add(this.pnlKeyboard);
            this.pnlCenterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenterContainer.Location = new System.Drawing.Point(180, 190);
            this.pnlCenterContainer.Name = "pnlCenterContainer";
            this.pnlCenterContainer.Size = new System.Drawing.Size(700, 598);
            this.pnlCenterContainer.TabIndex = 0;
            // 
            // pnlItems
            // 
            this.pnlItems.Controls.Add(this.flpItems);
            this.pnlItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlItems.Location = new System.Drawing.Point(0, 0);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(700, 310);
            this.pnlItems.TabIndex = 0;
            // 
            // flpItems
            // 
            this.flpItems.AutoScroll = true;
            this.flpItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpItems.Location = new System.Drawing.Point(0, 0);
            this.flpItems.Name = "flpItems";
            this.flpItems.Size = new System.Drawing.Size(700, 310);
            this.flpItems.TabIndex = 0;
            // 
            // pnlKeyboard
            // 
            this.pnlKeyboard.Controls.Add(this.tblKeyboard);
            this.pnlKeyboard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlKeyboard.Location = new System.Drawing.Point(0, 310);
            this.pnlKeyboard.Name = "pnlKeyboard";
            this.pnlKeyboard.Padding = new System.Windows.Forms.Padding(5);
            this.pnlKeyboard.Size = new System.Drawing.Size(700, 288);
            this.pnlKeyboard.TabIndex = 1;
            // 
            // tblKeyboard
            // 
            this.tblKeyboard.ColumnCount = 10;
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblKeyboard.Location = new System.Drawing.Point(5, 5);
            this.tblKeyboard.Name = "tblKeyboard";
            this.tblKeyboard.RowCount = 6;
            this.tblKeyboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblKeyboard.Size = new System.Drawing.Size(690, 278);
            this.tblKeyboard.TabIndex = 0;
            // 
            // pnlBill
            // 
            this.pnlBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlBill.Controls.Add(this.lblCartTitle);
            this.pnlBill.Controls.Add(this.lvCart);
            this.pnlBill.Controls.Add(this.lblTotal);
            this.pnlBill.Controls.Add(this.btnPay);
            this.pnlBill.Controls.Add(this.btnCancel);
            this.pnlBill.Controls.Add(this.btnHold);
            this.pnlBill.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBill.Location = new System.Drawing.Point(880, 190);
            this.pnlBill.Name = "pnlBill";
            this.pnlBill.Size = new System.Drawing.Size(400, 598);
            this.pnlBill.TabIndex = 1;
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCartTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCartTitle.ForeColor = System.Drawing.Color.White;
            this.lblCartTitle.Location = new System.Drawing.Point(0, 400);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Size = new System.Drawing.Size(400, 40);
            this.lblCartTitle.TabIndex = 0;
            this.lblCartTitle.Text = "CURRENT ORDER";
            this.lblCartTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvCart
            // 
            this.lvCart.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvCart.FullRowSelect = true;
            this.lvCart.GridLines = true;
            this.lvCart.HideSelection = false;
            this.lvCart.Location = new System.Drawing.Point(0, 50);
            this.lvCart.Name = "lvCart";
            this.lvCart.Size = new System.Drawing.Size(400, 350);
            this.lvCart.TabIndex = 1;
            this.lvCart.UseCompatibleStateImageBehavior = false;
            this.lvCart.View = System.Windows.Forms.View.Details;
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.Lime;
            this.lblTotal.Location = new System.Drawing.Point(0, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.lblTotal.Size = new System.Drawing.Size(400, 50);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.Color.SeaGreen;
            this.btnPay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(0, 408);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(400, 70);
            this.btnPay.TabIndex = 3;
            this.btnPay.Text = "PAY (F12)";
            this.btnPay.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Crimson;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(0, 478);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(400, 60);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "VOID";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnHold
            // 
            this.btnHold.BackColor = System.Drawing.Color.Gray;
            this.btnHold.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnHold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHold.ForeColor = System.Drawing.Color.White;
            this.btnHold.Location = new System.Drawing.Point(0, 538);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(400, 60);
            this.btnHold.TabIndex = 5;
            this.btnHold.Text = "HOLD";
            this.btnHold.UseVisualStyleBackColor = false;
            // 
            // FormSALEPOS
            // 
            this.ClientSize = new System.Drawing.Size(1280, 788);
            this.Controls.Add(this.pnlCenterContainer);
            this.Controls.Add(this.pnlBill);
            this.Controls.Add(this.pnlCategories);
            this.Controls.Add(this.pnlTables);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSALEPOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotel POS System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormSALEPOS_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlTables.ResumeLayout(false);
            this.pnlCategories.ResumeLayout(false);
            this.pnlCenterContainer.ResumeLayout(false);
            this.pnlItems.ResumeLayout(false);
            this.pnlKeyboard.ResumeLayout(false);
            this.pnlBill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
