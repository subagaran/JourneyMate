using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MauiTcpClient
{

    public class SyncService
    {
        private readonly string serverIp = "192.168.1.101"; // Replace with your Windows server IP
        private readonly int port = 5000;

        public async Task<List<Item>> GetItemsAsync()
        {
            var request = JsonConvert.SerializeObject(new { action = "get_items" });
            string response = await SendAsync(request);
            return JsonConvert.DeserializeObject<List<Item>>(response);
        }

        public async Task<bool> UpdateQtyAsync(int itemId, int qty)
        {
            var request = JsonConvert.SerializeObject(new { action = "update_qty", itemId, qty });
            string response = await SendAsync(request);
            var result = JsonConvert.DeserializeObject<dynamic>(response);
            return result.status == "success";
        }

        private async Task<string> SendAsync(string message)
        {
            using TcpClient client = new TcpClient();
            await client.ConnectAsync(serverIp, port);

            NetworkStream stream = client.GetStream();
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(buffer, 0, buffer.Length);

            byte[] responseBuffer = new byte[4096];
            int read = await stream.ReadAsync(responseBuffer, 0, responseBuffer.Length);

            return Encoding.UTF8.GetString(responseBuffer, 0, read);
        }
    }
}
 


