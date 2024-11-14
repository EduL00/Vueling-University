using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimulSW.Library.Contracts;
using SimulSW.Library.Contracts.DTOs;
using SimulSW.Presentation.WEBApiGUI.Controllers;

namespace SimulSW.Testing.UnitTesting.SimulSW.Presentation
{
    public class AppControllerUnitTest
    {
        #region WhenGetApiInfo_NoErrors_ReturnsListPlanetNames
        [Fact]
        public void WhenGetApiInfo_NoErrors_ReturnsListPlanetNames()
        {
            // Arrange
            Mock<IAppService> _mockAppService = new();
            ResGetApiInfoDTO serviceResult = new()
            {
                HasError = false,
                Error = null,
                PlanetNames = new List<string>()
                {
                    "planet"
                }
            };

            _mockAppService
                .Setup (x => x.GetApiInfo())
                .Returns(serviceResult);

            AppController sut = new(_mockAppService.Object);

            // Act
            IActionResult result = sut.GetApiInfo();

            // Assert
            Assert.IsType<OkObjectResult> (result);
        }
        #endregion

        #region WhenGetApiInfo_ApiError_ReturnsErrorInApi
        [Fact]
        public void WhenGetApiInfo_ApiError_ReturnsErrorInApi()
        {
            // Arrange
            Mock<IAppService> _mockAppService = new();
            ResGetApiInfoDTO serviceResult = new()
            {
                HasError = true,
                Error = XCuttin.Enums.ResGetApiInfoErrorEnum.ErrorInApi
            };

            _mockAppService
                .Setup(x => x.GetApiInfo())
                .Returns(serviceResult);

            AppController sut = new(_mockAppService.Object);

            // Act
            IActionResult result = sut.GetApiInfo();

            // Assert
            Assert.IsType<ObjectResult> (result);
            Assert.Equal (StatusCodes.Status400BadRequest, ((ObjectResult)result).StatusCode);
            Assert.Equal("Error getting the information data from the API", ((ObjectResult)result).Value);
        }
        #endregion

        #region WhenGetPopulationInfo_NoErrors_ReturnsListPopulationNames
        [Fact]
        public void WhenGetPopulationInfo_NoErrors_ReturnsListPopulationNames ()
        {
            // Arrange
            Mock<IAppService> _mockAppService = new();
            ResGetPopulationInfroDTO serviceResult = new()
            {
                HasError = false,
                Error = null,
                PopulationNames = new List<string>()
                {
                    "Bobba Fett"
                }
            };

            _mockAppService
                .Setup(x => x.GetPopulationInfo("planet"))
                .Returns(serviceResult);

            AppController sut = new(_mockAppService.Object);

            // Act
            IActionResult result = sut.GetPopulationInfo("planet");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region WhenGetPopulationInfo_PlanetNotFound_ReturnsPlanetNotFound
        [Fact]
        public void WhenGetPopulationInfo_PlanetNotFound_ReturnsPlanetNotFound()
        {
            // Arrange
            Mock<IAppService> _mockAppService = new();
            ResGetPopulationInfroDTO serviceResult = new()
            {
                HasError = true,
                Error = XCuttin.Enums.ResGetPopulationInfroEnumError.NotFound
            };

            _mockAppService
                .Setup(x => x.GetPopulationInfo("planet"))
                .Returns(serviceResult);

            AppController sut = new(_mockAppService.Object);

            // Act
            IActionResult result = sut.GetPopulationInfo("planet");

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal (StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
            Assert.Equal("Planet not found", ((ObjectResult)result).Value);

        }
        #endregion

        #region WhenGetPopulationInfo_PlanetNotPopulation_ReturnsPlanetNotHasPopulation
        [Fact]
        public void WhenGetPopulationInfo_PlanetNotPopulation_ReturnsPlanetNotHasPopulation()
        {
            // Arrange
            Mock<IAppService> _mockAppService = new();
            ResGetPopulationInfroDTO serviceResult = new()
            {
                HasError = true,
                Error = XCuttin.Enums.ResGetPopulationInfroEnumError.NoPopulation
            };

            _mockAppService
                .Setup(x => x.GetPopulationInfo("planet"))
                .Returns(serviceResult);

            AppController sut = new(_mockAppService.Object);

            // Act
            IActionResult result = sut.GetPopulationInfo("planet");

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, ((ObjectResult)result).StatusCode);
            Assert.Equal("Planet with unknown population", ((ObjectResult)result).Value);

        }
        #endregion

        #region WhenGetPopulationInfo_ApiError_ReturnsApiError
        [Fact]
        public void WhenGetPopulationInfo_ApiError_ReturnsApiError()
        {
            // Arrange
            Mock<IAppService> _mockAppService = new();
            ResGetPopulationInfroDTO serviceResult = new()
            {
                HasError = true,
                Error = XCuttin.Enums.ResGetPopulationInfroEnumError.ErrorApi
            };

            _mockAppService
                .Setup(x => x.GetPopulationInfo("planet"))
                .Returns(serviceResult);

            AppController sut = new(_mockAppService.Object);

            // Act
            IActionResult result = sut.GetPopulationInfo("planet");

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, ((ObjectResult)result).StatusCode);
            Assert.Equal("Error getting the population info from the Api", ((ObjectResult)result).Value);

        }
        #endregion

    }
}
