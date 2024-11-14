using SimulSW.Infraestructure.Contracts;
using SimulSW.Infraestructure.Contracts.APIEntities;
using SimulSW.Infraestructure.Contracts.DBEntities;
using SimulSW.Library.Contracts;
using SimulSW.Library.Contracts.DTOs;

namespace SimulSW.Library.Impl
{ 
    public class AppService : IAppService
    {
        private ISWApiRepository _swApiRespotiroy;
        private IPlanetRepository _planetRepository;

        public AppService(ISWApiRepository swApiRepository, IPlanetRepository planetRepository)
        {
            _swApiRespotiroy = swApiRepository;
            _planetRepository = planetRepository;
        }
        public ResGetApiInfoDTO GetApiInfo()
        {
            ResGetApiInfoDTO result = new()
            {
                HasError = false,
                Error = null
            };

            Task<APIInfoFromJsonEntity> apiDataTask = _swApiRespotiroy.GetApiInfo();
            if (apiDataTask.IsFaulted) 
            {
                result.HasError = true;
                result.Error = XCuttin.Enums.ResGetApiInfoErrorEnum.ErrorInApi;
                return result;

            }

            APIInfoFromJsonEntity apiData = apiDataTask.Result;

            result.PlanetNames = StoreInBD(apiData);
            
            return result;
        }

        private List<string> StoreInBD(APIInfoFromJsonEntity apiData)
        {
            List<string> result = new();

            foreach (PlanetInfoFromJsonEntity planet in apiData.PlanetsInfo)
            {
                PlanetEntity planetDBEntity = new();

                result.Add(planet.Name);

                ParseFromApiEntityToDBEntity(planet, planetDBEntity);

                _planetRepository.RegisterPlanet (planetDBEntity);
            }

            return result;
        }

        private void ParseFromApiEntityToDBEntity (PlanetInfoFromJsonEntity planet, PlanetEntity planetDBEntity)
        {
            planetDBEntity.Name = planet.Name;
            planetDBEntity.OrbRotation = int.Parse(planet.Rot);
            planetDBEntity.OrbPeriod = int.Parse(planet.Perd);
            planetDBEntity.Climate = planet.Climate;
            if (planet.Population == "unknown")
                planetDBEntity.Population = null;
            else
                planetDBEntity.Population = long.Parse(planet.Population);
            planetDBEntity.Urlinfo = planet.Url;

        }

        public ResGetPopulationInfroDTO GetPopulationInfo(string planetName)
        {
            ResGetPopulationInfroDTO result = new()
            {
                HasError = false,
                Error = null
            };

            PlanetEntity planetDBEnt = _planetRepository.GetPlanetInfo(planetName);

            if (planetDBEnt == null)
            {
                result.HasError = true;
                result.Error = XCuttin.Enums.ResGetPopulationInfroEnumError.NotFound;
                return result;
            }

            if (planetDBEnt.Population == null)
            {
                result.HasError = true;
                result.Error = XCuttin.Enums.ResGetPopulationInfroEnumError.NoPopulation;
                return result;
            }
            Task <List<string>> apiListNamesInfo = _swApiRespotiroy.GetPopulationNames(planetDBEnt.Urlinfo);

            if (apiListNamesInfo.IsFaulted)
            {
                result.HasError = true;
                result.Error = XCuttin.Enums.ResGetPopulationInfroEnumError.ErrorApi;
                return result;
            }

            result.PopulationNames = apiListNamesInfo.Result;

            return result;

        }
    }
}
