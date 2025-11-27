using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Server
{
    private const int Port = 5000;

    static void server()
    {
        Console.WriteLine("Server đang chạy...");
        TcpListener listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Task.Run(() => HandleClient(client));
        }
    }

    private static void HandleClient(TcpClient client)
    {
        try
        {
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
            {
                string request = reader.ReadLine();
                Console.WriteLine("Client send: " + request);

                if (request == "GET_FOOD_LIST")
                {
                    var foodList = DatabaseHelper.FoodList();
                    writer.WriteLine(string.Join("|", foodList));
                }
                else if (request.StartsWith("ADD_FOOD|"))
                {
                    var parts = request.Split('|');
                    if (parts.Length == 4)
                    {
                        string tenMon = parts[1];
                        string hinhAnh = parts[2];
                        string nguoi = parts[3];
                        DatabaseHelper.AddFood(tenMon, hinhAnh, nguoi);
                        writer.WriteLine("OK");
                    }
                    else
                    {
                        writer.WriteLine("ERROR|Dữ liệu không hợp lệ");
                    }
                }
                else
                {
                    writer.WriteLine("ERROR|Lệnh không xác định");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi: " + ex.Message);
        }
        finally
        {
            client.Close();
        }
    }
}
