using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Universities.DistributedServices.WebApiGUI.Controllers;
using Universities.Library.Contracts;
using Universities.Library.Contracts.DTOs.ResDTOs;

namespace Universities.Testing.UnitTests.DistributedServicesTesting
{
    public class MigrationControllerUnitTest
    {
        #region WhenMigrationInfo_NoApiError_ReturnNoError
        [Fact]
        public void WhenMigrationInfo_NoApiError_ReturnNoError ()
        {
            // Arrange
            Mock<IAppService> _mockAppService = new();
            MigrateInfoResDTO serviceResult = new MigrateInfoResDTO()
            {
                HasError = false,
                Error = null
            };

            _mockAppService
                .Setup(x => x.MigrateInfo())
                .ReturnsAsync(serviceResult);

            ApiController sut = new ApiController(_mockAppService.Object);

            // Act

            Task<IActionResult> result = sut.MigrateInfo();

            // Assert
            Assert.IsType<OkResult>  (result.Result);
        }
        #endregion
        #region WhenMigrationInfo_ApiError_ReturnApiError
        [Fact]
        public void WhenMigrationInfo_ApiError_ReturnApiError ()
        {
            // Arrange
            Mock<IAppService> _mockAppService = new();
            MigrateInfoResDTO serviceResult = new MigrateInfoResDTO()
            {
                HasError = true,
                Error = XCutting.Enums.MigrateInfoResErrorEnum.ApiError
            };

            _mockAppService
                .Setup(x => x.MigrateInfo())
                .ReturnsAsync(serviceResult);

            ApiController sut = new ApiController(_mockAppService.Object);

            // Act

            Task<IActionResult> result = sut.MigrateInfo();

            // Assert
            Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status400BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.Equal("Error in API while migrate the data to the Data Base", ((ObjectResult)result.Result).Value);
        }
        #endregion
        #region WhenMigrationInfo_NoApiData_ReturnNoApiData
        [Fact]
        public void WhenMigrationInfo_NoApiData_ReturnNoApiData()
        {
            // Arrange
            Mock<IAppService> _mockAppService = new();
            MigrateInfoResDTO serviceResult = new MigrateInfoResDTO()
            {
                HasError = true,
                Error = XCutting.Enums.MigrateInfoResErrorEnum.NoApiData
            };

            _mockAppService
                .Setup(x => x.MigrateInfo())
                .ReturnsAsync(serviceResult);

            ApiController sut = new ApiController(_mockAppService.Object);

            // Act

            Task<IActionResult> result = sut.MigrateInfo();

            // Assert
            Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result.Result).StatusCode);
            Assert.Equal("Not data found in the University  WEB API", ((ObjectResult)result.Result).Value);
        }
        #endregion
    }
}
