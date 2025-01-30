using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timezones.API.Shared.Helpers
{
    public class DecimalFormatConverterHelper : JsonConverter<decimal?>
    {
        public override void WriteJson(JsonWriter writer, decimal? value, JsonSerializer serializer)
        {
            if (value == null) return;

            writer.WriteValue(value.Value.ToString("F4", CultureInfo.InvariantCulture));
        }

        public override decimal? ReadJson(JsonReader reader, Type objectType, decimal? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get { return false; }
        }
    }
}