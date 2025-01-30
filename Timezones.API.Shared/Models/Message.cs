namespace Timezones.API.Shared.Models
{
    public record Message(string Code, string Description)
    {
        public string Code { get; set; } = Code;

        public string Description { get; set; } = Description;
    }
}