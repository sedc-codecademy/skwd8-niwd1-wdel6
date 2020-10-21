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
            return new Response();
        }
    }
}
