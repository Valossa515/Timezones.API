using Microsoft.OpenApi.Models;
using Timezones.API.Borders.Handlers;
using Timezones.API.Handlers.TimeZones;
using Timezones.API.Shared.Converters;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddControllers();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Timezones API",
        Version = "v1",
        Description = "API para pesquisa de fusos horários.",
        Contact = new OpenApiContact
        {
            Name = "Felipe Martins",
            Email = "fe.mmo515@gmail.com",
            Url = new Uri("https://github.com/Valossa515")
        }
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter("yyyy-MM-dd HH:mm:ss"));
    });

// Registra os Handlers
builder.Services.AddScoped<IGetTimezonesHandler, GetTimezonesHandler>();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Timezones API v1");
        options.RoutePrefix = string.Empty; // Deixa o Swagger na raiz (http://localhost:5000/)
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
