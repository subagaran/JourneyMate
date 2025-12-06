using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonWatcherSQLUpdater
{
    public partial class Form1 : Form
    {
        private string jsonPath = @"C:\SharedFolder\items.json";
        private string connStr = @"Server=SUBAGARAN\SQL2022;Database=AuraMdQ;User Id=SA;Password=2025@speedup;";
        private TcpJsonServer tcpServer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tcpServer = new TcpJsonServer(5000);
            Task.Run(() => tcpServer.Start());

            lblStatus.Text = "TCP Server Running on port 5000...";
        }

        private void GenerateJsonFromSql()
        {
            try
            {
                // Create folder if missing
                var folder = Path.GetDirectoryName(jsonPath);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                List<Item> items = new List<Item>();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string query = @"SELECT ItemId, ItemCode, ItemName, GenericName, Description, Strength, Form,
                                        CostPrice, SellingPrice, MRP, Qty 
                                     FROM tblItem";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Item item = new Item
                        {
                            ItemId = reader["ItemId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ItemId"]),
                            ItemCode = reader["ItemCode"] == DBNull.Value ? "" : reader["ItemCode"].ToString(),
                            ItemName = reader["ItemName"] == DBNull.Value ? "" : reader["ItemName"].ToString(),
                            GenericName = reader["GenericName"] == DBNull.Value ? "" : reader["GenericName"].ToString(),
                            Description = reader["Description"] == DBNull.Value ? "" : reader["Description"].ToString(),
                            Strength = reader["Strength"] == DBNull.Value ? "" : reader["Strength"].ToString(),
                            Form = reader["Form"] == DBNull.Value ? "" : reader["Form"].ToString(),

                            CostPrice = reader["CostPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["CostPrice"]),
                            SellingPrice = reader["SellingPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SellingPrice"]),
                            MRP = reader["MRP"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["MRP"]),

                            Qty = reader["Qty"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Qty"])
                        };

                        items.Add(item);
                    }
                }

                // Convert to JSON (Formatted)
                string json = JsonConvert.SerializeObject(items, Formatting.Indented);

                // Save to file
                File.WriteAllText(jsonPath, json);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating JSON: " + ex.Message);
            }
        }
    }

    // Item Model
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string GenericName { get; set; }
        public string Description { get; set; }
        public string Strength { get; set; }
        public string Form { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal MRP { get; set; }
        public int Qty { get; set; }
    }
}
