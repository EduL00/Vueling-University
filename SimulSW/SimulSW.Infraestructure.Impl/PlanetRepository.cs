using SimulSW.Infraestructure.Contracts;
using SimulSW.Infraestructure.Contracts.DBEntities;
using SimulSW.Infraestructure.Impl.DBContexts;

namespace SimulSW.Infraestructure.Impl
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly SWDBContext _dbContext;

        public PlanetRepository(SWDBContext dbContext)
        {
             _dbContext = dbContext;
        }
        public void RegisterPlanet(PlanetEntity planet)
        {
            _dbContext.Planets.Add(planet);
            _dbContext.SaveChanges();
        }

        public PlanetEntity GetPlanetInfo(string planetName)
        {
            return _dbContext.Planets.FirstOrDefault(x => x.Name == planetName);
        }
    }
}
