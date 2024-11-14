using SimulSW.Library.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulSW.Library.Contracts
{
    public interface IAppService
    {
        public ResGetApiInfoDTO GetApiInfo();
        public ResGetPopulationInfroDTO GetPopulationInfo(string planetName);
    }
}
