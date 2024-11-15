using Universities.XCutting.Enums;

namespace Universities.Library.Contracts.DTOs.ResDTOs
{
    public class DeleteUnivDTO
    {
        public bool HasError { get; set; }
        public DeleteUnivErrorEnum? Error { get; set; }
    }
}
