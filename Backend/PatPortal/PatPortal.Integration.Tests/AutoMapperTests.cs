using AutoMapper;
using NUnit.Framework;
using PatPortal.Application.Mappers;

namespace PatPortal.Integration.Tests
{
    public class AutoMapperTests
    {
        //TODO - verify why date of birth does not works while when application is running it looks correctly
        [Test]
        public void IConfigurationValidShouldPass()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapperProfile>();
            }).CreateMapper();

            //Act & Assert
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}