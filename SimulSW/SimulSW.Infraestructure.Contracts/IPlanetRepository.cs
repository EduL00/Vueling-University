using SimulSW.Infraestructure.Contracts.DBEntities;

namespace SimulSW.Infraestructure.Contracts
{
    public interface IPlanetRepository
    {
        public void RegisterPlanet (PlanetEntity planet);
        public PlanetEntity GetPlanetInfo (string planetName);
    }
}
