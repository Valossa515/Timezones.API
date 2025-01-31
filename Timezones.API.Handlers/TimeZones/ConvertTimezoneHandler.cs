using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Timezones.API.Borders.Dtos.TimeZones;
using Timezones.API.Borders.Handlers;
using Timezones.API.Shared.Enums;
using Timezones.API.Shared.Handlers;
using Timezones.API.Shared.Models;

namespace Timezones.API.Handlers.TimeZones
{
    public class ConvertTimezoneHandler : HandlerBase<ConvertTimezoneRequest, ConvertTimezoneResponse, IConvertTimezoneHandler>, IConvertTimezoneHandler
    {
        public ConvertTimezoneHandler(ILogger<IConvertTimezoneHandler> logger) : base(logger) { }

        public override async Task<HandlerResponse<ConvertTimezoneResponse>> DoExecute(ConvertTimezoneRequest request)
        {
            var response = new HandlerResponse<ConvertTimezoneResponse>();

            try
            {
                ValidateRequest(request);
                response.Result = new List<ConvertTimezoneResponse> { await ProcessConversion(request) };
                SetSuccessResponse(response);
            }
            catch (TimeZoneNotFoundException ex)
            {
                HandleTimeZoneNotFound(response, ex);
            }
            catch (Exception e)
            {
                HandleUnexpectedError(response, e);
            }

            return response;
        }

        private static void ValidateRequest(ConvertTimezoneRequest request)
        {
            if (string.IsNullOrEmpty(request.SourceTimezoneId) || string.IsNullOrEmpty(request.TargetTimezoneId))
            {
                throw new TimeZoneNotFoundException("Os fusos horários de origem e destino são obrigatórios.");
            }
        }

        private async Task<ConvertTimezoneResponse> ProcessConversion(ConvertTimezoneRequest request)
        {
            var sourceTimezone = TimeZoneInfo.FindSystemTimeZoneById(request.SourceTimezoneId);
            var targetTimezone = TimeZoneInfo.FindSystemTimeZoneById(request.TargetTimezoneId);

            var sourceTime = TimeZoneInfo.ConvertTimeFromUtc(request.UtcDateTime, sourceTimezone);
            var convertedTime = TimeZoneInfo.ConvertTime(sourceTime, sourceTimezone, targetTimezone);

            return await Task.FromResult(new ConvertTimezoneResponse
            {
                SourceTimezone = sourceTimezone.Id,
                TargetTimezone = targetTimezone.Id,
                ConvertedTime = convertedTime
            });
        }

        private static void SetSuccessResponse(HandlerResponse<ConvertTimezoneResponse> response)
        {
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Status = StatusResponseEnum.Success;
        }

        private static void HandleTimeZoneNotFound(HandlerResponse<ConvertTimezoneResponse> response, TimeZoneNotFoundException ex)
        {
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Status = StatusResponseEnum.BusinessRuleFailure;
            response.Messages.Add(new Message("001", $"Fuso horário não encontrado: {ex.Message}"));
        }

        private static void HandleUnexpectedError(HandlerResponse<ConvertTimezoneResponse> response, Exception e)
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Status = StatusResponseEnum.ServerFailure;
            response.Messages.Add(new Message("002", "Erro inesperado ao converter o fuso horário."));
        }
    }
}