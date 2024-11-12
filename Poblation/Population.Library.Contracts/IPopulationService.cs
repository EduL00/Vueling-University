using Population.Library.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population.Library.Contracts
{
    public interface IPopulationService
    {
        public void ImportDataToSerialize(string dataAsString);
        public ListPopulationResDto ListPopulation(ListPopulationReqDto input);
    }
}
