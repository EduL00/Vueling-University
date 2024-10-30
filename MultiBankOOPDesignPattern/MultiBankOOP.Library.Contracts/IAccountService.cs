using MultiBankOOP.Library.Contracts.DTOs;

namespace MultiBankOOP.Library.Contracts
{
    public interface IAccountService
    {
        void SetUserNumber(string number);
        LoginResultDto Login(string number, int pin);
        decimal? GetMoney();
        IncomeResultDto AddMoney(decimal income);
        OutcomeResultDto RetireMoney(decimal outcome);
        MovementListDto GetMovements();
        MovementListDto GetIncomes();
        MovementListDto GetOutcomes();
    }
}
