using NUnit.Framework;
using PatPortal.Domain.Enums;
using PatPortal.SharedKernel.Extensions;
using System;

namespace PatPortal.SharedKernel.Tests
{
    public class StringExtensionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("david", "David")]
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("d", "D")]
        [TestCase("DAVID", "David")]
        [TestCase("dAVID", "David")]
        public void FirstToUpperTest(string toConvert, string expectedResult)
        {
            //Act
            var result = toConvert.FirstToUpper();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("public", DataAccess.Public)]
        [TestCase("friEnds", DataAccess.Friends)]
        [TestCase("PRIVATE", DataAccess.Private)]
        [TestCase("Public", DataAccess.Public)]
        [TestCase("Friends", DataAccess.Friends)]
        public void EnumParseOrThrowWithCorrectStringsReturnCorrectEnum(string value, DataAccess expectedResult)
        {
            //Act
            var result = value.ParseToEnumOrThrow<DataAccess, Exception>();

            //Assert
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        [TestCase("publi")]
        [TestCase("fiEnds")]
        [TestCase("PRVATE")]
        [TestCase("Pubic")]
        [TestCase("xxx")]
        public void EnumParseOrThrowWithIncorrectStringsThrowError(string value)
        {
            //Assert
            var ex = Assert.Throws<InvalidOperationException>((() => value.ParseToEnumOrThrow<DataAccess, InvalidOperationException>()));
            Assert.IsTrue(ex.Message.Contains("Unable to parse"));
        }

    }
}