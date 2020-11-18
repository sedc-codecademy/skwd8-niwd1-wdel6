using ServerCore.Responses;

using ServerEntities;

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ServerCore.Engine
{
    class WebSocketProcessor : IProcessor
    {
        const string WebSocketGuid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        public bool CanProcess(Request request)
        {
            return (request.Uri.Paths.Length == 1) && (request.Uri.Paths[0] == "ws");
        }

        public ResponseBase Process(Request request)
        {
            if (request.Headers.HasHeader("Sec-WebSocket-Key"))
            {
                var webSocketKey = request.Headers.GetHeader("Sec-WebSocket-Key").Value;
                
                var magicString = Encoding.UTF8.GetBytes(webSocketKey + WebSocketGuid);
                var shaBytes = SHA1.Create().ComputeHash(magicString);
                var serverResponse = Convert.ToBase64String(shaBytes);

                Console.WriteLine(webSocketKey);
                Console.WriteLine(serverResponse);

                var headers = new HeaderCollection(new Dictionary<string, string>
                {
                    { "Connection", "Upgrade" },
                    { "Upgrade", "websocket" },
                    { "Sec-WebSocket-Accept",  serverResponse}
                });

                return new BodylessResponse
                {
                    Headers = headers,
                    Status = StatusCode.SwitchingProtocols
                };
            }
            
            Console.WriteLine("NO HEADER FOR YOU");
            return ResponseGenerator.MakeNotFoundResponse("");
        }
    }
}
