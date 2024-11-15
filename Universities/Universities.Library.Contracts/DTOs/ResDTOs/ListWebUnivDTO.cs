using Universities.XCutting.Enums;

namespace Universities.Library.Contracts.DTOs.ResDTOs
{
    public class ListWebUnivDTO
    {
        public bool HasError { get; set; }
        public ListWebUnivErrorEnum? Error { get; set; }
        public List<WebUnivDTO> WebsUnivs { get; set; }
    }
}
