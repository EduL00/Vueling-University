using MultiBankOOP.Library.Contracts.DTOs;

namespace MultiBankOOP.Library.Contracts.DTOs
{
    public class MovementListDto
    {
        public List<MovementDto> movements;
        public decimal totalMoney;
    }
}
