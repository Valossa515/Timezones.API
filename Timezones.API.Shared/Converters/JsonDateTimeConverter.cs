using Newtonsoft.Json;

namespace Timezones.API.Shared.Converters
{
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        private const string Format = "yyyy-MM-dd HH:mm:ss";

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return DateTime.MinValue;

            var dateStr = reader.Value.ToString();
            return DateTime.TryParseExact(dateStr, Format, null, System.Globalization.DateTimeStyles.None, out var date)
                ? date
                : DateTime.Parse(dateStr);
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(Format));
        }
    }
}