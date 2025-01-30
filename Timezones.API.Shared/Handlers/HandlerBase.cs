using FluentValidation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using Timezones.API.Shared.Models;
using Timezones.API.Shared.Properties;

namespace Timezones.API.Shared.Handlers
{
    public abstract class HandlerBase<TSchema, TResponse, THandler>
    {
        private readonly ILogger<THandler> _logger;
        private readonly IValidator<TSchema> _validator;

        protected HandlerBase(
            ILogger<THandler> logger, 
            IValidator<TSchema>? validator = null)
        {
            _logger = logger;
            _validator = validator;
        }
        public async Task<HandlerResponse<TResponse>> Execute(TSchema request)
        {
            try
            {
                if (_validator != null)
                    await _validator.ValidateAndThrowAsync(request);

                return await DoExecute(request);
            }
            catch (ValidationException ex)
            {
                return new HandlerResponse<TResponse> { StatusCode = (int)HttpStatusCode.BadRequest, Status = Enums.StatusResponseEnum.BusinessRuleFailure, Messages = ex.Errors.Select(err => new Message(err.PropertyName, err.ErrorMessage)).ToList() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{Resources.UnexpectedError} Item processado: {JsonConvert.SerializeObject(request)}");
                return new HandlerResponse<TResponse> { StatusCode = (int)HttpStatusCode.InternalServerError, Status = Enums.StatusResponseEnum.ServerFailure, Messages = new List<Message> { new Message("000", ex.Message) } };
            }
        }

        public abstract Task<HandlerResponse<TResponse>> DoExecute(TSchema request);
    }
}