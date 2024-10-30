using Moq;
using MultiBankOOP.Infrastructure.Contracts;
using MultiBankOOP.Infrastructure.Contracts.Entities;
using MultiBankOOP.Library.Contracts.DTOs;
using MultiBankOOP.Library.Impl;

namespace MultiBankOOP.Tests.UnitTests.MultiBankOOP.Library.Impl.UnitTests.AccountServiceUnitTest
{
    public class AddMoneyUnitTest
    {
        [Fact]
        public void WhenIncomeInValidRange_ThenReturnOk()
        {
            //Arrange
            Mock<IAccountRepository> mockAccountRepository = new();
            mockAccountRepository.Setup(x => x.GetAccountInfo()).Returns(new AccountEntity
            {
                money = 0
            });
            Mock<IMovementsRepository> mockMovementRepository = new();
            mockMovementRepository.Setup(x => x.GetMovements()).Returns(new List<MovementEntity>());
            AccountService sut = new(
                mockAccountRepository.Object,
                mockMovementRepository.Object
                );


            //Act
            IncomeResultDto result = sut.AddMoney(1);

            //Assert
            Assert.False(result.ResultHasErrors);
        }
    }
}
