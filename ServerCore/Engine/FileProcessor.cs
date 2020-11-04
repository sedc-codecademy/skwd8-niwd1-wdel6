using ServerEntities;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServerCore.Engine
{
    class FileProcessor : IProcessor
    {
        public Response Process(Request request)
        {
            var filename = request.Uri.Paths[0];
            
            var content = File.ReadAllBytes(filename);

            var headers = new HeaderCollection();
            headers.SetHeader("Content-Type", "text/plain");

            return new Response
            {
                Headers = headers,
                Version = request.Version,
                Status = StatusCode.OK,
                BinaryBody = content
            };
        }
    }
}
