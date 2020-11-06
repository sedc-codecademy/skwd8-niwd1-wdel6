using ServerEntities;
using System.IO;


namespace ServerCore.Engine
{
    class FileProcessor : IProcessor
    {
        public ResponseBase Process(Request request)
        {
            var filename = request.Uri.Paths[0];

            var content = File.ReadAllBytes("not-found.txt");
            if (File.Exists(filename))
            {
                content = File.ReadAllBytes(filename);
            }

            var headers = new HeaderCollection();
            headers.SetHeader("Content-Type", "html");

            return new BinaryResponse
            {
                Headers = headers,
                Version = request.Version,
                Status = StatusCode.OK,
                Body = content
            };
        }
    }
}
