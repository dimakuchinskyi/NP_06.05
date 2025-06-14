using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using var client = new TcpClient();
        await client.ConnectAsync("127.0.0.1", 5000);
        var stream = client.GetStream();

        string message = "Hello server";
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(messageBytes, 0, messageBytes.Length);

        var buffer = new byte[1024];
        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
        string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        var remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
        Console.WriteLine($"At {DateTime.Now:HH:mm} from {remoteEndPoint?.Address} received: {received}");
    }
}