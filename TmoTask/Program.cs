using Serilog;
using Serilog.Formatting.Compact;
using TmoTask.Api;
using TmoTask.Application;
using TmoTask.Infrastructure;
using TmoTask.Support;

string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_Environment")??"Developer";

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(new CompactJsonFormatter(), "../Logs/appLog_.txt", rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();

Log.Information("Site is starting up. Configuring Web Host.");

if (string.IsNullOrEmpty(environmentName))
{
    throw new ArgumentException("Environment variable is not set");
}

Log.Information("Environment is {EnvName}", environmentName);

try
{

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    ConfigureServices(builder);

    WebApplication app = builder.Build();
    app.Configure();
    app.Run();

}
catch (Exception ex)
{

    Log.Fatal(ex, "Website terminated unexpectedly");
}
finally
{

    Log.Information("Application Shutdown is complete.");
    Log.CloseAndFlush();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    // Add services to the container.
    builder.AddHostServices();
    builder.Services.AddApiServices(builder.Configuration);
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfrastructureServices(builder.Configuration);
}
