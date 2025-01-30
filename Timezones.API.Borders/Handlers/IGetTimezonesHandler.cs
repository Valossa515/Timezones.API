using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timezones.API.Borders.Dtos.TimeZones;
using Timezones.API.Shared.Handlers;

namespace Timezones.API.Borders.Handlers
{
    public interface IGetTimezonesHandler : IHandler<TimezoneRequest, TimezoneResponse>
    {
    }
}