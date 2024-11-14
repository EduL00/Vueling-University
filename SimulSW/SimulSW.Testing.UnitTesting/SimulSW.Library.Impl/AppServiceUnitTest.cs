using Moq;
using SimulSW.Infraestructure.Contracts;
using SimulSW.Infraestructure.Contracts.APIEntities;
using SimulSW.Infraestructure.Contracts.DBEntities;
using SimulSW.Library.Contracts.DTOs;
using SimulSW.Library.Impl;
using SimulSW.XCuttin.Enums;

namespace SimulSW.Testing.UnitTesting.SimulSW.Library.Impl
{
    public class AppServiceUnitTest
    {
        #region WhenGetApiInfo_NoApiError_NoDBError_ReturnNoError
        [Fact]
        public void WhenGetApiInfo_NoApiError_NoDBError_ReturnListPlanetNames()
        {
            // Arrange
            Mock<ISWApiRepository> _mockSWApiRepository = new();
            Mock<IPlanetRepository> _mockPlanetRepository = new();
            ResGetApiInfoDTO result = new();
            List<string> OkResultPlanetNames = new();
            OkResultPlanetNames.Add("Tatooine");
            APIInfoFromJsonEntity apiDataTask = new()
            {
                PlanetsInfo = new()
            };
            PlanetInfoFromJsonEntity planetInfoFromJsonEntity = new()
            {
                Name = "Tatooine",
                Rot = "23",
                Perd = "304",
                Climate = "arid",
                Population = "200000",
                Url = "https://swapi.dev/api/planets/1/"
            };

            apiDataTask.PlanetsInfo.Add(planetInfoFromJsonEntity);

            _mockSWApiRepository
                 .Setup(x => x.GetApiInfo())
                 .ReturnsAsync(apiDataTask);

            AppService sut = new AppService(_mockSWApiRepository.Object, _mockPlanetRepository.Object);

            // Act
            result = sut.GetApiInfo();

            // Assert
            Assert.False(result.HasError);
            Assert.Null(result.Error);
            Assert.Equal(OkResultPlanetNames, result.PlanetNames);
        }
        #endregion

        #region WhenGetApiInfo_ApiError_ReturnErrorInApi
        [Fact]
        public void WhenGetApiInfo_ApiError_ReturnErrorInApi()
        {
            // Arrange
            Mock<ISWApiRepository> _mockSWApiRepository = new();
            Mock<IPlanetRepository> _mockPlanetRepository = new();
            ResGetApiInfoDTO result = new();

            _mockSWApiRepository
                 .Setup(x => x.GetApiInfo())
                 .ThrowsAsync(new Exception());

            AppService sut = new AppService(_mockSWApiRepository.Object, _mockPlanetRepository.Object);

            // Act
            result = sut.GetApiInfo();

            // Assert
            Assert.True(result.HasError);
            Assert.Equal(ResGetApiInfoErrorEnum.ErrorInApi, result.Error);

        }
        #endregion

        #region WhenGetPopulationInfo_PlanetFound_HasPopulation_ReturnNoError
        [Fact]
        public void WhenGetPopulationInfo_PlanetFound_HasPopulation_ReturnNoError()
        {
            // Arrange
            Mock<ISWApiRepository> _mockSWApiRepository = new();
            Mock<IPlanetRepository> _mockPlanetRepository = new();
            ResGetPopulationInfroDTO result = new();
            PlanetEntity? DBEnt = new PlanetEntity()
            {
                Population = 1,
                Urlinfo = ""
            };
            List<string> strings = new List<string>()
            {
                "planet"
            };

            _mockPlanetRepository
                 .Setup(x => x.GetPlanetInfo("planet"))
                 .Returns(DBEnt);
            _mockSWApiRepository
                .Setup(x => x.GetPopulationNames(DBEnt.Urlinfo))
                .ReturnsAsync(strings);

            AppService sut = new AppService(_mockSWApiRepository.Object, _mockPlanetRepository.Object);

            // Act
            result = sut.GetPopulationInfo("planet");

            // Assert
            Assert.False(result.HasError);
            Assert.Null(result.Error);
            Assert.Equal(strings, result.PopulationNames);
        }
        #endregion

        #region WhenGetPopulationInfo_PlanetNotFound_ReturnNotFound
        [Fact]
        public void WhenGetPopulationInfo_PlanetNotFound_ReturnNotFound()
        {
            // Arrange
            Mock<ISWApiRepository> _mockSWApiRepository = new();
            Mock<IPlanetRepository> _mockPlanetRepository = new();
            ResGetPopulationInfroDTO result = new();
            PlanetEntity? nullEnt = null;

            _mockPlanetRepository
                 .Setup(x => x.GetPlanetInfo("planet"))
                 .Returns(nullEnt);

            AppService sut = new AppService(_mockSWApiRepository.Object, _mockPlanetRepository.Object);

            // Act
            result = sut.GetPopulationInfo("planet");

            // Assert
            Assert.True(result.HasError);
            Assert.Equal(ResGetPopulationInfroEnumError.NotFound, result.Error);
        }

        #endregion

        #region WhenGetPopulationInfo_PlanetNoPopulation_ReturnNoPopulation
        [Fact]
        public void WhenGetPopulationInfo_PlanetNoPopulation_ReturnNoPopulation ()
        {
            // Arrange
            Mock<ISWApiRepository> _mockSWApiRepository = new();
            Mock<IPlanetRepository> _mockPlanetRepository = new();
            ResGetPopulationInfroDTO result = new();
            PlanetEntity? DBEnt = new PlanetEntity()
            {
                Population = null
            };

            _mockPlanetRepository
                 .Setup(x => x.GetPlanetInfo("planet"))
                 .Returns(DBEnt);

            AppService sut = new AppService(_mockSWApiRepository.Object, _mockPlanetRepository.Object);

            // Act
            result = sut.GetPopulationInfo("planet");

            // Assert
            Assert.True(result.HasError);
            Assert.Equal (result.Error, ResGetPopulationInfroEnumError.NoPopulation);
        }
        #endregion

        #region WhenGetPopulationInfo_ApiError_ReturnErrorApi
        [Fact]
        public void WhenGetPopulationInfo_ApiError_ReturnErrorApi ()
        {
            // Arrange
            Mock<ISWApiRepository> _mockSWApiRepository = new();
            Mock<IPlanetRepository> _mockPlanetRepository = new();
            ResGetPopulationInfroDTO result = new();
            PlanetEntity? DBEnt = new PlanetEntity()
            {
                Population = 1,
                Urlinfo = ""
            };

            _mockPlanetRepository
                 .Setup(x => x.GetPlanetInfo("planet"))
                 .Returns(DBEnt);
            _mockSWApiRepository
                .Setup(x => x.GetPopulationNames(DBEnt.Urlinfo))
                .ThrowsAsync(new Exception());

            AppService sut = new AppService(_mockSWApiRepository.Object, _mockPlanetRepository.Object);

            // Act
            result = sut.GetPopulationInfo("planet");

            // Assert
            Assert.True(result.HasError);
            Assert.Equal(result.Error, ResGetPopulationInfroEnumError.ErrorApi);
        }
        #endregion
    }
}
