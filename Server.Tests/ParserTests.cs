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
Cache-Control: no-cache
Postman-Token: 87b90cfe-9637-4ba8-bf67-54e0ad7538ee
Host: localhost:668
Accept-Encoding: gzip, deflate, br
Connection: keep-alive
Content-Length: 0";

            // 2. Act
            var actual = parser.Parse(requestString);
            var actualAcceptHeaderTest = actual.Headers.HasHeader("Accept");

            // 3. Assert
            Assert.AreEqual(Method.Unknown, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
            Assert.AreEqual(false, actualAcceptHeaderTest);
            Assert.AreEqual(string.Empty, actual.Body);
            Assert.AreEqual(7, actual.Headers.Count);
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
            var actualAcceptHeader = actual.Headers.GetHeader("Accept");

            // 3. Assert
            Assert.AreEqual(Method.Patch, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
            Assert.AreEqual(8, actual.Headers.Count);
            Assert.AreEqual("*/*", actualAcceptHeader.Value);
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
        public void Get_Method_Should_Return_Get_Method_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var requestString = @"GET /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actual = parser.Parse(requestString);

            // 3. Assert
            Assert.AreEqual(Method.Get, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
        }

        [TestMethod]
        public void Put_Method_Should_Return_Put_Method_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var requestString = @"PUT /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actual = parser.Parse(requestString);

            // 3. Assert
            Assert.AreEqual(Method.Put, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
        }

        [TestMethod]
        public void Options_Method_Should_Return_Options_Method_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var requestString = @"OPTIONS /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actual = parser.Parse(requestString);

            // 3. Assert
            Assert.AreEqual(Method.Options, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
        }

        [TestMethod]
        public void Delete_Method_Should_Return_Delete_Method_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var requestString = @"DELETE /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actual = parser.Parse(requestString);

            // 3. Assert
            Assert.AreEqual(Method.Delete, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
        }

        [TestMethod]
        public void Post_Method_Should_Return_Post_Method_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var requestString = @"POST /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actual = parser.Parse(requestString);

            // 3. Assert
            Assert.AreEqual(Method.Post, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
        }

        [TestMethod]
        public void Head_Method_Should_Return_Head_Method_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var requestString = @"HEAD /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actual = parser.Parse(requestString);

            // 3. Assert
            Assert.AreEqual(Method.Head, actual.Method);
            Assert.AreEqual("/one/two?three=4", actual.Uri);
            Assert.AreEqual("1.1", actual.Version);
        }


        [TestMethod]
        public void General_Header_Should_Return_General_Header_Values()
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

            var actualUserAgentHeader = actual.Headers.GetHeader("User-Agent");
            var actualAcceptHeader = actual.Headers.GetHeader("Accept");
            var actualCacheControlHeader = actual.Headers.GetHeader("Cache-Control");
            var actualPostmanTokenHeader = actual.Headers.GetHeader("Postman-Token");
            var actualHostHeader = actual.Headers.GetHeader("Host");
            var actualAcceptEncodingHeader = actual.Headers.GetHeader("Accept-Encoding");
            var actualConnectionHeader = actual.Headers.GetHeader("Connection");

            // 3. Assert
            Assert.AreEqual("PostmanRuntime/7.26.7", actualUserAgentHeader.Value);
            Assert.AreEqual("*/*", actualAcceptHeader.Value);
            Assert.AreEqual("no-cache", actualCacheControlHeader.Value);
            Assert.AreEqual("87b90cfe-9637-4ba8-bf67-54e0ad7538ee", actualPostmanTokenHeader.Value);
            Assert.AreEqual("localhost:668", actualHostHeader.Value);
            Assert.AreEqual("gzip, deflate, br", actualAcceptEncodingHeader.Value);
            Assert.AreEqual("keep-alive", actualConnectionHeader.Value);

        }

        [TestMethod]
        public void Entity_Header_Should_Return_General_Entity_Values()
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

            var actualContentLengthHeader = actual.Headers.GetHeader("Content-Length");

            // 3. Assert
            Assert.AreEqual("0", actualContentLengthHeader.Value);
        }
    }
}
