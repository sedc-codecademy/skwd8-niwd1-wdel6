using ServerEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SedcServer
{
    class HiProcessor : IProcessor
    {
        public bool CanProcess(Request request)
        {
            return (request.Uri.Paths.Length > 1) && (request.Uri.Paths[0] == "hi");
        }

        public ResponseBase Process(Request request)
        {
            var name = request.Uri.Paths[1];
            var body = new StringBuilder(@$"
<html>
    <head>
        <title>SEDC Server Response</title>
    </head>
    <body>
        <h1>HI {name}</h1>
    </body>
</html>");

            return new Response
            {
                Headers = new HeaderCollection(),
                Version = request.Version,
                Status = StatusCode.OK,
                Body = body.ToString()
            };
        }
    }
}
