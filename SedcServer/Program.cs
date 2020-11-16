using ServerCore.Engine;

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
            
            var logger = new CompositeLogger(new ConsoleLogger(LogLevel.Info), new FileLogger("log.txt"));
            logger.Add(new FileLogger("other-log.txt", LogLevel.Error));

            var server = new WebServer(address, port, logger);

            server.RegisterProcessor<HiProcessor>();
            server.RegisterProcessor(new AngularProcessor(@"C:\Source\SEDC\skwd8-niwd1-wdel6\AngularSite\demo\dist\demo"));
            server.RegisterProcessor<ApiProcessor>();
            server.RegisterProcessor<CalcApiProcessor>();
            server.RegisterProcessor(new FileProcessor());

            server.Run();

        }
    }
}
