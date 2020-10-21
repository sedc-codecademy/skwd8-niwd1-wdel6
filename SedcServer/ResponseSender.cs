using ServerEntities;

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SedcServer
{
    public class ResponseSender
    {
        static public void SendResponse(NetworkStream stream, Response response)
        {
            var content = response.ToString();
            var contentBytes = Encoding.ASCII.GetBytes(content);
            stream.Write(contentBytes);
            stream.Close();
        }
    }
}
