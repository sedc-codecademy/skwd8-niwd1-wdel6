using ServerEntities;
using System;
using System.IO;
using System.Text;


namespace ServerCore.Responses
{
    public class ResponseGenerator
    {
        public static Response MakeRequestErrorResponse(Exception ex, bool debugMode)
        {
            var notFoundBytes = File.ReadAllBytes("not-found.txt");
            string notFound = Encoding.UTF8.GetString(notFoundBytes);

            var response = new Response();
            response.Status = StatusCode.BadRequest;
            response.Body = debugMode ? ex.ToString() : notFound;

            var headers = new HeaderCollection();
            headers.SetHeader("Content-Type", "html");
            response.Headers = headers;

            return response;
        }
    }
}
