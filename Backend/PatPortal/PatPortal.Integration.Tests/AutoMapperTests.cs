using AutoMapper;
using NUnit.Framework;
using PatPortal.Application.Mappers;
using PatPortal.Infrastructure.Mappers;

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
                cfg.AddProfile<InfrastructureMapperProfile>();
            }).CreateMapper();

            //Act & Assert
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}