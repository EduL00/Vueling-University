using SimulSW.XCuttin.Enums;

namespace SimulSW.Library.Contracts.DTOs
{
    public class ResGetPopulationInfroDTO
    {
        public bool HasError { get; set; }
        public ResGetPopulationInfroEnumError? Error { get; set; }
        public List<string> PopulationNames { get; set; }
    }
}
