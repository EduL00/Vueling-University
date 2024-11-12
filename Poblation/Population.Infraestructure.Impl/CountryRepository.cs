using Microsoft.EntityFrameworkCore;
using Population.Infraestructure.Contracts;
using Population.Infraestructure.Contracts.Entities;
using Population.Infraestructure.Impl.DbContexts;

namespace Population.Infraestructure.Impl
{
    public class CountryRepository : ICountryRepository
    {
        private readonly PopulationDBContext _dbContext;

        public CountryRepository(PopulationDBContext dbContext)
        {
             _dbContext = dbContext;
        }
        public void RegisterCountry(CountryEntity country)
        {
            _dbContext.Add(country);
            _dbContext.SaveChanges();
        }

        public List<CountryEntity> GetCountries()
        {
            return _dbContext.Countries.Include(x => x.CountryInfos).ToList();
        }
    }
}
