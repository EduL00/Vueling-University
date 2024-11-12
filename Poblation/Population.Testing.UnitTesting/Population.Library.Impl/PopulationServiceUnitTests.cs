
using Microsoft.EntityFrameworkCore;
using Moq;
using Population.Infraestructure.Contracts;
using Population.Infraestructure.Contracts.Entities;
using Population.Infraestructure.Impl.DbContexts;
using Population.Library.Contracts.DTOs;
using Population.Library.Impl;
using System;

namespace Population.Testing.UnitTesting.Population.Library.Impl
{
    public class PopulationServiceUnitTests
    {
        private DbContextOptions<PopulationDBContext> _dbContextOptions;

        public PopulationServiceUnitTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<PopulationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        }

        [Fact]
        public void List_WhenValidInputAndNoDBError_ReturnNoError()
        {
            using (var context = new PopulationDBContext(_dbContextOptions))
            {
                // Arrange
                Mock<ICountryRepository> _mockCountryRepository = new();
                CountryInfoEntity info = new CountryInfoEntity()
                {
                    Year = 1980,
                    Population = 1000
                };
                List<CountryInfoEntity> list = new List<CountryInfoEntity>();
                list.Add(info);
                context.Countries.Add (new CountryEntity { Id = 1, Name = "A", CountryInfos = list});

                PopulationService sut = new(_mockCountryRepository.Object);
                ListPopulationReqDto input = new ListPopulationReqDto()
                {
                    firstCharacter = "A",
                    year = 1980
                };

                // Act
                ListPopulationResDto result = sut.ListPopulation(input);

                // Assert
                Assert.Null(result.Error);
                Assert.False(result.HasError);
            }

        }
    }
}
