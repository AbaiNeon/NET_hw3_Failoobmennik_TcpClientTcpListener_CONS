using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace hw3_client
{
    class Program
    {
        private const int port = 3535;
        private const string server = "127.0.0.1";
        
        static void Main(string[] args)
        {
            string path1 = @"http://bipbap.ru/wp-content/uploads/2017/09/Cool-High-Resolution-Wallpaper-1920x1080.jpg";
            string path2 = @"http://elitefon.ru/images/201211/elitefon.ru_6492.jpg";
            string path3 = @"https://ribalych.ru/wp-content/uploads/2017/05/prikoly_000-17.jpg";

            try
            {
                //Image to byte array from a url
                byte[] imageBytes;
                using (WebClient webClient = new WebClient())
                {
                    imageBytes = webClient.DownloadData(path3);
                }

                TcpClient client = new TcpClient();
                client.Connect(server, port);

                NetworkStream stream = client.GetStream();
                //var buffer = Encoding.Default.GetBytes(path1);

                stream.Write(imageBytes, 0, imageBytes.Length);

                stream.Close();
                client.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Console.Read();
        }
    }
}
