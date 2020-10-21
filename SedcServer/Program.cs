using SedcServer.Engine;

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

            while (true) {
                Console.WriteLine("Waiting for request");
                var client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected");
                var stream = client.GetStream();

                // Step 1: Accept the request and get the data
                var request = RequestGetter.GetRequest(stream);

                // Step 2: Transform the request object into a response object
                var response = ServerEngine.Process(request);

                // Step 3: Sent the response data and close the request
                ResponseSender.SendResponse(stream, response);

                Console.WriteLine("Sent response");
                client.Close();
            }

        }
    }
}
