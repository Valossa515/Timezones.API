using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timezones.API.Borders.Dtos.TimeZones
{
    public class ConvertTimezoneRequest
    {
        public DateTime UtcDateTime { get; set; }
        public string SourceTimezoneId { get; set; }
        public string TargetTimezoneId { get; set; }
    }
}