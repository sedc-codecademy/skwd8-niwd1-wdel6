using System;
using System.Collections.Generic;
using System.Text;

namespace ServerEntities
{
    public class Response
    {
        public HeaderCollection Headers { get; set; }

        public string Version { get; set; }

        public string Body { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }
    }
}
