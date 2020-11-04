using Microsoft.VisualStudio.TestTools.UnitTesting;

using ServerCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Tests
{
    [TestClass]
    public class IsFilenameTests
    {
        [TestMethod]
        public void Valid_FileName_Should_Return_True()
        {
            // 1. Arrange
            var input = "file.name";
            var expected = true;

            // 2. Act
            var actual = input.IsFileName();

            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FileName_Without_Dot_Should_Return_False()
        {
            // 1. Arrange
            var input = "no-dot-name";
            var expected = false;

            // 2. Act
            var actual = input.IsFileName();

            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FileName_With_Dot_Beginning_Should_Return_False()
        {
            // 1. Arrange
            var input = ".notfile";
            var expected = false;

            // 2. Act
            var actual = input.IsFileName();

            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FileName_With_Dot_End_Should_Return_False()
        {
            // 1. Arrange
            var input = "notfile.";
            var expected = false;

            // 2. Act
            var actual = input.IsFileName();

            // 3. Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
