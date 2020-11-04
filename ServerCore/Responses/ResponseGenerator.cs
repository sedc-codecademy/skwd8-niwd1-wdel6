using ServerEntities;

using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore.Responses
{
    public class ResponseGenerator
    {
        public static Response MakeRequestErrorResponse(Exception ex, bool debugMode)
        {
            var response = new Response();
            response.Status = StatusCode.BadRequest;
            response.Body = debugMode ? ex.ToString() : "Error Occured";
            return response;
        }
    }
}
