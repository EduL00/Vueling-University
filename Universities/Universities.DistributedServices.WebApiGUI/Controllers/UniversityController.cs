using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universities.Library.Contracts;
using Universities.Library.Contracts.DTOs.ResDTOs;

namespace Universities.DistributedServices.WebApiGUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IAppService _appService;

        public UniversityController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("/LisUnivsCountry")]
        public IActionResult LisUnivsCountry()
        {
            ListCountryUnivDTO result = _appService.LisUnivsCountry();

            if (result.HasError)
            {
                switch (result.Error)
                {
                    case XCutting.Enums.ListUnivsErrorEnum.NoUnivsFound:
                        return StatusCode(StatusCodes.Status404NotFound, "No Universities found in Data Base");
                }
            }
            return Ok(result.Univs);
        }

        [HttpGet("/ListUnivsWebs")]
        public IActionResult ListUnivsWebs(string containsName)
        {
            ListWebUnivDTO result = _appService.LisUnivsWebs(containsName);

            if (result.HasError)
            {
                switch (result.Error)
                {
                    case XCutting.Enums.ListWebUnivErrorEnum.NoUnivsFound:
                        return StatusCode(StatusCodes.Status404NotFound, "No Universities found in Data Base");
                }
            }

            return Ok(result.WebsUnivs);
        }

        [HttpPost("/DeleteUniversity")]
        public IActionResult DeleteUniversity (int UnivId)
        {
            DeleteUnivDTO result = _appService.DeleteUniv(UnivId);

            if (result.HasError)
            {
                switch (result.Error)
                {
                    case XCutting.Enums.DeleteUnivErrorEnum.NoUnivFound:
                        return StatusCode(StatusCodes.Status404NotFound, "University not found");
                }
            }

            return Ok("University deleted successfully");
        }
    }
}
