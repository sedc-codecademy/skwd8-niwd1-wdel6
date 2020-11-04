using ServerEntities;

using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore.Engine
{
    public class ServerEngine
    {
        public static ResponseBase Process(Request request)
        {
            // do some check whether our request is for a file
            
            if ((request.Uri.Paths.Length == 1) && request.Uri.Paths[0].IsFileName())
            {
                var fileProcessor = new FileProcessor();
                var fileResponse = fileProcessor.Process(request);
                return fileResponse;
            }

            var processor = new EchoProcessor();
            var response = processor.Process(request);
            return response;
        }
    }
}
