using SimulSW.XCuttin.Enums;

namespace SimulSW.Library.Contracts.DTOs
{
    public class ResGetApiInfoDTO
    {
        public bool HasError {  get; set; }
        public ResGetApiInfoErrorEnum? Error { get; set; }
        public List<string> PlanetNames { get; set; }
    }
}
