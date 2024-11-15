using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universities.Library.Contracts;
using Universities.Library.Contracts.DTOs.ResDTOs;

namespace Universities.DistributedServices.WebApiGUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IAppService _appService;

        public ApiController(IAppService appService)
        {
            _appService = appService;
        }
        [HttpGet("/MigrateInfo")]
        public async Task<IActionResult> MigrateInfo()
        {
            MigrateInfoResDTO result = await _appService.MigrateInfo();

            if (result.HasError)
            {
                switch (result.Error)
                {
                    case XCutting.Enums.MigrateInfoResErrorEnum.ApiError:
                        return StatusCode(StatusCodes.Status400BadRequest, "Error in API while migrate the data to the Data Base");
                    case XCutting.Enums.MigrateInfoResErrorEnum.NoApiData:
                        return StatusCode(StatusCodes.Status404NotFound, "Not data found in the University  WEB API");
                }
            }

            return Ok();
        }
    }
}
