using System.Net.Sockets;
using System.Text;

Console.WriteLine("Enter 'date' or 'time':");
string command = Console.ReadLine();

using var client = new TcpClient();
await client.ConnectAsync("127.0.0.1", 5000);
using var stream = client.GetStream();
var data = Encoding.UTF8.GetBytes(command);
await stream.WriteAsync(data, 0, data.Length);

var buffer = new byte[256];
int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

Console.WriteLine("Server response: " + response);