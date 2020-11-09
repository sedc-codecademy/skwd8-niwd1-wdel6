using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SedcJsonSerializer.Tests
{
    [TestClass]
    public class SerializerBasicTests
    {
        [TestMethod]
        public void Serialize_True_Boolean()
        {
            // 1. Arrange
            var input = true;
            var expected = "true";
            // 2. Act
            var actual = Serializer.Serialize(input);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_False_Boolean()
        {
            // 1. Arrange
            var input = false;
            var expected = "false";
            // 2. Act
            var actual = Serializer.Serialize(input);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_String()
        {
            // 1. Arrange
            var input = "string";
            var expected = "\"string\"";
            // 2. Act
            var actual = Serializer.Serialize(input);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_Integer_Number()
        {
            // 1. Arrange
            var input = 3;
            var expected = "3";
            // 2. Act
            var actual = Serializer.Serialize(input);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_BIG_Number()
        {
            // 1. Arrange
            var input = 30000000000;
            var expected = "30000000000";
            // 2. Act
            var actual = Serializer.Serialize(input);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
