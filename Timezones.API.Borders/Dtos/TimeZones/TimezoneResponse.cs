using System.Text.Json.Serialization;
using Timezones.API.Shared.Converters;

namespace Timezones.API.Borders.Dtos.TimeZones
{
    public class TimezoneResponse
    {
        public string Id { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string StandardName { get; set; } = string.Empty;
        public TimeSpan BaseUtcOffset { get; set; }
        public string DaylightName { get; set; } = string.Empty;
        public DateTime CurrentTime { get; set; }
    }
}