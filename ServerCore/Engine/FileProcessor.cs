using ServerCore.Responses;

using ServerEntities;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServerCore.Engine
{
    public class FileProcessor : IProcessor
    {
        public string Root { get; private set; }
        public FileProcessor(string root = @"C:\Source\SEDC\skwd8-niwd1-wdel6\SedcServer\site")
        {
            Root = root;
        }

        private Dictionary<string, string> mimeTypes = new Dictionary<string, string>
        {
            { ".txt", "text/plain" },
            { ".jpg", "image/jpeg" },
            { ".html", "text/html" }
        };

        protected virtual string GetFileName(Request request)
        {
            return request.Uri.Paths[0];
        }

        public ResponseBase Process(Request request)
        {
            var filename = GetFileName(request);

            var filePath = Path.Combine(Root, filename);

            if (!File.Exists(filePath)) {
                return ResponseGenerator.MakeNotFoundResponse(filePath);
            }
            var content = File.ReadAllBytes(filePath);

            var extension = Path.GetExtension(filename);
            var mimeType = mimeTypes.ContainsKey(extension) ? mimeTypes[extension] : "text/plain";

            var headers = new HeaderCollection();
            headers.SetHeader("Content-Type", mimeType);

            return new BinaryResponse
            {
                Headers = headers,
                Version = request.Version,
                Status = StatusCode.OK,
                Body = content
            };
        }

        public virtual bool CanProcess(Request request)
        {
            return (request.Uri.Paths.Length == 1) && request.Uri.Paths[0].IsFileName();
        }
    }


}
