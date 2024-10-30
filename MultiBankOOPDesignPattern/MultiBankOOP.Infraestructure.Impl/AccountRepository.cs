using MultiBankOOP.Infrastructure.Contracts;
using MultiBankOOP.Infrastructure.Contracts.Entities;

namespace MultiBankOOP.Infrastructure.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private static List<AccountEntity> simulatedAccountDBTable = new()
        {
            new AccountEntity()
            {
                id = 1,
                pin = 111,
                number = "1000",
                money = 0
            },

            new AccountEntity()
            {
                id = 2,
                pin = 222,
                number = "2000",
                money = 0
            },

            new AccountEntity()
            {
                id = 3,
                pin = 333,
                number = "3000",
                money = 0
            }
        };

        public AccountEntity? GetAccountInfo(string number)
        {
            for (int i = 0; i < simulatedAccountDBTable.Count; ++i)
            {
                if (simulatedAccountDBTable[i].number == number)
                    return simulatedAccountDBTable[i];
            }

            return null;
        }

        public void UpdateAccount(string number, AccountEntity updatedEntity)
        {
            AccountEntity currentEntity = null;
            
            for (int i = 0; i < simulatedAccountDBTable.Count; ++i)
            {
                if (simulatedAccountDBTable[i].number == number)
                    currentEntity = simulatedAccountDBTable[i];
            }

            currentEntity.number = updatedEntity.number;
            currentEntity.money = updatedEntity.money;
        }
    }
}
