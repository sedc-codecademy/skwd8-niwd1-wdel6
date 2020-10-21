using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace ServerEntities
{
    public class Request
    {
        public Request(string method, string uri, string version, IEnumerable<Header> headers, string body)
        {
            Method = MethodResolver.FromString(method);
            Uri = uri;
            Version = version;

            Headers = new Dictionary<string, Header>();
            foreach (var header in headers)
            {
                Headers.Add(header.Name, header);
            }

            Body = body;
        }

        public Method Method { get; private set; }

        public string Uri { get; private set; }

        public string Version { get; private set; }

        public Dictionary<string, Header> Headers { get; private set; }

        public string Body { get; private set; }

        public string GetHeader(string name)
        {
            if (Headers.ContainsKey(name))
            {
                return Headers[name].Value;
            }
            return string.Empty;
        }

        //GET / HTTP/1.1
        //Host: localhost:668
        //Connection: keep-alive
        //Cache-Control: max-age=0
        //Upgrade-Insecure-Requests: 1
        //User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36
        //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9
        //Sec-Fetch-Site: none
        //Sec-Fetch-Mode: navigate
        //Sec-Fetch-User: ?1
        //Sec-Fetch-Dest: document
        //Accept-Encoding: gzip, deflate, br
        //Accept-Language: en-US,en;q=0.9,mk;q=0.8,sr;q=0.7,hr;q=0.6,bs;q=0.5,bg;q=0.4
        //Cookie: language=en; u_token=JWT%20eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjoiNWY0M2Q4NjAzZDlmN2Y1YThjNzQyNTI2IiwiaWF0IjoxNTk4MjgzNjQxfQ.O9dSGoRl3h08YozGhRfN-lpOHKiSxO8q0Ts_fIBrudU; _ga=GA1.1.2037389660.1601149779         

        public override string ToString()
        {
            return $"{Method} - {Uri} - HTTP {Version}{Environment.NewLine}{Body}";
        }
    }
}
