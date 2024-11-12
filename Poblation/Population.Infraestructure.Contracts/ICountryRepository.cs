using Population.Infraestructure.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population.Infraestructure.Contracts
{
    public interface ICountryRepository
    {
        public void RegisterCountry (CountryEntity country);

        List<CountryEntity> GetCountries();
    }
}
