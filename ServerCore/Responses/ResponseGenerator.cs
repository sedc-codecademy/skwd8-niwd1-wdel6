using ServerEntities;

using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore.Responses
{
    public class ResponseGenerator
    {
        public static Response MakeRequestErrorResponse(Exception ex, bool debugMode = false)
        {
            var response = new Response();
            response.Status = StatusCode.BadRequest;
            response.Headers = new HeaderCollection();
            response.Headers.SetHeader("Content-Type", "text/plain");
            response.Body = debugMode ? ex.ToString() : "Error Occured";
            return response;
        }

        public static Response MakeNotFoundResponse(string path, bool debugMode = false)
        {
            var response = new Response();
            response.Status = StatusCode.NotFound;
            response.Headers = new HeaderCollection();
            response.Headers.SetHeader("Content-Type", "text/plain");
            response.Body = debugMode ? path : "Not Found";
            return response;
        }

    }
}
