using System;
using System.Collections.Generic;
using System.Data.SqlClient; 

public static class Database
{
    private static string connString =
        @"Server=192.168.1.101\SQL2022;Database=AuraMdQ;User Id=sa;Password=2025@speedup;";

    public static List<Item> GetItems()
    {
        List<Item> list = new List<Item>();

        try
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = @"SELECT ItemId, ItemName, Qty FROM tblItem";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Item
                        {
                            ItemId = rd["ItemId"] != DBNull.Value ? Convert.ToInt32(rd["ItemId"]) : 0,
                            ItemName = rd["ItemName"] != DBNull.Value ? rd["ItemName"].ToString() : "",
                            Qty = rd["Qty"] != DBNull.Value ? Convert.ToInt32(rd["Qty"]) : 0
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Optional: log error
            Console.WriteLine("Error fetching items: " + ex.Message);
        }

        return list;
    }


    public static object UpdateQty(int itemId, int qty)
    {
        SqlConnection conn = new SqlConnection(connString);
        conn.Open();

        string query = "UPDATE tblItem SET Qty=@qty WHERE ItemId=@id";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@qty", qty);
        cmd.Parameters.AddWithValue("@id", itemId);

        int rows = cmd.ExecuteNonQuery();

        return new { status = rows > 0 ? "success" : "failed" };
    }
}
