using ServerEntities;
using ServerEntities.Logging;

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SedcServer
{
    public class RequestGetter
    {
        static public Request GetRequest(NetworkStream stream, ILogger logger)
        {
            byte[] bytes = new byte[8192];
            var readCount = stream.Read(bytes, 0, bytes.Length);
            string requestData = Encoding.ASCII.GetString(bytes, 0, readCount);
            var parser = new RequestParser(logger);
            var request = parser.Parse(requestData);
            return request;
        }
    }
}
