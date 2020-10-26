using ServerEntities;

using System;
using System.Collections.Generic;
using System.Text;

namespace SedcServer.Engine
{
    public class ServerEngine
    {
        public static Response Process(Request request)
        {
            Console.WriteLine(request);
            var message = "Hello Server World";
            return new Response
            {
                // Doesn't make sense, but nice first approximation
                Headers = request.Headers,
                Version = request.Version,
                Message = message,
                Status = 200,
                Body = "<h1>HELLO FROM SEDC SERVER</h1>"
            };
        }
    }
}
