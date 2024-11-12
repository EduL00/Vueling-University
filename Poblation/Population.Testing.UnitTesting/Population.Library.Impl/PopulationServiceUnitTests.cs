
using Moq;
using Population.Infraestructure.Contracts;
using Population.Infraestructure.Contracts.Entities;
using Population.Library.Contracts.DTOs;
using Population.Library.Impl;
using Population.XCutting.Enums;

namespace Population.Testing.UnitTesting.Population.Library.Impl
{
    public class PopulationServiceUnitTests
    {
        #region List_WhenValidInputAndNoDBError_ReturnNoError
        [Fact]
        public void List_WhenValidInputAndNoDBError_ReturnNoError()
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
            CountryEntity country = new CountryEntity();
            country.Name = "A";
            country.CountryInfos = list;
            List<CountryEntity> countryEntityList = new List<CountryEntity>();
            countryEntityList.Add(country);

            _mockCountryRepository
                .Setup(x => x.GetCountries())
                .Returns(countryEntityList);


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
            Assert.Equal(1, result.result.Count);

        }
        #endregion

        #region ListWhenInvalidCharacter_InvalidChar
        [Fact]
        public void ListWhenInvalidCharacter_ReturnError()
        {
            // Arrange
            Mock<ICountryRepository> _mockCountryRepository = new();

            PopulationService sut = new(_mockCountryRepository.Object);
            ListPopulationReqDto input = new ListPopulationReqDto()
            {
                firstCharacter = "Aa",
                year = 1980
            };

            // Act
            ListPopulationResDto result = sut.ListPopulation(input);

            // Assert
            Assert.True(result.HasError);
            Assert.Equal(ListPopulationReEnumError.InvalidChar, result.Error);
        }
        #endregion

        #region ListWhenInvalidYear_YearNotInBounds
        [Fact]
        public void ListWhenInvalidYear_YearNotInBounds()
        {
            // Arrange
            Mock<ICountryRepository> _mockCountryRepository = new();

            PopulationService sut = new(_mockCountryRepository.Object);
            ListPopulationReqDto input = new ListPopulationReqDto()
            {
                firstCharacter = "A",
                year = 1900
            };

            // Act
            ListPopulationResDto result = sut.ListPopulation(input);

            // Assert
            Assert.True(result.HasError);
            Assert.Equal(ListPopulationReEnumError.YearNotInBounds, result.Error);
        }
        #endregion

        #region List_WhenValidInputButNoCountrySatisfies_ReturnNoCountriesWithCondition
        [Fact]
        public void List_WhenValidInputButNoCountrySatisfies_ReturnNoCountriesWithCondition()
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
            CountryEntity country = new CountryEntity();
            country.Name = "A";
            country.CountryInfos = list;
            List<CountryEntity> countryEntityList = new List<CountryEntity>();
            countryEntityList.Add(country);

            _mockCountryRepository
                .Setup(x => x.GetCountries())
                .Returns(countryEntityList);


            PopulationService sut = new(_mockCountryRepository.Object);
            ListPopulationReqDto input = new ListPopulationReqDto()
            {
                firstCharacter = "B",
                year = 1980
            };

            // Act
            ListPopulationResDto result = sut.ListPopulation(input);

            // Assert
            Assert.True(result.HasError);
            Assert.Equal(ListPopulationReEnumError.NoCountriesWithCondition, result.Error);

        }
        #endregion

        #region List_WhenValidInputButNoCountryInDB_ReturnNoCountriesInBD
        [Fact]
        public void List_WhenValidInputButNoCountryInDB_ReturnNoCountriesInBD()
        {
            // Arrange
            Mock<ICountryRepository> _mockCountryRepository = new();
            CountryInfoEntity info = new CountryInfoEntity()
            {
                Year = 1980,
                Population = 1000
            };

            List<CountryEntity> countryEntityList = new List<CountryEntity>();
            _mockCountryRepository
                .Setup(x => x.GetCountries())
                .Returns(countryEntityList);


            PopulationService sut = new(_mockCountryRepository.Object);
            ListPopulationReqDto input = new ListPopulationReqDto()
            {
                firstCharacter = "B",
                year = 1980
            };

            // Act
            ListPopulationResDto result = sut.ListPopulation(input);

            // Assert
            Assert.True(result.HasError);
            Assert.Equal(ListPopulationReEnumError.NoCountriesInBD, result.Error);

        }
        #endregion
    }
}
