using ServerEntities;

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SedcServer
{
    public class RequestGetter
    {
        static public Request GetRequest(NetworkStream stream)
        {
            byte[] bytes = new byte[8192];
            var readCount = stream.Read(bytes, 0, bytes.Length);
            string requestData = Encoding.ASCII.GetString(bytes, 0, readCount);
            var parser = new RequestParser();
            var request = parser.Parse(requestData);
            return request;
        }
    }
}
