using ServerCore.Responses;

using ServerEntities;

using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore.Engine
{
    public class CalculationProcessor : IProcessor
    {
        public bool CanProcess(Request request)
        {
            return (request.Uri.Paths.Length >= 1) && request.Uri.Paths[0] == "calculation";
        }

        public ResponseBase Process(Request request)
        {
            var op = request.Uri.Paths[1];
            var first = int.Parse(request.Uri.Paths[2]);
            var second = int.Parse(request.Uri.Paths[3]);

            if (op == "add")
            {
                var headers = new HeaderCollection();
                headers.SetHeader("Content-Type", "text/plain");
                var response = new Response
                {
                    Body = (first + second).ToString(),
                    Headers = headers,
                    Status = StatusCode.OK,
                };
                return response;
            }
            else if (op == "sub")
            {
                var headers = new HeaderCollection();
                headers.SetHeader("Content-Type", "text/plain");
                var response = new Response
                {
                    Body = (first - second).ToString(),
                    Headers = headers,
                    Status = StatusCode.OK,
                };
                return response;
            }

            return ResponseGenerator.MakeNotFoundResponse("Invalid operation");
        }
    }
}
