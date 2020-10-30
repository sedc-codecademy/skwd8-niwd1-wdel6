using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerCore.Engine;
using ServerEntities;
using ServerEntities.Logging;


namespace Server.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Whatevs_Method_Should_Return_Unknown_Method_Test()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser(new ConsoleLogger());
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
            RequestParser parser = new RequestParser(new ConsoleLogger());
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
            RequestParser parser = new RequestParser(new ConsoleLogger());
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
        public void Request_With_Uri_Should_Return_Correct_Uri()
        {
            // 1. Arrange
            var uri = @"/paths/4/paths/2/path?key=value&key2=value2&key3=value3";

            // 2. Act
            var uriParts = UriParser.Parse(uri);

            // 3. Assert
            string actualUri = uriParts.Uri;
            Assert.AreEqual(uri, actualUri);
        }

        [TestMethod]
        public void Request_Without_Query_Should_Return_Correct_Uri()
        {
            // 1. Arrange
            var uri = "/paths/4/paths/2/path";

            // 2. Act
            var uriParts = UriParser.Parse(uri);

            // 3. Assert
            string actualUri = uriParts.Uri;
            Assert.AreEqual(uri, actualUri);
        }

        [TestMethod]
        [DataRow("path/.#$$.../path")]
        [DataRow("path/path?key=")]
        [DataRow("Some custom invalid uri.")]
        [DataRow("path%20%20/path")]
        [DataRow("path?key=value&key")]
        [DataRow("")]
        public void Request_With_Invalid_Uri_Should_Throw_Argument_Exception(string invalidUri)
        {
            // 2. Act
            System.Action parseUri = () => UriParser.Parse(invalidUri);

            // 3. Assert            
            Assert.ThrowsException<System.ArgumentException>(parseUri, "uri");
        }
    }
}
