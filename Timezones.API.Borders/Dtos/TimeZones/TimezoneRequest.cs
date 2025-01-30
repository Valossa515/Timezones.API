using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timezones.API.Borders.Dtos.TimeZones
{
    public record TimezoneRequest(string? TimezoneId)
    {
        public string? TimezoneId { get; init; } = TimezoneId;
    }
}
