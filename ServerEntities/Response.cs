using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerEntities
{
    public class Response: ResponseBase<string>
    {
        public static Response EmptyResponse = new Response
        {
            Headers = new HeaderCollection(),
            Version = "1.1",
            Status = StatusCode.Invalid,
            Message = string.Empty
        };

        public override string AppendToBody(string content)
        {
            return content + Body;
        }
    }

    public class BinaryResponse : ResponseBase<byte[]>
    {
        public override byte[] AppendToBody(byte[] content)
        {
            return content.Concat(Body).ToArray();
        }
    }
}
