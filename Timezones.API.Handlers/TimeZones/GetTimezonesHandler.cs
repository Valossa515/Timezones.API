
using Microsoft.Extensions.Logging;
using System.Net;
using Timezones.API.Borders.Dtos.TimeZones;
using Timezones.API.Borders.Handlers;
using Timezones.API.Shared.Enums;
using Timezones.API.Shared.Handlers;
using Timezones.API.Shared.Models;

namespace Timezones.API.Handlers.TimeZones
{
    public class GetTimezonesHandler : HandlerBase<TimezoneRequest, TimezoneResponse, IGetTimezonesHandler>, IGetTimezonesHandler
    {

        public GetTimezonesHandler(
            ILogger<IGetTimezonesHandler> logger) : base(logger)
        {

        }

        public override async Task<HandlerResponse<TimezoneResponse>> DoExecute(TimezoneRequest request)
        {
            return await HandleTimezoneRequest(request);
        }

        private async Task<HandlerResponse<TimezoneResponse>> HandleTimezoneRequest(TimezoneRequest request)
        {
            var response = new HandlerResponse<TimezoneResponse>();

            try
            {
                response.Result = !string.IsNullOrEmpty(request.TimezoneId)
                    ? await GetSingleTimezone(request.TimezoneId)
                    : await GetAllTimezones();

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Status = StatusResponseEnum.Success;
            }
            catch (TimeZoneNotFoundException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Status = StatusResponseEnum.BusinessRuleFailure;
                response.Messages.Add(new Message("001", "Fuso horário não encontrado."));
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = StatusResponseEnum.ServerFailure;
                response.Messages.Add(new Message("002", "Erro inesperado."));
            }

            return response;
        }

        private async Task<List<TimezoneResponse>> GetSingleTimezone(string timezoneId)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);

            return await Task.FromResult(new List<TimezoneResponse>
        {
            new TimezoneResponse
            {
                Id = timezone.Id,
                DisplayName = timezone.DisplayName,
                StandardName = timezone.StandardName,
                BaseUtcOffset = timezone.BaseUtcOffset,
                DaylightName = timezone.DaylightName,
                CurrentTime = currentTime
            }});
        }

        private async Task<List<TimezoneResponse>> GetAllTimezones()
        {
            return await Task.FromResult(TimeZoneInfo.GetSystemTimeZones()
                .Select(tz => new TimezoneResponse
                {
                    Id = tz.Id,
                    DisplayName = tz.DisplayName,
                    StandardName = tz.StandardName,
                    BaseUtcOffset = tz.BaseUtcOffset,
                    DaylightName = tz.DaylightName,
                    CurrentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz)
                })
                .ToList());
        }
    }
}