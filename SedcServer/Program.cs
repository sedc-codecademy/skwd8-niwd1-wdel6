using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SedcServer
{
    class Program
    {


        static void Main(string[] args)
        {
            var address = IPAddress.Loopback;
            var port = 668;

            TcpListener listener = new TcpListener(address, port);
            listener.Start();

            Console.WriteLine("Started listening");

            var client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected");

            var stream = client.GetStream();

            // Weko needs to learn how to do this with spans
            //Span<byte> data = new Span<byte>();
            //stream.Read(data);
            //Console.WriteLine(data.ToByteString());

            byte[] bytes = new byte[256];

            var readCount = stream.Read(bytes, 0, bytes.Length);

            string requestData = string.Empty;

            while (readCount != 0)
            {
                var realData = Encoding.ASCII.GetString(bytes, 0, readCount);
                requestData += realData;
                readCount = stream.Read(bytes, 0, bytes.Length);
            }

            Console.WriteLine(requestData);

            stream.Close();
            client.Close();
        }
    }
}
