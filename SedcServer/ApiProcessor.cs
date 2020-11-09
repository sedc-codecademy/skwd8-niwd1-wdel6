using SedcJsonSerializer;

using ServerEntities;

using System;
using System.Collections.Generic;
using System.Text;

namespace SedcServer
{
    public class ApiProcessor : IProcessor
    {
        public bool CanProcess(Request request)
        {
            return (request.Uri.Paths.Length >= 1) && (request.Uri.Paths[0] == "api");
        }

        public ResponseBase Process(Request request)
        {
            var controller = request.Uri.Paths[1];
            var body = Serializer.Serialize(controller);
            return new Response
            {
                Body = body,
                Headers = new HeaderCollection
                {
                    { "Content-Type", "application/json" }
                },
                Status = StatusCode.OK
            };
        }
    }
}
