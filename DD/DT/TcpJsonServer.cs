using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text; 
using System.Threading.Tasks;

namespace JsonWatcherSQLUpdater
{
    public class TcpJsonServer
    {
        private readonly int port;

        public TcpJsonServer(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            Console.WriteLine("TCP Server Started...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
        } 

private void HandleClient(TcpClient client)
    {
        try
        {
            NetworkStream ns = client.GetStream();

            // Read data from client
            byte[] buffer = new byte[4096];
            int read = ns.Read(buffer, 0, buffer.Length);

            string json = Encoding.UTF8.GetString(buffer, 0, read);

            // Deserialize request using Newtonsoft.Json
            var request = JsonConvert.DeserializeObject<Request>(json);

            // Process the request
            string response = RequestProcessor.Handle(request);

            // Send response back to client
            byte[] respBytes = Encoding.UTF8.GetBytes(response);
            ns.Write(respBytes, 0, respBytes.Length);

            client.Close();
        }
        catch (Exception ex)
        {
            // Optional: log the exception for debugging
            Console.WriteLine("Error handling client: " + ex.Message);
        }
    }


}

}
