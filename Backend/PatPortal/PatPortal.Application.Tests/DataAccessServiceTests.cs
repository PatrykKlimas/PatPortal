using NUnit.Framework;
using PatPortal.Application.Services;
using NSubstitute;
using PatPortal.Domain.Enums;

namespace PatPortal.Application.Tests
{
    public class DataAccessServiceTests
    {
        private DataAccessService _dataAccessService;
        [SetUp]
        public void Setup()
        {
            _dataAccessService = new DataAccessService();
        }

        [Test]
        [TestCase("Friends")]
        [TestCase("frieNds")]
        [TestCase("Public")]
        [TestCase("public")]
        [TestCase("private")]
        [TestCase("Private")]
        public void ProperStringValueDoesNotReturnUndefinded(string dataAccess)
        {
            //Act
            var result = _dataAccessService.ParseFromString(dataAccess);

            //Assert
            Assert.AreNotEqual(result, DataAccess.Undefined);
        }

        [Test]
        [TestCase("Firends")]
        [TestCase("fieNds")]
        [TestCase("forbidden")]
        [TestCase("pulbic")]
        [TestCase("priate")]
        [TestCase("Prviate")]
        public void InncorectStringValueReturnsUndefinded(string dataAccess)
        {
            //Act
            var result = _dataAccessService.ParseFromString(dataAccess);

            //Assert
            Assert.AreEqual(result, DataAccess.Undefined);
        }
    }
}