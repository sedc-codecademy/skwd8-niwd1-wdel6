using ServerEntities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SedcServer
{
    public class ResponseSender
    {
        static public void SendResponse(NetworkStream stream, Response response)
        {
            var statusLine = $"HTTP/{response.Version} {response.Status} {response.Message}";
            var headersLines = GetHeaderLines(response.Headers);

            string content = statusLine + Environment.NewLine + headersLines + Environment.NewLine;
            if (!string.IsNullOrEmpty(response.Body))
            {
                content += Environment.NewLine + response.Body;
            }

            Console.WriteLine(content);
            var contentBytes = Encoding.ASCII.GetBytes(content);
            stream.Write(contentBytes);
            stream.Close();
        }

        private static string GetHeaderLines(HeaderCollection headers)
        {
            return string.Join(Environment.NewLine, headers.GetAvailableHeaders().Select(header => $"{header}: {headers.GetHeader(header).Value}"));
        }
    }
}
