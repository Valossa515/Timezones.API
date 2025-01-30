using Microsoft.AspNetCore.Mvc;
using System.Net;
using Timezones.API.Borders.Dtos.TimeZones;
using Timezones.API.Borders.Handlers;
using Timezones.API.Shared.Handlers;
using Timezones.API.Shared.Models;

namespace Timezones.API.Controllers
{
    [Route("api/timezones")]
    [ApiController]
    public class TimezonesController : ControllerBase
    {
        private readonly IGetTimezonesHandler _handler;
        private readonly IActionResultConverter _actionResultConverter;

        public TimezonesController(IGetTimezonesHandler handler,
            IActionResultConverter actionResultConverter)
        {
            _handler = handler;
            _actionResultConverter = actionResultConverter;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(HandlerResponse<TimezoneResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(HandlerResponse<TimezoneResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(HandlerResponse<TimezoneResponse>))]
        public async Task<IActionResult> GetTimezones([FromQuery] string? timezoneId)
        {
            var request = new TimezoneRequest(timezoneId);
            var response = await _handler.Execute(request);
            return _actionResultConverter.Convert(response);
        }
    }
}