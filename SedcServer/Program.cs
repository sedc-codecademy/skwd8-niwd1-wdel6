using SedcServer.Engine;

using ServerEntities.Logging;

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
            var logger = new DummyLogger();
            var server = new WebServer(address, port, logger);

            server.Run();

        }
    }
}
