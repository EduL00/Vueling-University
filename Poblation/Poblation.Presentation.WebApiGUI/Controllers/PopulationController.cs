using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Population.Library.Contracts;
using Population.Library.Contracts.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace Population.Presentation.WebApiGUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulationController : ControllerBase
    {
        private readonly IPopulationService _populationService;

        public PopulationController(IPopulationService populationService)
        {
            _populationService = populationService;
        }

        [HttpGet]
        public async Task<ActionResult> ShowPopulation (string firstCharacter, int year, bool storeData)
        {
            if (storeData)
            {
                using HttpClient client = new HttpClient();

                HttpResponseMessage data = await client.GetAsync("https://countriesnow.space/api/v0.1/countries/population");
                string dataAsString = await data.Content.ReadAsStringAsync();

                _populationService.ImportDataToSerialize(dataAsString);
            }

            ListPopulationReqDto input = new ListPopulationReqDto()
            {
                firstCharacter = firstCharacter,
                year = year
            };
            ListPopulationResDto result = _populationService.ListPopulation (input);

            if (result.HasError)
            {
                if (result.Error == XCutting.Enums.ListPopulationReEnumError.NoCountriesInBD)
                    return StatusCode(StatusCodes.Status404NotFound, "No countries found in the BD");
                if (result.Error == XCutting.Enums.ListPopulationReEnumError.InvalidChar)
                    return StatusCode(StatusCodes.Status400BadRequest, "Character must contain one and only character");
                if (result.Error == XCutting.Enums.ListPopulationReEnumError.YearNotInBounds)
                    return StatusCode(StatusCodes.Status416RangeNotSatisfiable, "Year has to be between 1961 and 2018");
                if (result.Error == XCutting.Enums.ListPopulationReEnumError.NoCountriesWithCondition)
                    return StatusCode(StatusCodes.Status404NotFound, "No Countries witch satisfies the conditions found");
            }

            return Ok(result.result);

        }
    }
}
