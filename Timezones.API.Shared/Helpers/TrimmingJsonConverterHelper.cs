using Newtonsoft.Json;

namespace Timezones.API.Shared.Helpers
{
    public class TrimmingJsonConverterHelper : JsonConverter<string?>
    {
        public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
        {
            if (value == null) return;

            writer.WriteValue(value.Trim());
        }

        public override string? ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get { return false; }
        }
    }
}