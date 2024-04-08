using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using UserAPI;

var builder = WebApplication.CreateBuilder(args);

ConfigureLogging(Environment.GetEnvironmentVariable("ELASTIC_SEARCH"));
builder.Host.UseSerilog();

// Add services to the container.

var startup = new Startup(builder.Configuration); // My custom startup class.

startup.ConfigureServices(builder.Services); // Add services to the container.

builder.Services.RegisterApplicationDependencies();

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();

void ConfigureLogging(string elasticSearchUrl)
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, elasticSearchUrl))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string elasticSearchUrl)
{
    return new ElasticsearchSinkOptions(new Uri(elasticSearchUrl))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"userApi-{DateTime.UtcNow:yyyy-MM}"
    };
}