using SimulSW.Library.Contracts.DTOs;

namespace SimulSW.Library.Contracts
{
    public interface IAppService
    {
        public ResGetApiInfoDTO GetApiInfo();
        public ResGetPopulationInfroDTO GetPopulationInfo(string planetName);
    }
}
