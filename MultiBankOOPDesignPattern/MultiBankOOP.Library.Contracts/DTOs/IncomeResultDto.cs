using MultiBankOOP.XCutting.Enums;

namespace MultiBankOOP.Library.Contracts.DTOs
{
    public class IncomeResultDto
    {
        public bool ResultHasErrors;
        public IncomeErrorEnum? Error;
        public decimal maxIncomeAllowed;
        public decimal totalMoney;
    }
}