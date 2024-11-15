using Universities.XCutting.Enums;

namespace Universities.Library.Contracts.DTOs.ResDTOs
{
    public class ListCountryUnivDTO
    {
        public bool HasError { get; set; }
        public ListUnivsErrorEnum? Error;
        public List<CountryUnivDTO> Univs { get; set; }
    }
}
