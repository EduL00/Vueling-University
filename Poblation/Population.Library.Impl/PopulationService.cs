using Population.Domain.Models;
using Population.Infraestructure.Contracts;
using Population.Infraestructure.Contracts.Entities;
using Population.Library.Contracts;
using Population.Library.Contracts.DTOs;
using System.Data;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Population.Library.Impl
{
    public class PopulationService : IPopulationService
    {
        private readonly ICountryRepository _countryRepository;
        private const int MinYear = 1961;
        private const int MaxYear = 2018;

        public PopulationService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public void ImportDataToSerialize(string dataAsString)
        {
            PopulationDataFromJsonEntity? dataDeserialized = JsonSerializer.Deserialize<PopulationDataFromJsonEntity>(dataAsString);

            StoreInDB (dataDeserialized);
        }

        private void StoreInDB(PopulationDataFromJsonEntity dataDeserialized)
        {
            for (int i = 0; i < dataDeserialized.CountryInfo.Count; i++)
            {

                CountryInfoFromJsonEntity IData = dataDeserialized.CountryInfo[i];
                CountryEntity ent = new CountryEntity()
                {
                    Name = IData.Country
                };

                List<CountryInfoEntity> info = new List<CountryInfoEntity>();

                for (int j = 0; j < IData.PopulationCountryInfo.Count; ++j)
                {
                    CountryInfoEntity infoEntity = new CountryInfoEntity();
                    PopulationCountryInfoFromJsonEntity jsonCountryInfo = new();

                    jsonCountryInfo = IData.PopulationCountryInfo[j];

                    ParsePopulationCountryInfoFromJsonEntityToCountryInfoEntity(infoEntity, jsonCountryInfo);

                    info.Add(infoEntity);

                }

                ent.CountryInfos = info;
                _countryRepository.RegisterCountry(ent);

            }

        }

        private void ParsePopulationCountryInfoFromJsonEntityToCountryInfoEntity (CountryInfoEntity ent, PopulationCountryInfoFromJsonEntity jsonData)
        {
            ent.Year = jsonData.Year;
            ent.Population = (int)jsonData.Value;

        }

        public ListPopulationResDto ListPopulation(ListPopulationReqDto input)
        {
            ListPopulationResDto result = new ListPopulationResDto()
            {
                HasError = false,
                Error = null
            };

            #region Input Cheks
            if (input.firstCharacter.Length != 1)
            {
                result.HasError = true;
                result.Error = XCutting.Enums.ListPopulationReEnumError.InvalidChar;
                return result;
            }

            if ((input.year < MinYear) || (input.year > MaxYear))
            {
                result.HasError = true;
                result.Error = XCutting.Enums.ListPopulationReEnumError.YearNotInBounds; 
                return result;
            }
            #endregion


            List<PopulationResDto> resultList = new List<PopulationResDto>();
            List<CountryEntity> countries = _countryRepository.GetCountries();

            if (countries == null)
            {
                result.HasError = true;
                result.Error = XCutting.Enums.ListPopulationReEnumError.NoCountriesInBD;
                return result;
            }

            foreach (CountryEntity country in countries)
            {
                PopulationResDto resultListEntry = new();

                CountryInfoFromJsonEntity countryModel = new();
                ParseCountryEntityToCountryInfoFromJsonEntity (country, countryModel);

                if (countryModel.NameStartWithCharacter(input.firstCharacter) == false)
                    continue;

                resultListEntry.CountryName = countryModel.Country;
                resultListEntry.CountryPop = (int)countryModel.PopulationCountryInfo.Where(d => d.Year == input.year).Select(d => d.Value).FirstOrDefault();

                resultList.Add(resultListEntry);
            }

            if (resultList.Count == 0)
            {
                result.HasError = true;
                result.Error = XCutting.Enums.ListPopulationReEnumError.NoCountriesWithCondition;
                return result;
            }

            result.result = resultList;
            return result;
        }

        private void ParseCountryEntityToCountryInfoFromJsonEntity(CountryEntity ent, CountryInfoFromJsonEntity model)
        {
            model.Country = ent.Name;
            model.PopulationCountryInfo = new List<PopulationCountryInfoFromJsonEntity>();

            List<PopulationCountryInfoFromJsonEntity> infoCountryModel = new();

            foreach (CountryInfoEntity infoEntity in ent.CountryInfos)
            {
                PopulationCountryInfoFromJsonEntity infoCountry = new();

                ParseCountryInfoEntityToPopulationCountryInfoFromJsonEntity (infoCountry, infoEntity);

                model.PopulationCountryInfo.Add(infoCountry);
            }

        }

       private void  ParseCountryInfoEntityToPopulationCountryInfoFromJsonEntity (PopulationCountryInfoFromJsonEntity infoCountryModel, CountryInfoEntity infoCountryEntity)
        {
            infoCountryModel.Year = infoCountryEntity.Year;
            infoCountryModel.Value = infoCountryEntity.Population;
        }
    }
}
