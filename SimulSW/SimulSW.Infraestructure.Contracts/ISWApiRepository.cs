using SimulSW.Infraestructure.Contracts.APIEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulSW.Infraestructure.Contracts
{
    public interface ISWApiRepository
    {
        public Task<APIInfoFromJsonEntity> GetApiInfo();
        public Task<List<string>> GetPopulationNames(string planetUrlInfo);
        
    }
}
