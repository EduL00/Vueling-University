using MultiBankOOP.Library.Contracts.DTOs;

namespace MultiBankOOP.Library.Contracts
{
    public interface IAccountService
    {
        decimal? GetMoney();
        IncomeResultDto AddMoney(decimal income);
        OutcomeResultDto RetireMoney(decimal outcome);
        MovementListDto GetMovements();
        MovementListDto GetIncomes();
        MovementListDto GetOutcomes();
    }
}
