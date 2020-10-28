using System;
using System.Collections.Generic;
using System.Text;

namespace ServerEntities
{
    public class UriParts
    {
        public string[] Paths { get; set; }
        public Dictionary<string, string> Query { get; set; }
    }
}
