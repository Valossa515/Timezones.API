using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timezones.API.Borders.Dtos.TimeZones
{
    public class ConvertTimezoneResponse
    {
        public string SourceTimezone { get; set; }
        public string TargetTimezone { get; set; }
        public DateTime ConvertedTime { get; set; }
    }
}