using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using Timezones.API.Shared.Handlers;
using Timezones.API.Shared.Helpers;
using System.Diagnostics.CodeAnalysis;
using Timezones.API.Shared.Converters;

namespace Timezones.API.Shared.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(HandlerResponse<T> response, bool withContentOnSuccess = true);
    }

    [ExcludeFromCodeCoverage]
    public class ActionResultConverter : IActionResultConverter
    {
        private readonly string? _path;

        public ActionResultConverter(IHttpContextAccessor accessor)
        {
            _path = accessor?.HttpContext?.Request?.Path.Value;
        }

        public IActionResult Convert<T>(HandlerResponse<T> response, bool withContentOnSuccess = true)
        {
            if (response == null)
                return BuildResult(new[] { new ErrorMessage("000", "ActionResultConverter Error") }, 500);

            if (response.IsSuccess)
            {
                if (withContentOnSuccess)
                {
                    return BuildResult(response, response.StatusCode);
                }
                else
                {
                    return new NoContentResult();
                }
            }
            else if (response.Result != null)
            {
                return BuildResult(response, response.StatusCode);
            }

            return BuildResult(response, response.StatusCode);
        }

        ActionResult BuildResult(object data, int statusCode)
        {
            if (statusCode == (int)HttpStatusCode.InternalServerError)
                Log.Error("[ERROR] {Path} ({@Data})", _path, data);

            var result = new JsonResult(data)
            {
                StatusCode = statusCode,
                SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = new List<JsonConverter>
                        {
                            new DecimalFormatConverterHelper(),
                            new TrimmingJsonConverterHelper(),
                            new StringEnumConverter(),
                            new JsonDateTimeConverter()
                         }
                }
            };
            result.StatusCode = statusCode;
            return result;
        }
    }
}