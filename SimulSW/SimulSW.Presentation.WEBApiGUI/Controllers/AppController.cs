using Microsoft.AspNetCore.Mvc;
using SimulSW.Library.Contracts;
using SimulSW.Library.Contracts.DTOs;

namespace SimulSW.Presentation.WEBApiGUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IAppService _appService;

        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("/ApiInfo")]
        public ActionResult GetApiInfo()
        {
            ResGetApiInfoDTO result = _appService.GetApiInfo();

            if (result.HasError)
            {
                if (result.Error == XCuttin.Enums.ResGetApiInfoErrorEnum.ErrorInApi)
                    return StatusCode(StatusCodes.Status400BadRequest, "Error getting the information data from the API");
            }

            return Ok(result.PlanetNames);
        }

        [HttpGet("/PopulationInfo")] 
        public ActionResult GetPopulationInfo(string planetName) 
        {
            ResGetPopulationInfroDTO result = _appService.GetPopulationInfo(planetName);

            if (result.HasError)
            {
                if (result.Error == XCuttin.Enums.ResGetPopulationInfroEnumError.NotFound)
                    return StatusCode(StatusCodes.Status404NotFound, "Planet not found");
                if (result.Error == XCuttin.Enums.ResGetPopulationInfroEnumError.NoPopulation)
                    return StatusCode(StatusCodes.Status400BadRequest, "Planet wit unknown population");
                if (result.Error == XCuttin.Enums.ResGetPopulationInfroEnumError.ErrorApi)
                    return StatusCode(StatusCodes.Status400BadRequest, "Error getting the population info from the Api");
            }

            return Ok(result.PopulationNames); 
        }
    }
}
