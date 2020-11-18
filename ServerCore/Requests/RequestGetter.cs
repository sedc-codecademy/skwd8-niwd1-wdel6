using ServerEntities;
using ServerEntities.Logging;

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore.Requests
{
    public class RequestGetter: IDisposable
    {
        public TcpClient Client { get; private set; }
        public ILogger Logger { get; private set; }

        public RequestGetter(TcpClient client, ILogger logger)
        {
            Client = client;
            Logger = logger;
        }
        public Request GetRequest()
        {
            var stream = Client.GetStream();
            while (!stream.DataAvailable);

            byte[] bytes = new byte[Client.Available];
            var readCount = stream.Read(bytes, 0, Client.Available);
            string requestData = Encoding.ASCII.GetString(bytes, 0, readCount);
            var parser = new RequestParser(Logger);
            var request = parser.Parse(requestData);
            Logger.Info($"Received {request.Method} request on path {request.Uri.Uri}");
            return request;
        }

        public void Dispose()
        {
            // intentionally does not close the stream
        }
    }
}
