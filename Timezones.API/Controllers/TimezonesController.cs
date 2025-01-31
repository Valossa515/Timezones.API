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
        private readonly IGetTimezonesHandler _getTimezonesHandler;
        private readonly IConvertTimezoneHandler _convertTimezoneHandler;
        private readonly IActionResultConverter _actionResultConverter;

        public TimezonesController(IGetTimezonesHandler getTimezonesHandler,
            IConvertTimezoneHandler convertTimezoneHandler,
            IActionResultConverter actionResultConverter)
        {
            _getTimezonesHandler = getTimezonesHandler;
            _convertTimezoneHandler = convertTimezoneHandler;
            _actionResultConverter = actionResultConverter;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(HandlerResponse<TimezoneResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(HandlerResponse<TimezoneResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(HandlerResponse<TimezoneResponse>))]
        public async Task<IActionResult> GetTimezones([FromQuery] string? timezoneId)
        {
            var request = new TimezoneRequest(timezoneId);
            var response = await _getTimezonesHandler.Execute(request);
            return _actionResultConverter.Convert(response);
        }

        [HttpPost("convert")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(HandlerResponse<ConvertTimezoneResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(HandlerResponse<ConvertTimezoneResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(HandlerResponse<ConvertTimezoneResponse>))]
        public async Task<IActionResult> ConvertTimezone([FromBody] ConvertTimezoneRequest request)
        {
            var response = await _convertTimezoneHandler.Execute(request);
            return _actionResultConverter.Convert(response);
        }
    }
}