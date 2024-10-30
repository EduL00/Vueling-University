using MultiBankOOP.Infrastructure.Contracts;
using MultiBankOOP.Infrastructure.Contracts.Entities;

namespace MultiBankOOP.Infrastructure.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private static List<AccountEntity> simulatedAccountDBTable = new()
        {
            new()
            {
                id = 1,
                number = "1000",
                money = 0
            }
        };

        public AccountEntity? GetAccountInfo()
        {
            return simulatedAccountDBTable.First();
        }

        public void UpdateAccount(AccountEntity updatedEntity)
        {
            AccountEntity currentEntity = simulatedAccountDBTable.First();

            currentEntity.number = updatedEntity.number;
            currentEntity.money = updatedEntity.money;
        }
    }
}
