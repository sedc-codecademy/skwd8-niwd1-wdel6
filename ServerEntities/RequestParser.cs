using ServerEntities.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ServerEntities
{
    public class RequestParser
    {
        public ILogger Logger { get; set; }

        public RequestParser(ILogger logger)
        {
            Logger = logger;
        }

        public Request Parse(string requestData)
        {
            Logger.Debug(requestData);

            var lines = requestData.Split(Environment.NewLine);

            var requestLine = lines[0];
            var requestRegex = new Regex(@"^([A-Z]+) (.+) HTTP\/(.*)$");
            var match = requestRegex.Match(requestLine);
            var method = match.Groups[1].Value;

            var uri = match.Groups[2].Value;

            var version = match.Groups[3].Value;

            var headerLines = lines.Skip(1).TakeWhile(line => !string.IsNullOrEmpty(line));

            var headerRegex = new Regex(@"^(.*): (.*)$");
            var headers = headerLines.Select(line => {
                var match = headerRegex.Match(line);
                return new Header(match.Groups[1].Value, match.Groups[2].Value);
            });

            var body = string.Join(Environment.NewLine, lines.Skip(1).Skip(headerLines.Count()).Skip(1));

            return new Request(method, uri, version, headers, body);

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

//WHATEVS /one/two?three=4 HTTP/1.1
//User-Agent: PostmanRuntime / 7.26.7
//Accept: */*
//Cache-Control: no-cache
//Postman-Token: 43e9edea-6f92-413f-a9d9-f4babcde5230
//Host: localhost:668
//Accept-Encoding: gzip, deflate, br
//Connection: keep-alive
//Content-Length: 0

        }
    }
}


// DEBUG
// INFO
// WARNING
// ERROR
// FATAL