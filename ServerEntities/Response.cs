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

        public override byte[] AppendBody(byte[] content)
        {
            return content.Concat(Encoding.ASCII.GetBytes(Body)).ToArray();
        }
    }

    public class BinaryResponse : ResponseBase<byte[]>
    {
        public override byte[] AppendBody(byte[] content)
        {
            return content.Concat(Body).ToArray();
        }
    }

    public class BodylessResponse : ResponseBase
    {
        public override byte[] AppendBody(byte[] content)
        {
            return content;
        }
    }
}
