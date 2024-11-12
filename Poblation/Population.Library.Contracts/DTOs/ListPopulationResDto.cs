using Population.XCutting.Enums;

namespace Population.Library.Contracts.DTOs
{
    public class ListPopulationResDto
    {
        public bool HasError { get; set; }
        public ListPopulationReEnumError? Error { get; set; }
        public List<PopulationResDto> result { get; set; }

    }
}
