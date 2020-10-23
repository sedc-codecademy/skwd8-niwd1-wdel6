using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerEntities;


namespace Server.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Whatevs_Method_Should_Return_Unknown_Method_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var requestString = @"WHATEVS /one/two?three=4 HTTP/1.1
User-Agent: PostmanRuntime/7.26.7
Accept: */*
Cache-Control: no-cache
Postman-Token: 87b90cfe-9637-4ba8-bf67-54e0ad7538ee
Host: localhost:668
Accept-Encoding: gzip, deflate, br
Connection: keep-alive
Content-Length: 0";

            // 2. Act
            var actual = parser.Parse(requestString);
            var actualAcceptHeader = actual.GetHeader("Accept");

            // 3. Assert
            Assert.AreEqual(Method.Unknown, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
            Assert.AreEqual(8, actual.Headers.Count);
            Assert.AreEqual("*/*", actualAcceptHeader);
            Assert.AreEqual(string.Empty, actual.Body);
        }

        [TestMethod]
        public void Patch_Method_Should_Return_Patch_Method_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var requestString = @"PATCH /one/two?three=4 HTTP/1.1
User-Agent: PostmanRuntime/7.26.7
Accept: */*
Cache-Control: no-cache
Postman-Token: 87b90cfe-9637-4ba8-bf67-54e0ad7538ee
Host: localhost:668
Accept-Encoding: gzip, deflate, br
Connection: keep-alive
Content-Length: 0";

            // 2. Act
            var actual = parser.Parse(requestString);
            var actualAcceptHeader = actual.GetHeader("Accept");

            // 3. Assert
            Assert.AreEqual(Method.Patch, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
            Assert.AreEqual(8, actual.Headers.Count);
            Assert.AreEqual("*/*", actualAcceptHeader);
            Assert.AreEqual(string.Empty, actual.Body);
        }

        [TestMethod]
        public void Request_With_Body_Should_Return_Correct_Body()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var requestString = @"PATCH /one/two?three=4 HTTP/1.1
User-Agent: PostmanRuntime/7.26.7
Accept: */*
Cache-Control: no-cache
Postman-Token: 87b90cfe-9637-4ba8-bf67-54e0ad7538ee
Host: localhost:668
Accept-Encoding: gzip, deflate, br
Connection: keep-alive
Content-Length: 0

THIS IS THE BODY";

            // 2. Act
            var actual = parser.Parse(requestString);

            // 3. Assert
            Assert.AreEqual(Method.Patch, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
            Assert.AreEqual("THIS IS THE BODY", actual.Body);
        }


        [TestMethod]
        public void Requests_With_Specific_Method_Should_Return_Correct_Method_Value_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var GETrequestString = @"GET /one/two?three=4 HTTP/1.1";
            var POSTrequestString = @"POST /one/two?three=4 HTTP/1.1";
            var PUTrequestString = @"PUT /one/two?three=4 HTTP/1.1";
            var DELETErequestString = @"DELETE /one/two?three=4 HTTP/1.1";
            var OPTIONSrequestString = @"OPTIONS /one/two?three=4 HTTP/1.1";
            var HEADrequestString = @"HEAD /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actualGet = parser.Parse(GETrequestString);
            var actualPost = parser.Parse(POSTrequestString);
            var actualPut = parser.Parse(PUTrequestString);
            var actualDelete = parser.Parse(DELETErequestString);
            var actualOptions = parser.Parse(OPTIONSrequestString);
            var actualHead = parser.Parse(HEADrequestString);

            // 3. Assert
            Assert.AreEqual(Method.Get, actualGet.Method);
            Assert.AreEqual(Method.Post, actualPost.Method);
            Assert.AreEqual(Method.Put, actualPut.Method);
            Assert.AreEqual(Method.Delete, actualDelete.Method);
            Assert.AreEqual(Method.Options, actualOptions.Method);
            Assert.AreEqual(Method.Head, actualHead.Method);
        }

        [TestMethod]
        public void Request_With_Specific_Header_Should_Return_Correct_Header_Value_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var GETrequestString = @"GET /one/two?three=4 HTTP/1.1
User-Agent: PostmanRuntime/7.26.7
Accept: */*
Cache-Control: no-cache
Postman-Token: 87b90cfe-9637-4ba8-bf67-54e0ad7538ee
Host: localhost:668
Accept-Encoding: gzip, deflate, br
Connection: keep-alive
Content-Length: 0";

            // 2. Act
            var actualGetRequest = parser.Parse(GETrequestString);
            var actualUserAgent = actualGetRequest.GetHeader("User-Agent");
            var actualCacheControl = actualGetRequest.GetHeader("Cache-Control");
            var actualPostmanToken = actualGetRequest.GetHeader("Postman-Token");
            var actualHost = actualGetRequest.GetHeader("Host");
            var actualAcceptEncoding = actualGetRequest.GetHeader("Accept-Encoding");
            var actualConnection = actualGetRequest.GetHeader("Connection");
            var actualContentLength = actualGetRequest.GetHeader("Content-Length");

            // 3. Assert
            Assert.AreEqual("PostmanRuntime/7.26.7", actualUserAgent);
            Assert.AreEqual("no-cache", actualCacheControl);
            Assert.AreEqual("87b90cfe-9637-4ba8-bf67-54e0ad7538ee", actualPostmanToken);
            Assert.AreEqual("localhost:668", actualHost);
            Assert.AreEqual("gzip, deflate, br", actualAcceptEncoding);
            Assert.AreEqual("keep-alive", actualConnection);
            Assert.AreEqual("0", actualContentLength);
        }
    }
}