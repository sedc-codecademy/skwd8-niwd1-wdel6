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

        #region GET, POST, PUT, DELETE, OPTIONS, HEAD - Tests        
        [TestMethod]
        public void Get_Request_Should_Return_Correct_Method_Value_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var GETrequestString = @"GET /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actualGet = parser.Parse(GETrequestString);                        

            // 3. Assert
            Assert.AreEqual(Method.Get, actualGet.Method);                                                
        }

        [TestMethod]
        public void Post_Request_Should_Return_Correct_Method_Value_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var POSTrequestString = @"POST /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actualPost = parser.Parse(POSTrequestString);

            // 3. Assert
            Assert.AreEqual(Method.Post, actualPost.Method);
        }

        [TestMethod]
        public void Put_Request_Should_Return_Correct_Method_Value_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var PUTrequestString = @"PUT /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actualPut = parser.Parse(PUTrequestString);

            // 3. Assert
            Assert.AreEqual(Method.Put, actualPut.Method);
        }

        [TestMethod]
        public void Delete_Request_Should_Return_Correct_Method_Value_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var DELETErequestString = @"DELETE /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actualDelete = parser.Parse(DELETErequestString);

            // 3. Assert
            Assert.AreEqual(Method.Delete, actualDelete.Method);
        }

        [TestMethod]
        public void Options_Request_Should_Return_Correct_Method_Value_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var OPTIONSrequestString = @"OPTIONS /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actualOptions = parser.Parse(OPTIONSrequestString);

            // 3. Assert
            Assert.AreEqual(Method.Options, actualOptions.Method);
        }

        [TestMethod]
        public void Head_Request_Should_Return_Correct_Method_Value_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            var HEADrequestString = @"HEAD /one/two?three=4 HTTP/1.1";

            // 2. Act
            var actualHead = parser.Parse(HEADrequestString);

            // 3. Assert
            Assert.AreEqual(Method.Head, actualHead.Method);
        }
        #endregion

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