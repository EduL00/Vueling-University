using SimulSW.Infraestructure.Contracts.APIEntities;

namespace SimulSW.Infraestructure.Contracts
{
    public interface ISWApiRepository
    {
        public Task<APIInfoFromJsonEntity> GetApiInfo();
        public Task<List<string>> GetPopulationNames(string planetUrlInfo);
        
    }
}
