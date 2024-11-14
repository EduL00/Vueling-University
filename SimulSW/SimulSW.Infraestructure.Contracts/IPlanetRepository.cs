using SimulSW.Infraestructure.Contracts.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulSW.Infraestructure.Contracts
{
    public interface IPlanetRepository
    {
        public void RegisterPlanet (PlanetEntity planet);
        public PlanetEntity GetPlanetInfo (string planetName);
    }
}
