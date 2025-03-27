using System.Reflection;
using big_core.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Big Core demo BFF API",
        Version = "v1",
        Description = "BFF demonstrativo para coleta de informações da API eLog para monitoramento de veículos.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Ricardo Didimo",
            Email = "ricardodidimo21@gmail.com",
            Url = new Uri("https://github.com/ricardodidimo/big_core_interview")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "Licença MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddApplicationServices();
builder.Services.AddApplicationRepositories();
builder.Services.AddControllers();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Big Core demo BFF API");
    options.RoutePrefix = string.Empty;
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();

public partial class Program { };