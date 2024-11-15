using Universities.Infraestructure.Contracts;
using Universities.Infraestructure.Contracts.DBEntities;
using Universities.Infraestructure.Contracts.JSONEntities;
using Universities.Library.Contracts;
using Universities.Library.Contracts.DTOs;
using Universities.Library.Contracts.DTOs.ResDTOs;

namespace Universities.Library.Impl
{
    public class AppService : IAppService
    {
        private readonly IAPIRepository _apiRespository;
        private readonly IDBUniversityRepository _dbUniversityRepsitory;
        private const int FalseInt = 0;
        private const int TrueInt = 1;

        public AppService(IAPIRepository apiRepository, IDBUniversityRepository dbUniversityRepository)
        {
             _apiRespository = apiRepository;
            _dbUniversityRepsitory = dbUniversityRepository;
        }
        public async Task<MigrateInfoResDTO> MigrateInfo()
        {
            MigrateInfoResDTO result = new MigrateInfoResDTO()
            {
                HasError = false,
                Error = null
            };

            JSONListUniversityEntities? apiTaskInfo = await _apiRespository.GetApiInfo();

            if (apiTaskInfo == null)
            {
                result.HasError = true;
                result.Error = XCutting.Enums.MigrateInfoResErrorEnum.ApiError;
                return result;
            }

            if (apiTaskInfo.Universities.Count == 0)
            {
                result.HasError = true;
                result.Error = XCutting.Enums.MigrateInfoResErrorEnum.NoApiData;
                return result;
            }

            StoreInDB(apiTaskInfo);

            return result;

        }

        private void StoreInDB (JSONListUniversityEntities apiData)
        {
            foreach (JSONUniversityEntity jsonUnivEntity in apiData.Universities)
            {
                DBUniversityEntity dbUnivEnt = new DBUniversityEntity();

                ParseJsonUnivEntToDBUnivEnt (jsonUnivEntity, dbUnivEnt);

                foreach (string jsonDomain in jsonUnivEntity.Domains) 
                {
                    DBDomainEntity dbDomain = new DBDomainEntity();

                    ParseJsonUnivDomainToDBDomainEnt (jsonDomain, dbDomain);

                    dbUnivEnt.Domains.Add (dbDomain);
                }

                foreach (string jsonWeb in  jsonUnivEntity.Webs)
                {
                    DBWebEntity dbWeb = new DBWebEntity();
                    ParseJsonUnivWebToDBWebEnt (jsonWeb, dbWeb);

                    dbUnivEnt.Webs.Add(dbWeb);
                }

                _dbUniversityRepsitory.RegisterUniv(dbUnivEnt);
            }
        }

        private void ParseJsonUnivEntToDBUnivEnt (JSONUniversityEntity jsonEnt, DBUniversityEntity dbEnt)
        {
            dbEnt.Name = jsonEnt.Name;
            dbEnt.Alpha = jsonEnt.Alpha;
            dbEnt.Province = jsonEnt.Province;
            dbEnt.Country = jsonEnt.Country;
            dbEnt.Deleted = FalseInt;
        }

        private void ParseJsonUnivDomainToDBDomainEnt (string jsonDomain, DBDomainEntity dbDomain)
        {
            dbDomain.Domain1 = jsonDomain;
        }

        private void ParseJsonUnivWebToDBWebEnt (string jsonWeb, DBWebEntity dbWeb)
        {
            dbWeb.Web1 = jsonWeb;
        }

        public ListCountryUnivDTO LisUnivsCountry()
        {
            ListCountryUnivDTO result = new()
            {
                HasError = false,
                Error = null
            };

            List<DBUniversityEntity> univs = _dbUniversityRepsitory.GetUnivsEssentialInfo();
            List<CountryUnivDTO> CountryUnivs = new();

            if (univs.Count == 0)
            {
                result.HasError = true;
                result.Error = XCutting.Enums.ListUnivsErrorEnum.NoUnivsFound;
                return result;
            }

            foreach (DBUniversityEntity univ in univs)
            {
                if (univ.Deleted == TrueInt) continue;

                CountryUnivDTO countryUniv = new CountryUnivDTO()
                {
                    IDUniv = univ.Id,
                    UnivName = univ.Name,
                    CountryName = univ.Country
                };

                CountryUnivs.Add(countryUniv);
            }

            result.Univs = CountryUnivs;

            return result;
        }

        public ListWebUnivDTO LisUnivsWebs(string containName)
        {
            ListWebUnivDTO result = new()
            {
                HasError = false,
                Error = null
            };

            List<DBUniversityEntity> univs = _dbUniversityRepsitory.GetUnivsInfoWebs();
            List<WebUnivDTO> WebUnivs = new();

            if (univs.Count == 0)
            {
                result.HasError = true;
                result.Error = XCutting.Enums.ListWebUnivErrorEnum.NoUnivsFound;
                return result;
            }

            foreach (DBUniversityEntity univ in univs)
            {
                if (univ.Name.Contains(containName) == false) continue;
                if (univ.Deleted == TrueInt) continue;

                WebUnivDTO webUniv = new WebUnivDTO()
                {
                    IDUniv = univ.Id,
                    UnivName = univ.Name,
                    UnivWebs = new()
                };

                foreach (DBWebEntity web in univ.Webs)
                {
                    webUniv.UnivWebs.Add(web.Web1);
                }

                WebUnivs.Add(webUniv);
            }

            result.WebsUnivs = WebUnivs;

            return result;
        }

        public DeleteUnivDTO DeleteUniv(int univId)
        {
            DeleteUnivDTO result = new()
            {
                HasError = false,
                Error = null
            };

            DBUniversityEntity univ = _dbUniversityRepsitory.GetSingleUnivInfo(univId);

            if ((univ == null) || (univ.Deleted == TrueInt))
            {
                result.HasError = true;
                result.Error = XCutting.Enums.DeleteUnivErrorEnum.NoUnivFound;
                return result;
            }

            _dbUniversityRepsitory.DeleteUniv(univId);

            return result;
        }
    }
}
