using MultiBankOOP.Infrastructure.Contracts.Entities;

namespace MultiBankOOP.Infrastructure.Contracts
{
    public interface IAccountRepository
    {
        AccountEntity? GetAccountInfo(string number);
        void UpdateAccount(string number, AccountEntity updatedEntity);
    }
}
