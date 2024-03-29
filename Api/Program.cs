using System.Text.Json.Serialization;
using Api.Filters;
using Infrastructure.Context;
using Infrastructure.Extensions;
using Infrastructure.Extensions.Email;
using Infrastructure.Inicialize;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
if (builder.Environment.IsDevelopment())
{
    config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
}
else
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
}
 config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

builder.Services.AddHealthChecks().AddSqlServer(config["ConnectionStrings:database"]);
builder.Services.AddControllers(opts => opts.Filters.Add(typeof(AppExceptionFilterAttribute)))
//     .AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
// });
    ;

builder.Services.AddInfrastructure(config);
builder.Services.AddEndpointsApiExplorer();

Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin() // Permitir cualquier origen
            .AllowAnyMethod()
            .AllowAnyHeader());
});
var app = builder.Build();

app.UseInfrastructure(app.Environment);

using var scope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
var contex = scope!.ServiceProvider.GetRequiredService<PersistenceContext>();
var start = new Start(contex);
start.Inicializar();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseHttpMetrics().UseEndpoints(endpoints =>
{
    endpoints.MapGet("/palm/base-version", () => new { version = 1.0, by = "Finotex" });
    endpoints.MapMetrics();
    endpoints.MapHealthChecks("/health");
});
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program
{
}