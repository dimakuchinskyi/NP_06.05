using System.Net;
using System.Net.Sockets;
using System.Text;

var listener = new TcpListener(IPAddress.Any, 5000);
listener.Start();
Console.WriteLine("Async server started...");

while (true)
{
    var client = await listener.AcceptTcpClientAsync();
    _ = Task.Run(async () =>
    {
        using (client)
        using (var stream = client.GetStream())
        {
            var buffer = new byte[256];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            var request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

            string response = request switch
            {
                "date" => DateTime.Now.ToShortDateString(),
                "time" => DateTime.Now.ToLongTimeString(),
                _ => "Unknown command"
            };

            var responseBytes = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    });
}