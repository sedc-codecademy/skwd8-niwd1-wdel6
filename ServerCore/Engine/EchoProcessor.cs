using ServerEntities;
using System;
using System.Text;


namespace ServerCore.Engine
{
    class EchoProcessor : IProcessor
    {
        public ResponseBase Process(Request request)
        {
            var body = new StringBuilder(@$"
<html>
    <head>
        <title>SEDC Server Response</title>
    </head>
<body>
<h1>HELLO FROM SEDC SERVER {DateTime.Now.ToLongTimeString()}</h1>
<img src=""https://www.sedc.mk/wp-content/uploads/2016/05/logo.png"" />
<p>The type of the request is <strong>{request.Method.ToString().ToUpperInvariant()}</strong></p>
<p>The base URI of the request is {request.Uri.Uri}</p>
<p>The headers of the request are</p>
<ul>
");
            foreach (var header in request.Headers.GetAvailableHeaders())
            {
                var value = request.Headers.GetHeader(header).Value;
                body.AppendLine($"<li><strong>{header}:</strong> {value}</li>");
            }

            body.AppendLine("</ul>");
            if (string.IsNullOrEmpty(request.Body))
            {
                body.AppendLine("<p>There is no body </p>");
            } else
            {
                body.AppendLine("<p>The body is:</p>");
                body.AppendLine($"<pre>{request.Body}</pre>");
            }

            body.AppendLine("</body></html>");

            var headers = new HeaderCollection();
            headers.SetHeader("Content-Type", "html");

            return new Response
            {
                Headers = new HeaderCollection(),
                Version = request.Version,
                Status = StatusCode.OK,
                Body = body.ToString()
            };
        }
    }
}
