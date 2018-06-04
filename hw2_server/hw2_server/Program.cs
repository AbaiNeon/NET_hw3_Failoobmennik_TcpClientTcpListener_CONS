using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace hw2_server
{
    class Program
    {
        static string path = @"C:\images\";
        static void Main(string[] args)
        {
            

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 3535);
            try
            {
                server.Start();
                //int name = 1;

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();

                    Task task = new Task(() => Proccess(client));
                    task.Start();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.Message);
            }
            finally
            {
                server.Stop();
                Console.ReadLine();
            }
        }

        private static void Proccess(TcpClient client)
        {
            int bytes;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            MemoryStream ms = new MemoryStream();

            using (var networkStream = client.GetStream())
            {
                do
                {
                    bytes = networkStream.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, buffer.Length);
                }
                while (networkStream.DataAvailable);
            }

            using (Image image = Image.FromStream(ms))
            {
                image.Save(path + "output.jpg");  // Or Png
            }

            ms.Close();
            client.Close();
        }
    }
}
