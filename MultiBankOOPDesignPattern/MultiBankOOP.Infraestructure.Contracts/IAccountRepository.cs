using MultiBankOOP.Infrastructure.Contracts.Entities;

namespace MultiBankOOP.Infrastructure.Contracts
{
    public interface IAccountRepository
    {
        AccountEntity? GetAccountInfo();
        void UpdateAccount(AccountEntity updatedEntity);
    }
}
