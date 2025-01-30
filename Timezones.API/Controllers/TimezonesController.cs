using Microsoft.AspNetCore.Mvc;
using Timezones.API.Borders.Dtos.TimeZones;
using Timezones.API.Borders.Handlers;

namespace Timezones.API.Controllers
{
    [Route("api/timezones")]
    [ApiController]
    public class TimezonesController : ControllerBase
    {
        private readonly IGetTimezonesHandler _handler;

        public TimezonesController(IGetTimezonesHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> GetTimezones([FromQuery] string? timezoneId)
        {
            var request = new TimezoneRequest(timezoneId);
            var response = await _handler.Execute(request);

            if (!response.IsSuccess)
                return StatusCode(response.StatusCode, response);

            return Ok(response.Result);
        }
    }
}
