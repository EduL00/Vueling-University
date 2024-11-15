using Moq;
using Universities.Infraestructure.Contracts;
using Universities.Infraestructure.Contracts.JSONEntities;
using Universities.Library.Contracts.DTOs.ResDTOs;
using Universities.Library.Impl;
using Universities.XCutting.Enums;

namespace Universities.Testing.UnitTests.LibraryTesting
{
    public class AppServiceUnitTest
    {
        #region WhenMigrationInfo_NoApiError_ReturnNoError
        [Fact]
        public void WhenMigrationInfo_NoApiError_ReturnNoError()
        {
            // Arrange 
            Mock<IAPIRepository> _mockApiRepository = new();
            Mock<IDBUniversityRepository> _mockDBUnivRepository = new();
            JSONUniversityEntity univ = new()
            {
                Webs = new(),
                Domains = new(),
            };

            JSONListUniversityEntities jsonList = new()
            {
                Universities = new()
            };

            jsonList.Universities.Add(univ);

            _mockApiRepository
                .Setup(x => x.GetApiInfo())
                .ReturnsAsync(jsonList);

            AppService sut = new AppService(_mockApiRepository.Object, _mockDBUnivRepository.Object);

            // Act
            Task<MigrateInfoResDTO> result = sut.MigrateInfo();

            //Assert
            Assert.Null(result.Result.Error);
            Assert.False (result.Result.HasError);
        }
        #endregion

        #region WhenMigrationInfo_ApiError_RetunApiError
        [Fact]
        public void WhenMigrationInfo_ApiError_RetunApiError ()
        {
            // Arrange 
            Mock<IAPIRepository> _mockApiRepository = new();
            Mock<IDBUniversityRepository> _mockDBUnivRepository = new();

            JSONListUniversityEntities? jsonList = null;

            _mockApiRepository
                .Setup(x => x.GetApiInfo())
                .ReturnsAsync(jsonList);

            AppService sut = new AppService(_mockApiRepository.Object, _mockDBUnivRepository.Object);

            // Act
            Task<MigrateInfoResDTO> result = sut.MigrateInfo();

            //Assert
            Assert.True(result.Result.HasError);
            Assert.Equal(MigrateInfoResErrorEnum.ApiError, result.Result.Error);
        }
        #endregion

        #region WhenMigrationInfo_NoApiData_ReturnNoApiData
        [Fact]
        public void WhenMigrationInfo_NoApiData_ReturnNoApiData()
        {
            // Arrange 
            Mock<IAPIRepository> _mockApiRepository = new();
            Mock<IDBUniversityRepository> _mockDBUnivRepository = new();

            JSONListUniversityEntities? jsonList = new()
            {
                Universities = new()
            };

            _mockApiRepository
                .Setup(x => x.GetApiInfo())
                .ReturnsAsync(jsonList);

            AppService sut = new AppService(_mockApiRepository.Object, _mockDBUnivRepository.Object);

            // Act
            Task<MigrateInfoResDTO> result = sut.MigrateInfo();

            //Assert
            Assert.True(result.Result.HasError);
            Assert.Equal(MigrateInfoResErrorEnum.NoApiData, result.Result.Error);
        }
        #endregion

    }
}
