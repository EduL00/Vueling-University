using MultiBank.XCutting.Enums;
using MultiBankOOP.XCutting.Enums;

namespace MultiBankOOP.Library.Contracts.DTOs
{
    public class OutcomeResultDto
    {
        public bool ResultHasErrors;
        public OutcomeErrorEnum? Error;
        public decimal maxOutcomeAllowed;
        public decimal maxDebtAllowed;
        public decimal totalMoney;
    }
}
