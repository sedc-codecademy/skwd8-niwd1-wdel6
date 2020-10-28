using ServerCore.Engine;

using ServerEntities.Logging;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    public class WebServer
    {
        public IPAddress Address { get; private set; }

        public int Port { get; private set; }

        public ILogger Logger { get; set; }

        public WebServer(IPAddress address, int port, ILogger logger = null)
        {
            Address = address;
            Port = port;
            Logger = logger ?? new ConsoleLogger();
        }

        public void Run()
        {
            TcpListener listener = new TcpListener(Address, Port);
            listener.Start();
            Logger.Info("Started listening");

            while (true)
            {
                Logger.Info("Waiting for request");
                var client = listener.AcceptTcpClient();
                Logger.Info("Client connected");
                var stream = client.GetStream();

                // Step 1: Accept the request and get the data
                var request = RequestGetter.GetRequest(stream, Logger);

                // Step 2: Transform the request object into a response object
                var response = ServerEngine.Process(request);

                // Step 3: Sent the response data and close the request
                ResponseSender.SendResponse(stream, response, Logger);

                Logger.Info("Sent response");
                client.Close();
            }
        }
    }
}
